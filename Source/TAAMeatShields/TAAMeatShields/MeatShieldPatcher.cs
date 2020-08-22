// Patcher.cs created by Iron Wolf for TAAMeatShields on 08/21/2020 2:12 PM
// last updated 08/21/2020  2:12 PM

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace TAAMeatShields
{
    [StaticConstructorOnStartup]
    public static class MeatShieldPatcher
    {
        private static   MethodInfo FillageMethodReplacer { get; }
        private static MethodInfo FillPercentageReplacer { get; }
        private static FieldInfo ThingDefField { get; }
        private static MethodInfo BaseBlockChanceReplacer { get; }
        private static MethodInfo BaseBlockChanceTarget { get; }
        private static FieldInfo FillPercentageTarget { get; }

        private static MethodInfo TranspilerMI { get; }

        private static MethodInfo FillageTarget { get; }

        static MeatShieldPatcher()
        {
            var tp = typeof(CoverUtils);
            BindingFlags attrs = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
            FillageMethodReplacer = tp.GetMethod(nameof(CoverUtils.GetFillage), attrs);

            FillPercentageReplacer = tp.GetMethod(nameof(CoverUtils.GetFillPercentage), attrs);
            FillPercentageTarget = typeof(ThingDef).GetField(nameof(ThingDef.fillPercent), attrs);
            BaseBlockChanceReplacer = typeof(CoverUtils).GetMethod(nameof(CoverUtils.BaseBlockChance));
            BaseBlockChanceTarget =
                typeof(CoverUtility).GetMethod(nameof(CoverUtility.BaseBlockChance), new Type[] {typeof(ThingDef)});
            FillageTarget = typeof(ThingDef)
                           .GetProperty(nameof(ThingDef.Fillage),
                                        attrs)
                           .GetGetMethod();

            ThingDefField = typeof(Thing).GetField(nameof(Thing.def));

            TranspilerMI = typeof(MeatShieldPatcher).GetMethod(nameof(Transpiler), attrs); 

            var harmony = new HarmonyLib.Harmony(MeatShieldMod.MOD_ID);


            harmony.PatchAll();
            MassPatch(harmony);

        }

        static bool IsPatchable(MethodInfo info, Type gottenType)
        {
            if (info.IsAbstract) return false;
            if (info.IsConstructor) return false;
            if (info.DeclaringType != gottenType) return false;
            return true;
        }

        static void MassPatch(Harmony harmony)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Type targetType in TargetTypes)
            {
                builder.AppendLine($"Patching {targetType.Name}:");
                foreach (MethodInfo methodInfo in GetAllMethodsFromType(targetType))
                {
                    builder.AppendLine($"\t\t{methodInfo.Name}");

                    try
                    {
                        harmony.Patch(methodInfo, transpiler: new HarmonyMethod(TranspilerMI));
                    }
                    catch (Exception e)
                    {
                        builder.AppendLine($"\t\tunable to patch {methodInfo.Name}! caught {e.GetType().Name}\n{e}");
                        Log.Error($"\t\tunable to patch {methodInfo.Name}! caught {e.GetType().Name}\n{e}");
                    }


                }
            }

            Log.Message(builder.ToString()); 

        }

        private static Type[] TargetTypes { get; } = new Type[]
        {
            typeof(Projectile),
            typeof(CoverUtility),
            typeof(DebugOutputsGeneral),
            typeof(CoverGrid),
            typeof(ShotReport)
        };


        static IEnumerable<MethodInfo> GetAllMethodsFromType([NotNull] Type type)
        {
            BindingFlags allFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance; 

            foreach (MethodInfo methodInfo in type.GetMethods(allFlags).Where(m => IsPatchable(m, type)))
            {
                yield return methodInfo; 
            }

            foreach (var nestedType in type.GetNestedTypes(allFlags)) 
            {
                foreach (MethodInfo methodInfo in GetAllMethodsFromType(nestedType))
                {
                    yield return methodInfo;
                }   
            }



        }


        enum ReplacementType
        {
            Fillage,
            FillPercentage,
            BaseBlockChance
        }


        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructionCollection)
        {
            var instructions = instructionCollection.ToList();

            

            for (int i = 0; i < instructions.Count - 2; i++)
            {
                ReplacementType? rpType = null; 
                int j = i + 1;


                var instI = instructions[i];
                var instJ = instructions[j];

                if (instI.opcode == OpCodes.Ldfld && (instI.operand as FieldInfo)==  ThingDefField)
                {
                    if ((instJ.operand as FieldInfo) == FillPercentageTarget)
                    {
                        rpType = ReplacementType.FillPercentage;
                    }else if ((instJ.operand as MethodInfo) == FillageTarget)
                    {
                        rpType = ReplacementType.Fillage;
                    }else if ((instJ.operand as MethodInfo) == BaseBlockChanceTarget)
                    {
                        rpType = ReplacementType.BaseBlockChance;
                    }
                }

                switch (rpType)
                {
                    case ReplacementType.Fillage:
                        instI.opcode = OpCodes.Call;
                        instI.operand = FillageMethodReplacer;
                        break;
                    case ReplacementType.FillPercentage:
                        instI.opcode = OpCodes.Call;
                        instI.operand = FillPercentageReplacer;
                        break;
                    case ReplacementType.BaseBlockChance:
                        instI.opcode = OpCodes.Call;
                        instI.operand = BaseBlockChanceReplacer;
                        break;
                    case null:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                instJ.opcode = OpCodes.Nop;
                instJ.operand = null;
            }


            return instructions;
        }


    }
}