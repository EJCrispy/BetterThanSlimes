using BetterThanSlimes.Content.Items.Materials;
using BetterThanSlimes.Content.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Consumables
{
    public class CookedMorsel : ModItem
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
            Item.rare = ItemRarityID.Blue;
            Item.value = 100000000;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 24600;
        } // Buff duration in ticks
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Morsel>(), 1);
            recipe.AddTile(TileID.Campfire);
            recipe.Register();
        }

    }
}
