# GitHub Copilot Instructions for Tactical Ablative Armor (Meat Shields) (Continued) Mod

## Mod Overview and Purpose
Tactical Ablative Armor (Meat Shields) (Continued) is a RimWorld mod that introduces mobile armor technology by allowing humanoids to provide cover for adjacent pawns. The amount of cover provided depends on the body type of the pawn, making it a unique and innovative addition to the game.

## Key Features and Systems
- **Cover System**: Humanoid pawns now provide cover for adjacent pawns based on their body type.
- **Tweakable Cover Values**: The cover values for different body types are adjustable in the mod settings, up to a maximum of 99% cover.
- **Downed Pawns**: Downed pawns can also provide cover, enhancing the defensive capabilities of your colonists.
- **Integration with Humanoid Alien Races**: Adds a default cover provider for alien races.
- **Dependency on Harmony**: The mod requires Harmony to function properly.

## Coding Patterns and Conventions
- **Mod Organization**: The mod is organized into C# source files and XML patch files. C# files contain the core logic and Harmony patches, while XML files define the mod's metadata and dependencies.
- **Naming Conventions**: Use descriptive and consistent naming for classes, methods, and variables. For example, `BodyTypeDefExtension`, `MeatShieldPatcher`, `CoverRegisterComp`, etc.
- **Error Handling**: Implement error handling in critical sections of the code to ensure the mod's stability and provide meaningful error messages.

## XML Integration
The mod uses XML files to define its metadata and dependencies. Here are some key XML files and their purposes:
- **Manifest.xml**: Contains the mod's package ID, display name, and dependencies.
- **About.xml**: Provides information about the mod, including its author and dependencies.
- **Patches/Core_AddComps.xml**: Contains Harmony patches to add components to core game entities.
- **Patches/Core_GiveHumansFillPercent.xml**: Contains Harmony patches to modify the fill percentage of humanoids.

## Harmony Patching
The mod uses Harmony for patching game code. Here are some key Harmony patching techniques used in the mod:
- **Transpilers**: Used to modify the behavior of game methods by replacing specific instructions.
- **Prefixes/Suffixes**: Used to execute code before or after the original game method is called.
- **Target Methods**: Identify the specific game methods to be patched.

## Suggestions for Copilot
- **C# Code Completion**: When implementing new features, use Copilot to complete C# code snippets, such as method definitions, loops, and conditionals.
- **XML File Generation**: Use Copilot to generate XML patch files for Harmony patches, ensuring the correct syntax and structure.
- **Error Handling Templates**: Use Copilot to generate error handling templates for critical sections of the code.
- **Integration with Humanoid Alien Races**: Use Copilot to generate code for integrating with Humanoid Alien Races, ensuring compatibility with the mod's cover system.

By following these guidelines and utilizing GitHub Copilot effectively, you can enhance the development process for the Tactical Ablative Armor (Meat Shields) (Continued) mod and ensure its stability and compatibility with other mods.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

