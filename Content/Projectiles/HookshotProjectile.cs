using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace BetterThanSlimes.Content.Projectiles
{
    public class HookshotProjectile : ModProjectile
    {
        private NPC latchedNPC = null;
        private Vector2 latchedTile;
        private bool latchedToTile = false;
        private int groundLatchTimer = 0; // Timer for ground latch duration
        private int enemyLatchTimer = 0; // Timer for enemy latch duration
        private const int MaxRange = 320; // 20 blocks * 16 pixels per block
        private const int GroundLatchDuration = 9; // 0.15 seconds (9 frames)
        private const int EnemyLatchDuration = 120; // 2 seconds (120 frames)
        private bool hasDealtDamage = false; // Ensure damage is only dealt once

        // Static variable to track the active projectile
        private static HookshotProjectile activeProjectile = null;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 3600; // Set a large initial timeLeft to prevent immediate despawn
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Kill the projectile if the player takes damage or dies
            if (player.dead || player.immune)
            {
                Projectile.Kill();
                return;
            }

            // Prevent the player from using grappling hooks while holding the Hookshot
            player.grappling[0] = -1;
            player.grapCount = 0;

            // If there's an active projectile, kill the previous one
            if (activeProjectile != null && activeProjectile != this)
            {
                activeProjectile.Projectile.Kill();
            }

            // Set this as the active projectile
            activeProjectile = this;

            // Check if the projectile exceeds the maximum range
            if (Vector2.Distance(player.Center, Projectile.Center) > MaxRange)
            {
                Projectile.Kill();
                return;
            }

            // Rotate the projectile to face the direction it is moving
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }

            if (latchedNPC != null)
            {
                // Increment the enemy latch timer
                enemyLatchTimer++;

                // Give the player the Striking Moment buff while latched to an enemy
                player.AddBuff(BuffID.ParryDamageBuff, 30);

                // Deal damage only once when initially latching onto the enemy
                if (!hasDealtDamage)
                {
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = 15,
                        Knockback = 0f,
                        HitDirection = player.direction
                    };
                    latchedNPC.StrikeNPC(hitInfo);
                    hasDealtDamage = true; // Ensure damage is only dealt once

                    // Set a very large timeLeft to prevent the projectile from expiring
                    Projectile.timeLeft = 1000000; // Effectively infinite
                }

                // Check if player is close enough to bounce
                if (Vector2.Distance(player.Center, latchedNPC.Center) < 30f && player.controlUseItem)
                {
                    // Launch player away from the NPC
                    Vector2 launchDirection = Vector2.Normalize(player.Center - latchedNPC.Center);
                    player.velocity = launchDirection * 10f; // Launch the player backwards

                    // Apply vertical launch (up or down) depending on the NPC's position
                    if (player.Center.Y < latchedNPC.Center.Y)
                    {
                        player.velocity.Y = -15f; // Launch upwards
                    }
                    else
                    {
                        player.velocity.Y = 15f; // Launch downwards
                    }

                    Projectile.Kill(); // Kill the projectile after the action
                }
                else
                {
                    // Move the player towards the latched NPC
                    player.velocity = Vector2.Normalize(latchedNPC.Center - player.Center) * 10f;
                    Projectile.position = latchedNPC.Center;
                }

                // Kill the projectile after 2 seconds (120 frames) of being latched to an enemy
                if (enemyLatchTimer >= EnemyLatchDuration)
                {
                    Projectile.Kill();
                }
            }
            else if (latchedToTile)
            {
                // Increment the ground latch timer
                groundLatchTimer++;

                // Move the player towards the latched tile
                player.velocity = Vector2.Normalize(latchedTile - player.Center) * 10f;
                Projectile.position = latchedTile;

                // Kill the projectile after 0.15 seconds (9 frames)
                if (groundLatchTimer >= GroundLatchDuration)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                // Check for collision with an NPC
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && Projectile.Hitbox.Intersects(npc.Hitbox))
                    {
                        latchedNPC = npc;
                        Projectile.velocity = Vector2.Zero;
                        hasDealtDamage = false; // Reset damage flag when latching to a new enemy
                        break;
                    }
                }

                // Check for tile collision
                if (!latchedToTile)
                {
                    Vector2 checkTile = Projectile.position + Projectile.velocity;
                    int tileX = (int)(checkTile.X / 16f);
                    int tileY = (int)(checkTile.Y / 16f);
                    if (Main.tile[tileX, tileY].HasTile)
                    {
                        latchedTile = new Vector2(tileX * 16, tileY * 16);
                        latchedToTile = true;
                        Projectile.velocity = Vector2.Zero;
                    }
                }
            }
        }

        // Use Utils.DrawLine to draw the chain
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            // Draw the chain between the player and the projectile
            Vector2 start = player.Center;
            Vector2 end = Projectile.Center;
            Terraria.Utils.DrawLine(Main.spriteBatch, start, end, Color.White, Color.White, 1f);

            return true;
        }

        // Ensure the projectile can always be fired
        public static bool CanFireHookshot(Player player)
        {
            // Ensure only one Hookshot can be fired at a time
            if (activeProjectile != null && activeProjectile.Projectile.active)
            {
                return false; // There's already an active projectile
            }
            return true; // No active projectile, can fire
        }

        // Reset the activeProjectile when the projectile is killed
        public override void Kill(int timeLeft)
        {
            activeProjectile = null;
        }
    }
}