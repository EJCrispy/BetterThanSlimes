using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BetterThanSlimes.Content.Items.Materials;
using System;

namespace BetterThanSlimes.Content.Items.Tools
{
    public class Dreamcatcher : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24; // Width of the item's hitbox
            Item.height = 28; // Height of the item's hitbox
            Item.rare = ItemRarityID.Blue; // Rarity of the item
            Item.value = Item.sellPrice(silver: 50); // Sell price (50 silver)
            Item.useTime = 10; // How quickly the item is used
            Item.useAnimation = 10; // Animation duration
            Item.useStyle = ItemUseStyleID.HoldUp; // Holding up the item
            Item.noMelee = true; // Doesn't deal melee damage
            Item.autoReuse = true; // Automatically reuse the item
            Item.noUseGraphic = false; // Show the item graphic when held
        }

        public override void HoldItem(Player player)
        {
            // Only function if the player is actively holding left-click
            if (player.controlUseItem)
            {
                // Attract Fallen Stars while the item is held
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.active && projectile.type == ProjectileID.FallingStar)
                    {
                        // Check if the star is within a 150-block radius of the player
                        float distance = Vector2.Distance(projectile.Center, player.Center);
                        if (distance <= 150 * 16) // 150 blocks (1 block = 16 pixels)
                        {
                            // Target a point directly below the player (on the ground)
                            Vector2 targetPosition = new Vector2(
                                player.Center.X, // Same X position as the player
                                player.position.Y + player.height + 2200 // Ground level below the player
                            );

                            // Check if the star is close to the target position
                            if (Vector2.Distance(projectile.Center, targetPosition) > 16f) // 16 pixels = 1 block
                            {
                                // Adjust the projectile's velocity to move toward the target position
                                Vector2 direction = targetPosition - projectile.Center;
                                direction.Normalize();
                                projectile.velocity = direction * 10f; // Adjust speed as needed
                            }
                            else
                            {
                                // If the star is close enough, reset its velocity to behave like a normal star
                                projectile.velocity = new Vector2(0, 5f); // Default falling behavior

                                // Disable further attraction for this star
                                projectile.ai[0] = 1; // Use ai[0] as a flag to mark the star as "reached"
                            }
                        }
                    }
                }

                // Check if the player is on the surface layer and it is night
                if (player.ZoneOverworldHeight && !Main.dayTime)
                {
                    // Spawn yellow sparkling dust particles that move toward the player
                    if (Main.rand.NextBool(2)) // Adjust spawn rate as needed
                    {
                        // Random position around the player
                        Vector2 spawnPosition = player.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));

                        // Create the dust particle
                        Dust sparkleDust = Dust.NewDustPerfect(
                            spawnPosition, // Spawn position
                            DustID.YellowTorch, // Yellow sparkling dust
                            Vector2.Zero, // Initial velocity (zero for now)
                            150, // Alpha (transparency)
                            default, // No color override
                            Main.rand.NextFloat(0.3f, 0.6f) // Reduced scale for less light
                        );

                        // Set the dust's velocity to move toward the player
                        Vector2 direction = player.Center - spawnPosition;
                        direction.Normalize();
                        sparkleDust.velocity = direction * Main.rand.NextFloat(2f, 4f); // Random speed

                        sparkleDust.noGravity = true; // Make the dust float
                        sparkleDust.fadeIn = 1f; // Fade in effect
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            // Crafting recipe for the Dreamcatcher
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Twig>(), 5) // 5 Twigs
                .AddIngredient(ItemID.Cobweb, 2) // 2 Cobwebs
                .Register();
        }
    }
}