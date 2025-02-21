using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Starvation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // This is a debuff
            Main.buffNoSave[Type] = false; // This buff won't save when exiting and rejoining the world
            Main.buffNoTimeDisplay[Type] = true; // Time will be displayed for this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Gradually reduce the player's health
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 6; // Adjust this value to control how fast the player loses health

            // Optionally, kill the player if their health reaches 0
            if (player.statLife <= 0)
            {
                player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " starved to death."), 1, 0);
            }
        }
    }
}