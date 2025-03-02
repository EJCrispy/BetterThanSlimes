using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // For Vector2

namespace BetterThanSlimes.Common.VanillaItemChanges.Weapons
{
    internal class FlareGunChanges : GlobalItem
    {
        // Override the SetDefaults method to modify the Flare Gun's properties
        public override void SetDefaults(Item item)
        {
            // Check if the item is the Flare Gun
            if (item.type == ItemID.FlareGun)
            {
                // Set the use time (delay between uses) to 130
                item.useTime = 200;
                item.useAnimation = 20;
            }
        }

        // Override the UseItem method to control the shooting sound
        public override bool? UseItem(Item item, Player player)
        {
            // Check if the item is the Flare Gun
            if (item.type == ItemID.FlareGun)
            {
                // Check if the useTime has elapsed
                if (player.itemTime == 0)
                {
                    // Play the shooting sound only if useTime has elapsed
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item11, player.Center);
                }
            }
            return base.UseItem(item, player);
        }
    }

    // Create a new GlobalProjectile class to modify the Flare Gun's projectile behavior
    internal class FlareGunProjectileChanges : GlobalProjectile
    {
        // Track the number of enemies pierced
        private int pierceCount = 0;

        // Track whether the projectile has stuck to an enemy
        private bool isStuck = false;

        // Track the enemy the projectile is stuck to
        private NPC stuckNPC = null;

        // Track the initial rotation of the projectile when it sticks
        private float stuckRotation = 0f;

        // Override InstancePerEntity to return true
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            // Check if the projectile is a Flare (Flare Gun's projectile)
            if (projectile.type == ProjectileID.Flare)
            {
                // Set the projectile's timeLeft to 15 seconds (60 ticks per second)
                projectile.timeLeft = 15 * 60;
                // Allow the projectile to pierce enemies
                projectile.penetrate = 3; // Pierce up to 3 enemies
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Check if the projectile is a Flare
            if (projectile.type == ProjectileID.Flare)
            {
                // Increment the pierce counter
                pierceCount++;

                // If the projectile has pierced 3 enemies, make it stick
                if (pierceCount >= 3 && !isStuck)
                {
                    isStuck = true;
                    stuckNPC = target; // Store the NPC the projectile is stuck to
                    stuckRotation = projectile.rotation; // Store the initial rotation
                    projectile.penetrate = -1; // Make the projectile stick
                    projectile.tileCollide = false; // Disable tile collision
                    projectile.velocity = Vector2.Zero; // Stop the projectile
                    projectile.damage = 0; // Stop dealing damage

                    // Debug message
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            // Check if the projectile is a Flare
            if (projectile.type == ProjectileID.Flare)
            {
                // If the projectile is stuck to an enemy
                if (isStuck && stuckNPC != null && stuckNPC.active)
                {
                    // Update the projectile's position to follow the stuck NPC
                    projectile.Center = stuckNPC.Center;

                    // Preserve the initial rotation
                    projectile.rotation = stuckRotation;

                    // Ensure the projectile doesn't move
                    projectile.velocity = Vector2.Zero;

                    // Debug message
                    if (projectile.timeLeft % 60 == 0) // Print every second
                    {
                    }
                }

                // If the projectile's timeLeft is up, explode
                if (projectile.timeLeft <= 1)
                {
                    Explode(projectile);
                    projectile.Kill(); // Destroy the projectile after exploding

                    // Debug message
                }
            }
        }

        private void Explode(Projectile projectile)
        {
            // Define the explosion radius and damage
            int explosionRadius = 100; // Radius in pixels
            int explosionDamage = 7; // Damage value
            float explosionKnockback = 3f; // Knockback strength

            // Create an explosion effect
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                dust.velocity *= 1.4f;
            }
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
                dust.noGravity = true;
                dust.velocity *= 5f;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                dust.velocity *= 3f;
            }

            // Damage nearby NPCs
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(projectile.Center) < explosionRadius)
                {
                    // Use the updated StrikeNPC method with NPC.HitInfo
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = explosionDamage,
                        Knockback = explosionKnockback, // Add knockback
                        HitDirection = npc.Center.X < projectile.Center.X ? 1 : -1, // Direction of knockback
                        Crit = false
                    };
                    npc.StrikeNPC(hitInfo);
                }
            }

            // Damage nearby players (if you want to include players in the explosion)
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead && player.Distance(projectile.Center) < explosionRadius)
                {
                    player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI), explosionDamage, 0);
                }
            }
        }
    }
}