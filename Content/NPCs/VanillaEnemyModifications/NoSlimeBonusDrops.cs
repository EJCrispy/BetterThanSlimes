using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using HarmonyLib;
using System.Reflection;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class NoBonusSlimeDrops : Mod
    {
        public override void Load()
        {
            // Apply the Harmony patch when the mod loads
            Harmony harmony = new Harmony("BetterThanSlimes.Content.NPCs.VanillaEnemyModifications.NoBonusSlimeDrops");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void Unload()
        {
            // Clean up the Harmony patches when the mod unloads
            Harmony harmony = new Harmony("BetterThanSlimes.Content.NPCs.VanillaEnemyModifications.NoBonusSlimeDrops");
            harmony.UnpatchAll("BetterThanSlimes.Content.NPCs.VanillaEnemyModifications.NoBonusSlimeDrops");
        }
    }

    [HarmonyPatch(typeof(SlimeBodyItemDropRule), "CanDrop")]
    public class SlimeBodyItemDropRulePatch
    {
        public static bool Prefix(ref bool __result)
        {
            // Override the CanDrop method to always return false
            __result = false;
            return false; // Skip the original method
        }
    }
}