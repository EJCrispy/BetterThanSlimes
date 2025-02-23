using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Fear : ModBuff
    {
        // Store the original music volume to restore it later
        private float originalMusicVolume = -1; // Use -1 as a flag to indicate no volume is stored

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
            if (originalMusicVolume == -1)
            {
                originalMusicVolume = Main.musicVolume;
            }

            // Set the music volume to 0% while the debuff is active
            Main.musicVolume = 0f;

            // Restore the original music volume when the debuff is about to end
            if (player.buffTime[buffIndex] <= 1) // Check if the buff is about to expire
            {
                if (originalMusicVolume != -1)
                {
                    Main.musicVolume = originalMusicVolume;
                    originalMusicVolume = -1; // Reset the stored volume
                }
            }
        }
    }
}