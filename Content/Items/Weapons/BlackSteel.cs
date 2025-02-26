using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BetterThanSlimes.Content.Projectiles.Weapons;

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
            Item.crit = 104;
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

namespace BetterThanSlimes.Content
{
    public class GlobalNPCMod : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];

            // Check if the NPC was killed by your custom weapon and is not a critter
            if (npc.catchItem <= 0 && player.HeldItem.type == ModContent.ItemType<Items.Weapons.BlackSteel>())
            {
                int projectileID = ModContent.ProjectileType<VengefulSpirit>();
                Vector2 spawnPosition = npc.Center;

                // Determine the number of projectiles to spawn (between 1 and 3)
                int numberOfProjectiles = Main.rand.Next(1, 4);

                // Spawn the projectiles in random directions
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    // Generate random direction for the projectile
                    Vector2 randomDirection = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)).SafeNormalize(Vector2.UnitY);

                    // Set speed similar to Dungeon Spirit (around 6-8)
                    float speed = Main.rand.NextFloat(6f, 8f);

                    // Spawn the projectile with the random direction and speed
                    Projectile.NewProjectile(npc.GetSource_Death(), spawnPosition, randomDirection * speed, projectileID, 15, 2, Main.myPlayer);
                }
            }
        }
    }
}
