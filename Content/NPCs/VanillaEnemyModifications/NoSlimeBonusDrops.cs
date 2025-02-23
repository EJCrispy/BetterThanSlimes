using Terraria;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System.Reflection;

namespace YourModNamespace
{
    public class BetterThanSlimes : GlobalNPC
    {
        private static ILHook _slimeDropRuleHook;
        private static ILHook _generateItemHook;

        public override void Load()
        {
            // Call the base Load method
            base.Load();

            // Hook into GetDropInfo to disable bonus drops
            MethodInfo getDropInfoMethod = typeof(Terraria.GameContent.ItemDropRules.SlimeBodyItemDropRule)
                .GetMethod("GetDropInfo", BindingFlags.Instance | BindingFlags.Public);

            if (getDropInfoMethod != null)
            {
                _slimeDropRuleHook = new ILHook(getDropInfoMethod, DisableSlimeBonusDrops);
                Mod.Logger.Info("Successfully hooked into GetDropInfo!");
            }
            else
            {
                Mod.Logger.Error("Failed to find method: GetDropInfo");
            }

            // Hook into AI_001_Slimes_GenerateItemInsideBody to disable item generation
            MethodInfo generateItemMethod = typeof(NPC)
                .GetMethod("AI_001_Slimes_GenerateItemInsideBody", BindingFlags.Static | BindingFlags.NonPublic);

            if (generateItemMethod != null)
            {
                _generateItemHook = new ILHook(generateItemMethod, DisableItemGeneration);
                Mod.Logger.Info("Successfully hooked into AI_001_Slimes_GenerateItemInsideBody!");
            }
            else
            {
                Mod.Logger.Error("Failed to find method: AI_001_Slimes_GenerateItemInsideBody");
            }
        }

        public override void Unload()
        {
            // Call the base Unload method
            base.Unload();

            // Dispose of the hooks when the mod is unloaded
            _slimeDropRuleHook?.Dispose();
            _generateItemHook?.Dispose();
            Mod.Logger.Info("Unloaded NoSlimeBonusDrops functionality.");
        }

        private void DisableSlimeBonusDrops(ILContext il)
        {
            var c = new ILCursor(il);

            // Replace the entire method body with a simple return statement
            c.Emit(OpCodes.Ret); // Immediately return without executing any logic
            Mod.Logger.Info("Disabled slime bonus drops!");
        }

        private void DisableItemGeneration(ILContext il)
        {
            var c = new ILCursor(il);

            // Replace the entire method body with a return 0 statement
            c.Emit(OpCodes.Ldc_I4_0); // Load the constant 0 onto the stack
            c.Emit(OpCodes.Ret);      // Return the value 0
            Mod.Logger.Info("Disabled item generation inside slimes!");
        }
    }
}