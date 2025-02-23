using Terraria;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using System.Reflection;
using Terraria.Graphics.Light;
using System;

namespace BetterThanSlimes
{
    public class FogOfWar : ModSystem
    {
        private static ILHook _updateLightDecayHook;
        private static Hook _lightingModeHook;

        public override void Load()
        {
            // Hook into the Lighting.Mode setter to force it to "Color"
            MethodInfo lightingModeSetter = typeof(Lighting)
                .GetProperty("Mode", BindingFlags.Public | BindingFlags.Static)
                ?.GetSetMethod();

            if (lightingModeSetter != null)
            {
                _lightingModeHook = new Hook(lightingModeSetter, ForceLightingMode);
                Mod.Logger.Info("Successfully hooked into Lighting.Mode setter!");
            }
            else
            {
                Mod.Logger.Error("Failed to find Lighting.Mode setter!");
            }

            // Force the lighting mode to "Color" initially
            Lighting.Mode = LightMode.Color;

            // Find the UpdateLightDecay method in the LightingEngine class
            MethodInfo updateLightDecayMethod = typeof(LightingEngine)
                .GetMethod("UpdateLightDecay", BindingFlags.NonPublic | BindingFlags.Instance);

            if (updateLightDecayMethod != null)
            {
                // Create an IL hook for the UpdateLightDecay method
                _updateLightDecayHook = new ILHook(updateLightDecayMethod, ModifyLightDecayValues);
                Mod.Logger.Info("Successfully hooked into UpdateLightDecay!");
            }
            else
            {
                Mod.Logger.Error("Failed to find method: UpdateLightDecay");
            }
        }

        public override void Unload()
        {
            // Dispose of the hooks when the mod is unloaded
            _updateLightDecayHook?.Dispose();
            _lightingModeHook?.Dispose();
            Mod.Logger.Info("Unloaded FogOfWar functionality.");
        }

        // Hook to force the lighting mode to "Color"
        private void ForceLightingMode(Action<LightMode> orig, LightMode mode)
        {
            // Override the mode to "Color"
            orig(LightMode.Color);
            Mod.Logger.Info("Forced Lighting.Mode to Color!");
        }

        private void ModifyLightDecayValues(ILContext il)
        {
            var c = new ILCursor(il);

            // Find the instruction where LightDecayThroughAir is set to 0.91f
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdcR4(0.91f)))
            {
                // Replace 0.91f with 0.60f
                c.Prev.Operand = 0.71f;
                Mod.Logger.Info("Modified LightDecayThroughAir to 0.60f!");
            }
            else
            {
                Mod.Logger.Error("Failed to find LightDecayThroughAir assignment!");
            }

            // Reset the cursor to the start of the method
            c.Index = 0;

            // Find the instruction where LightDecayThroughSolid is set to 0.56f
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdcR4(0.56f)))
            {
                // Replace 0.56f with 0.01f
                c.Prev.Operand = 0.37f;
                Mod.Logger.Info("Modified LightDecayThroughSolid to 0.01f!");
            }
            else
            {
                Mod.Logger.Error("Failed to find LightDecayThroughSolid assignment!");
            }
        }
    }
}