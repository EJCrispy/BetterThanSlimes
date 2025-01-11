using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BetterThanSlimes.Content.Items.Accessories
{
    public class BigRedNose : ModItem
    {
        public override void SetStaticDefaults()
        {
            // The display name and tooltip will be set using the .lang file
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
            Item.vanity = false;
            Item.rare = ItemRarityID.Blue; // Set the rarity to blue
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().bigRedNose = true;
        }
    }

    public class MyPlayer : ModPlayer
    {
        public bool bigRedNose;

        public override void ResetEffects()
        {
            bigRedNose = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (bigRedNose && info.Damage > 30)
            {
                Vector2 spawnPosition = Player.position;
                spawnPosition.Y -= 45; // Adjust the value as needed

                // Create the explosion projectile
                int proj = Projectile.NewProjectile(Player.GetSource_Misc("BigRedNoseExplosion"), spawnPosition, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 40, 10, Player.whoAmI);

                // Set local immunity for the projectile
                Main.projectile[proj].usesLocalNPCImmunity = true;
                Main.projectile[proj].localNPCHitCooldown = 10; // Adjust the cooldown value as needed
            }
        }
    }

    public class MyGlobalNPC { }
}