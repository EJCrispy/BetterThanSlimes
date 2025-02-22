using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using BetterThanSlimes.Content.Projectiles;
using Terraria.Audio;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class Slingshot : ModItem
    {

        private int delay = 0;
        private bool lastLMouse = false;

        public static readonly SoundStyle PullSound = new SoundStyle($"FargowiltasSouls/Assets/Sounds/Weapons/BowPull") with { Volume = 1f };
        public static readonly SoundStyle ReleaseSound = new SoundStyle($"FargowiltasSouls/Assets/Sounds/Weapons/BowRelease") with { Volume = 1f };
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Spirit Longbow");
            //Tooltip.SetDefault("Converts arrows to Spirit Arrows that release spirit energy behind them\nHold button to charge shots for more damage and higher speed");

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 30;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.5f;
            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item2;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<LooseStoneProjectile>();
            Item.useAmmo = ModContent.ItemType<LooseStone>();
            Item.noMelee = true;
        }

        private float Charge = 1;

        public override void HoldItem(Player player)
        {
            if (lastLMouse && !Main.mouseLeft && CanUseItem(player))
            {
                delay = (int)MathF.Ceiling(30f);
            }
            if (delay-- < 0)
            {
                delay = 0;
            }
            if (player.channel)
            {
                if (Charge < 4)
                {
                    Charge += 1 / 30f;
                }
            }
            else
            {
                Charge = 1;
            }
            lastLMouse = Main.mouseLeft;

            if (player.channel && player.ownedProjectileCounts[Item.shoot] < 1 && delay == 0)
            {
                int damage = (int)(player.GetDamage(DamageClass.Ranged).ApplyTo(Item.damage));

                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, Item.shoot, damage, Item.knockBack, player.whoAmI);
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false; // projectile is manually spawned in HoldItem
        }

        public override bool CanUseItem(Player player)
        {
            return delay <= 0 && base.CanUseItem(player);

        }

    }
}
