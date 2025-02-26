using BetterThanSlimes.Content.Projectiles.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class MagnifyingGlass : ModItem
    {
        private float Charge = 1f;
        private bool maxChargeSoundPlayed = false;

        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 3;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 18;
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
            Item.shoot = ProjectileID.None;
            Item.noMelee = true;
        }

        public override void HoldItem(Player player)
        {
            if (player.channel)
            {
                if (Charge < 4f)
                {
                    Charge += 1f / 30f;
                    maxChargeSoundPlayed = false;
                }
                else if (!maxChargeSoundPlayed)
                {
                    SoundEngine.PlaySound(SoundID.Item82 with { Volume = 1f, Pitch = 2f }, player.position);
                    maxChargeSoundPlayed = true;
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<HoldingMagnifyingGlass>()] < 1)
                {
                    int damage = (int)(player.GetDamage(DamageClass.Ranged).ApplyTo(Item.damage));
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<HoldingMagnifyingGlass>(),
                        damage,
                        Item.knockBack,
                        player.whoAmI,
                        Charge
                    );
                }
            }
            else
            {
                Charge = 1f;
                maxChargeSoundPlayed = false;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
}
