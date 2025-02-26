using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BetterThanSlimes.Content.Projectiles.Weapons;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class Hookshot : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot; // Allows swinging in any direction
            Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HookshotProjectile>();
            Item.shootSpeed = 16f;
            Item.noMelee = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true; // Enables right-click functionality
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                // Right-click: Shoots grappling hook
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = ModContent.ProjectileType<HookshotProjectile>();
                Item.shootSpeed = 16f;
                Item.noMelee = true;
            }
            else
            {
                // Left-click: Regular melee attack
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ProjectileID.None;
                Item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Adjust the projectile's velocity based on the player's aim direction
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(0)); // No spread, just straight
        }
    }
}