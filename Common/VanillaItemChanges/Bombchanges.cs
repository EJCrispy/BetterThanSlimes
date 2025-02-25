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
            if (projectile.type == ProjectileID.Bomb)
            {
                // Prevent the bomb explosion from hitting NPCs
                return false;
            }
            return base.CanHitNPC(projectile, target); // Return null to use default behavior
        }

        public override bool CanHitPlayer(Projectile projectile, Player target)
        {
            // Check if the projectile is a bomb explosion
            if (projectile.type == ProjectileID.Bomb)
            {
                // Allow the bomb explosion to hit players
                return true;
            }
            return base.CanHitPlayer(projectile, target); // Use default behavior for other projectiles
        }
    }
}