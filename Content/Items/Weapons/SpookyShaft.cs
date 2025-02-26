using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using BetterThanSlimes.Content.Projectiles.Weapons;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class SpookyShaft : ModItem
    {
        public override void SetDefaults()
        {
            // Weapon properties
            Item.damage = 44; // Damage value
            Item.DamageType = DamageClass.Melee; // Melee weapon
            Item.width = 64; // Hitbox width
            Item.height = 64; // Hitbox height
            Item.useTime = 3; // Speed of use (in frames)
            Item.useAnimation = 3; // Animation speed (in frames)
            Item.useStyle = ItemUseStyleID.Rapier; // Rapier-style animation
            Item.knockBack = 35f; // Knockback strength
            Item.value = Item.buyPrice(copper: 0); // Value in copper coins
            Item.rare = ItemRarityID.White; // Rarity
            Item.UseSound = SoundID.Item1; // Sound when used
            Item.autoReuse = false; // Cannot auto-swing
            Item.noMelee = true; // Required for rapier-style weapons
            Item.noUseGraphic = true; // Required for rapier-style weapons

            Item.shoot = ModContent.ProjectileType<SpookyShaftProjectile>(); // The projectile is what makes a shortsword work
            Item.shootSpeed = 2.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyTwig);
            recipe.Register();
        }
    }
}