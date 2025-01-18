using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class LilaBuff : ModBuff
    {
        public static readonly int DefenseBonus = 10;

        public override LocalizedText Description => base.Description.WithFormatArgs(DefenseBonus);

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += DefenseBonus;
        }
    }
}