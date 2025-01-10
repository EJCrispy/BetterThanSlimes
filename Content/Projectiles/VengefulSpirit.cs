using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BetterThanSlimes.Content.Projectiles
{
    public class VengefulSpirit : ModProjectile
    {
        private Dictionary<int, int> npcImmunity = new Dictionary<int, int>();

        public override void SetDefaults()
        {
            Projectile.width = 24; // Frame width
            Projectile.height = 30; // Frame height
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 1600;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            float maxDetectRadius = 800f; // The maximum radius within which the projectile will detect enemies
            float accelerationFactor = 4f; // The speed at which the projectile accelerates

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

            // Set the rotation to match the projectile's direction
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            }

            // Set sprite direction based on the velocity
            Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;

            // Animation logic
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 18) // Adjust the frame delay for slower animation
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 3) // Adjust this based on the number of frames in your spritesheet
                {
                    Projectile.frame = 0;
                }
            }

            // Update the immunity frames for each NPC
            foreach (int npcIndex in npcImmunity.Keys.ToArray())
            {
                if (npcImmunity[npcIndex] > 0)
                {
                    npcImmunity[npcIndex]--;
                }
                else
                {
                    npcImmunity.Remove(npcIndex);
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // Set the immunity frames for the hit NPC
            npcImmunity[target.whoAmI] = 10; // Adjust this value as needed
        }

        public override bool? CanHitNPC(NPC target)
        {
            // Check if the NPC is immune to the projectile
            if (npcImmunity.TryGetValue(target.whoAmI, out int immunity) && immunity > 0)
            {
                return false;
            }

            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Load the texture of your projectile
            Texture2D texture = ModContent.Request<Texture2D>("BetterThanSlimes/Content/Projectiles/VengefulSpirit").Value;
            int frameHeight = 30; // Set the height to match the sprite height
            int frameWidth = texture.Width / 3; // Adjust this based on the number of frames
            Rectangle sourceRectangle = new Rectangle(Projectile.frame * frameWidth, 0, frameWidth, frameHeight);
            Vector2 origin = new Vector2(frameWidth / 2, frameHeight / 2);
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, effects, 0);
            return false;
        }
    }
}
