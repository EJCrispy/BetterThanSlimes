using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace BetterThanSlimes.Content.Items.Materials
{
    public class Shaft : ModItem
    {
        public override void SetDefaults()
        {
            // Weapon properties
            Item.damage = 3; // Damage value
            Item.DamageType = DamageClass.Melee; // Melee weapon
            Item.width = 32; // Hitbox width
            Item.height = 16; // Hitbox height
            Item.useTime = 50; // Speed of use (in frames)
            Item.useAnimation = 50; // Animation speed (in frames)
            Item.useStyle = ItemUseStyleID.Rapier; // Shortsword thrusting animation
            Item.knockBack = 10f; // Knockback strength
            Item.value = Item.buyPrice(copper: 0); // Value in copper coins
            Item.rare = ItemRarityID.White; // Rarity
            Item.UseSound = SoundID.Item1; // Sound when used
            Item.autoReuse = false; // Cannot auto-swing
            Item.noMelee = false; // This is a melee weapon
            Item.noUseGraphic = false; // Show the weapon when used
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Twig>(), 3); // Requires 3 Twigs
            recipe.Register();
        }
    }
}