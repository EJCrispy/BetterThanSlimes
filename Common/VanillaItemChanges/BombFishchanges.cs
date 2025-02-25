using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common
{
    public class BombHitModifier : GlobalProjectile
    {
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            // Check if the projectile is a bomb explosion
            if (projectile.type == ProjectileID.BombFish)
            {
                // Allow the bomb explosion to hit NPCs
                return true;
            }
            return base.CanHitNPC(projectile, target); // Return null to use default behavior
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            // Check if the projectile is a bomb explosion
            if (projectile.type == ProjectileID.BombFish)
            {
                // Reduce damage to 10% for NPCs
                modifiers.SourceDamage *= 0.1f;
            }
        }

        public override bool CanHitPlayer(Projectile projectile, Player target)
        {
            // Check if the projectile is a bomb explosion
            if (projectile.type == ProjectileID.BombFish)
            {
                // Allow the bomb explosion to hit players
                return true;
            }
            return base.CanHitPlayer(projectile, target); // Use default behavior for other projectiles
        }
    }
}