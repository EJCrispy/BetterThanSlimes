using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Consumables
{
    public class Morsel : ModItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = 10000;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 15000; // Buff duration in ticks
        }
    }
}
