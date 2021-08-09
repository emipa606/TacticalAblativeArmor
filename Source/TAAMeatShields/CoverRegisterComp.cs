// CoverRegisterComp.cs created by Iron Wolf for TAAMeatShields on 08/22/2020 6:58 AM
// last updated 08/22/2020  6:58 AM

using System.Reflection;
using Verse;

namespace TAAMeatShields
{
    [StaticConstructorOnStartup]
    public class CoverRegisterComp : ThingComp
    {
        private const int TICK_PERIOD = 100;

        private IntVec3? _lastPos;

        private bool? _movingLast;

        private Pawn _pPawn;

        static CoverRegisterComp()
        {
            RecalculateCellMethod =
                typeof(CoverGrid).GetMethod("RecalculateCell", BindingFlags.Instance | BindingFlags.NonPublic);
            ArgArr = new object[2];
        }

        private static object[] ArgArr { get; }
        private Pawn Parent => _pPawn ?? (_pPawn = (Pawn) parent);

        private bool ShouldBeCover => Parent.GetFillagePawn() != FillCategory.None;


        // private void RecalculateCell(IntVec3 c, Thing ignoreThing = null)       

        private static MethodInfo RecalculateCellMethod { get; }

        private void RecalculateCell(IntVec3 cell, CoverGrid grid)
        {
            ArgArr[0] = cell;
            RecalculateCellMethod.Invoke(grid, ArgArr);
        }


        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            _lastPos = Parent.Position;
        }

        public override void CompTick()
        {
            base.CompTick();

            if (!parent.IsHashIntervalTick(TICK_PERIOD) || !ShouldBeCover)
            {
                return;
            }

            var map = parent.Map;
            if (map == null)
            {
                return;
            }

            var movingNow = Parent.pather.MovingNow;
            if (movingNow != _movingLast)
            {
                _movingLast = movingNow;

                if (movingNow && _lastPos != null)
                {
                    RecalculateCell(_lastPos.Value, map.coverGrid);
                    map.coverGrid.DeRegister(parent);
                }
                else
                {
                    _lastPos = Parent.Position;
                    map.coverGrid.Register(parent);
                }
            }
            else if (_lastPos != Parent.Position)
            {
                if (_lastPos != null)
                {
                    RecalculateCell(_lastPos.Value, map.coverGrid);
                }

                map.coverGrid.Register(Parent);
                _lastPos = Parent.Position;
            }
        }
    }
}