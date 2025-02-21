using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Full : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; // This is not a debuff
            Main.buffNoSave[Type] = false; // This buff won't save when exiting and rejoining the world
            Main.buffNoTimeDisplay[Type] = false; // Time will be displayed for this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // When the buff is about to run out, apply the Starvation debuff permanently
            if (player.buffTime[buffIndex] == 1) // 1 frame before the buff expires
            {
                player.AddBuff(ModContent.BuffType<Starvation>(), int.MaxValue); // Apply Starvation permanently
            }

            // Remove the Starvation debuff if it's active
            if (player.buffTime[buffIndex] > 1) // While the Full buff is active
            {
                player.ClearBuff(ModContent.BuffType<Starvation>());
            }

            // Apply defense bonuses based on the remaining time of the Full buff
            if (player.buffTime[buffIndex] > 60 * 8 * 60) // 8 minutes or more (480 seconds)
            {
                player.statDefense += 10; // +10 defense for 8+ minutes
            }
            else if (player.buffTime[buffIndex] > 60 * 4 * 60) // 4 minutes or more (240 seconds)
            {
                player.statDefense += 5; // +5 defense for 4+ minutes
            }
        }
    }
}