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

        public override void Load()
        {
            // Call the base Load method
            base.Load();

            // Find the GetDropInfo method in SlimeBodyItemDropRule
            MethodInfo method = typeof(Terraria.GameContent.ItemDropRules.SlimeBodyItemDropRule)
                .GetMethod("GetDropInfo", BindingFlags.Instance | BindingFlags.Public);

            if (method != null)
            {
                // Create an IL hook for the method
                _slimeDropRuleHook = new ILHook(method, DisableSlimeBonusDrops);
                Mod.Logger.Info("Successfully hooked into GetDropInfo!");
            }
            else
            {
                Mod.Logger.Error("Failed to find method: GetDropInfo");
            }
        }

        public override void Unload()
        {
            // Call the base Unload method
            base.Unload();

            // Dispose of the hook when the mod is unloaded
            _slimeDropRuleHook?.Dispose();
            Mod.Logger.Info("Unloaded NoSlimeBonusDrops functionality.");
        }

        private void DisableSlimeBonusDrops(ILContext il)
        {
            var c = new ILCursor(il);

            // Replace the entire method body with a simple return statement
            c.Emit(OpCodes.Ret); // Immediately return without executing any logic
            Mod.Logger.Info("Disabled slime bonus drops!");
        }
    }
}