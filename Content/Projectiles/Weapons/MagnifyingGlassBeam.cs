// Magnifying Glass Beam - Custom projectile with Last Prism attack animation and Shadowbeam-like projectile, colored orange
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BetterThanSlimes.Content.Projectiles.Weapons
{
    public class MagnifyingGlassBeam : ModProjectile
    {
        private const int HitCooldown = 60; // One hit per NPC per second (60 ticks)

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1; // Infinite penetration
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = HitCooldown;
            Projectile.extraUpdates = 3; // Smooth beam effect like Last Prism
            Projectile.timeLeft = 300; // Beam duration
        }

        public override void AI()
        {
            // Beam direction and rotation
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            // Add bright orange lighting
            Lighting.AddLight(Projectile.Center, 1.0f, 0.5f, 0.0f);

            // Beam particle effect (optional for extra flair)
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Terraria/Images/Projectile_255").Value; // Shadowbeam texture
            Vector2 drawOrigin = new(texture.Width / 2, texture.Height / 2);
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;

            Color beamColor = new Color(255, 128, 0, 200); // Bright orange with slight transparency

            Main.EntitySpriteDraw(
                texture,
                drawPosition,
                null,
                beamColor,
                Projectile.rotation,
                drawOrigin,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            return false; // Skip default drawing
        }
    }
}
