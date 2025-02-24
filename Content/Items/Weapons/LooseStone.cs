using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class LooseStone : ModItem
    {
        public override void SetDefaults()
        {

            //Common Properties
            Item.rare = ItemRarityID.White;
            Item.value = 0;
            Item.maxStack = 9999;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 46;
            Item.useTime = 46;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.ammo = Item.type; // Set as ammo


            // Weapon Properties
            Item.damage = 3;
            Item.knockBack = 3;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;

            // Projectile Properties
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Projectiles.LooseStoneProjectile>();
        }
    }
}
