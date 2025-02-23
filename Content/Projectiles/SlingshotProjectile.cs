using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Projectiles
{
    public class SlingshotProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; // Single-hit projectile
            Projectile.timeLeft = 600;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bone;
        }

        public override void AI()
        {
            // Gravity effect
            Projectile.velocity.Y += 0.2f;

            // Rotate to match movement
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            // Dust effect on death
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                dust.noGravity = false;
                dust.velocity *= 1.5f;
                dust.scale *= 0.9f;
            }
        }
    }
}
