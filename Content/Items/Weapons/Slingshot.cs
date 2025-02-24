using System;
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
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 30;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.5f;
            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SlingshotProjectile>();
            Item.useAmmo = ModContent.ItemType<LooseStone>();
            Item.noMelee = true;
        }
        private float Charge = 1f;
        private bool maxChargeSoundPlayed = false;

        public override void HoldItem(Player player)
        {
            if (lastLMouse && !Main.mouseLeft && CanUseItem(player))
            {
                delay = (int)MathF.Ceiling(30f); // Reset delay on release
            }
            if (delay-- < 0)
            {
                delay = 0;
            }

            if (player.channel)
            {
                // Increase charge gradually up to a max of 4
                if (Charge < 4f)
                {
                    Charge += 1f / 30f;
                    maxChargeSoundPlayed = false; // Reset when not at max
                }
                else if (!maxChargeSoundPlayed)
                {
                    // Play "The Axe" sound with higher pitch
                    SoundEngine.PlaySound(SoundID.Item47 with { Volume = 1f, Pitch = 2f }, player.position);
                    maxChargeSoundPlayed = true;
                }

                // Spawn holding projectile if not present
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SlingshotHoldingProjectile>()] < 1 && delay == 0 && player.HasAmmo(Item))
                {
                    int damage = (int)(player.GetDamage(DamageClass.Ranged).ApplyTo(Item.damage));
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<SlingshotHoldingProjectile>(),
                        damage,
                        Item.knockBack,
                        player.whoAmI,
                        Charge
                    );
                }
            }
            else
            {
                Charge = 1f; // Reset charge on release
                maxChargeSoundPlayed = false;
            }

            lastLMouse = Main.mouseLeft;
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Return false to prevent default shooting behavior since we handle it manually in HoldItem
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensure the player has ammo to use the weapon
            if (!player.HasAmmo(Item))
            {
                return false; // Prevent use if no ammo is available
            }
            return delay <= 0 && base.CanUseItem(player);
        }
    }
}