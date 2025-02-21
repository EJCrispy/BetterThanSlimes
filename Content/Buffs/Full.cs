using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Full : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false; // This buff will save when exiting and rejoining the world
            Main.buffNoTimeDisplay[Type] = false; // Time will be displayed for this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Remove the Starvation debuff if it's active
            if (player.buffTime[buffIndex] > 1) // While the Full buff is active
            {
                player.ClearBuff(ModContent.BuffType<Starvation>());
            }

        }
    }
}