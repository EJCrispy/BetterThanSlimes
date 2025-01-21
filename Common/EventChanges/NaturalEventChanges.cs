using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace BetterThanSlimes.Common.EventChanges
{
    public class BossEventChanges : ModSystem // Change to extend ModSystem
    {
        // Flags to track whether bosses have been defeated
        public static bool HasEyeOfCthulhuBeenDefeated = false;
        public static bool HasMechanicalBossesBeenDefeated = false;

        public override void OnWorldLoad()
        {
            // Initialize flags when the world is loaded
            HasEyeOfCthulhuBeenDefeated = false;
            HasMechanicalBossesBeenDefeated = false;
        }

        public override void PreUpdateWorld()
        {
            // Prevent the Eye of Cthulhu from spawning if it hasn't been defeated
            if (Main.bloodMoon && !HasEyeOfCthulhuBeenDefeated)
            {
                // Stop Eye of Cthulhu from spawning unless defeated
                Main.bloodMoon = false;
            }

            // Prevent mechanical bosses from spawning if they haven't been defeated
            if (Main.eclipse && !HasMechanicalBossesBeenDefeated)
            {
                // Stop mechanical bosses from spawning unless defeated
                Main.eclipse = false;
            }
        }

        public override void PostWorldGen()
        {
            // If you want to trigger boss events manually based on world generation, you can do it here
            // For example, triggering the Eye of Cthulhu after world generation
            if (!HasEyeOfCthulhuBeenDefeated)
            {
                HasEyeOfCthulhuBeenDefeated = true;
                // Manually set a flag for Eye of Cthulhu defeat
                // This can be done via world generation or manually after defeating it
            }

            // Similarly, handle mechanical bosses (The Destroyer, Skeletron Prime, The Twins)
            if (!HasMechanicalBossesBeenDefeated)
            {
                HasMechanicalBossesBeenDefeated = true;
                // Set flags for the mechanical bosses
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            // Save the boss defeat flags to world data
            tag["HasEyeOfCthulhuBeenDefeated"] = HasEyeOfCthulhuBeenDefeated;
            tag["HasMechanicalBossesBeenDefeated"] = HasMechanicalBossesBeenDefeated;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            // Load the boss defeat flags from world data
            HasEyeOfCthulhuBeenDefeated = tag.GetBool("HasEyeOfCthulhuBeenDefeated");
            HasMechanicalBossesBeenDefeated = tag.GetBool("HasMechanicalBossesBeenDefeated");
        }
    }

    // In your mod player class, you could manually set these flags based on event triggers
    public class CustomPlayer : ModPlayer
    {
        public override void UpdateDead()
        {
            // If the player dies, check if the bosses have been defeated
            if (!BossEventChanges.HasEyeOfCthulhuBeenDefeated)
            {
                BossEventChanges.HasEyeOfCthulhuBeenDefeated = true;
            }

            if (!BossEventChanges.HasMechanicalBossesBeenDefeated)
            {
                BossEventChanges.HasMechanicalBossesBeenDefeated = true;
            }
        }
    }
}
