using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Accessories
{
    // This example attempts to showcase most of the common boot accessory effects.
    [AutoloadEquip(EquipType.Shoes)]
    public class RunningShoes : ModItem
    {
        public static readonly int MoveSpeedBonus = 1;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

        public override void SetDefaults()
        {
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {


        {
        public int runningTimer;

        public override void ResetEffects()
        {
            runningTimer = 0;
        }
    }
}