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
            // Common Properties
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
            Item.knockBack = 3;
            Item.noUseGraphic = false;
            Item.noMelee = false;
            Item.DamageType = DamageClass.Melee;
        }

        public override bool? UseItem(Player player)
        {
            // Get the direction vector from the player to the mouse position
            Vector2 direction = Main.MouseWorld - player.Center;
            direction.Normalize();

            // Update the player's direction based on the mouse position
            if (direction.X > 0)
            {
                player.direction = 1;
            }
            else
            {
                player.direction = -1;
            }

            return base.UseItem(player);
        }
    }
}
