using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Fear : ModBuff
    {
        // Store the original music volume to restore it later
        private float? originalMusicVolume = null;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // This is a debuff
            Main.buffNoSave[Type] = false; // This buff won't save when exiting and rejoining the world
            Main.buffNoTimeDisplay[Type] = true; // Time will be displayed for this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Halve the player's movement speed
            player.moveSpeed *= 0.5f;

            // Halve the player's jump speed
            player.jumpSpeedBoost *= 0.5f;

            // If the debuff is just applied, store the original music volume
            if (originalMusicVolume == null)
            {
                originalMusicVolume = Main.musicVolume;
            }

            // Set the music volume to 0% while the debuff is active
            Main.musicVolume = 0f;

            // Restore the original music volume when the debuff ends
            if (player.buffTime[buffIndex] <= 1) // Check if the buff is about to end
            {
                if (originalMusicVolume.HasValue)
                {
                    Main.musicVolume = originalMusicVolume.Value;
                    originalMusicVolume = null; // Reset the stored volume
                }
            }
        }
    }
}