using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class BlackSteel : ModItem
    {
        public override void SetDefaults()
        {
            //Common Properties
            Item.rare = ItemRarityID.Blue;
            Item.value = 40464;
            Item.maxStack = 1;

            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = false;

            // Weapon Properties
            Item.damage = 15;
            Item.crit = 96;
            Item.knockBack = 2;
            Item.noUseGraphic = false;
            Item.noMelee = false;
            Item.DamageType = DamageClass.Melee;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.life <= 30)
            {
                // LostSoulHostile is a placeholder, we need a homing spirit ghost shitter
                int projectileID = ProjectileID.LostSoulHostile;
                Vector2 spawnPosition = target.Center;
                Vector2 direction = Main.MouseWorld - target.Center;
                direction.Normalize();
                Projectile.NewProjectile(Projectile.InheritSource(target), spawnPosition, direction * 10f, projectileID, Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }
}
