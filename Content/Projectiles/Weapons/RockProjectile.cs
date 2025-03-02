using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

//This section of code codes for the Loose stone projectile's behavior and attributes.

namespace BetterThanSlimes.Content.Projectiles.Weapons
{
    public class RockProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 1200;
            Projectile.aiStyle = 2;

        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.
            for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                dust.noGravity = false;
                dust.velocity *= 1.5f;
                dust.scale *= 0.9f;
            }
        }


    }
}
