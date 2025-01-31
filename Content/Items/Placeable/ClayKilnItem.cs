using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Placeable
{
    public class ClayKilnItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 14;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ClayKiln>(); // Ensure this matches the class
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RedBrick, 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
