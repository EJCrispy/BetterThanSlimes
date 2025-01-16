using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Tools
{
    public class StoneHatchet : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 52;
            Item.useAnimation = 52;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = 10;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

            Item.axe = 1; // How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
            Item.tileBoost = -1; // Set the range to -1
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Add any melee effects you want to apply here
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddIngredient(5553, 2); // 5553 is twig
            recipe.AddIngredient(5555, 1); // 5555 is loose stone
            recipe.Register();
        }
    }
}
