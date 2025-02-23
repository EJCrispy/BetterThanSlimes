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
                }
            }
        }
    