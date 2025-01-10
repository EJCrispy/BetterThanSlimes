using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Projectile.penetrate = 4;
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
                direction *= accelerationFactor;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction, 0.1f);
            }

            // Animation logic
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5) // Adjust the frame delay as needed
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 4) // Adjust this based on the number of frames in your spritesheet
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Load the texture of your projectile
            Texture2D texture = ModContent.Request<Texture2D>("BetterThanSlimes/Content/Projectiles/VengefulSpirit").Value;
            int frameHeight = texture.Height / 4; // Adjust this based on the number of frames
            int frameWidth = texture.Width; // Adjust this if frames are aligned horizontally
            Rectangle sourceRectangle = new Rectangle(0, Projectile.frame * frameHeight, frameWidth, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, effects, 0);
            return false;
        }
    }
}
