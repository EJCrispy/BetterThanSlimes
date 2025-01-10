using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace BetterThanSlimes.Content.Projectiles
{
    public class VengefulSpirit : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            float maxDetectRadius = 400f; // The maximum radius within which the projectile will detect enemies
            float speed = 4f; // The speed at which the projectile will move towards the target
            float accelerationFactor = 2f; // The speed at which the projectile accelerates

            NPC closestNPC = null;
            float closestDist = float.MaxValue;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(this) && !npc.friendly)
                {
                    float dist = Vector2.Distance(npc.Center, Projectile.Center);
                    if (dist < maxDetectRadius && dist < closestDist)
                    {
                        closestDist = dist;
                        closestNPC = npc;
                    }
                }
            }

            if (closestNPC != null)
            {
                Vector2 direction = closestNPC.Center - Projectile.Center;
                direction.Normalize();
                direction *= speed;
                Projectile.velocity = (Projectile.velocity * (20f - 1f) + direction) / 20f;
            }
        }
    }
}
