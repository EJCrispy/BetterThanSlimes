using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class Gloom : ModPlayer
    {
        private int damageCooldown = 0;

        public override void PostUpdate()
        {
            if (IsPlayerInDarkness())
            {
                if (damageCooldown <= 0)
                {
                    int damage = 1;
                    Player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{Player.name} succumbed to the darkness."), damage, 0);
                    damageCooldown = 60; // 1 second cooldown (60 ticks)
                }
                else
                {
                    damageCooldown--;
                }
            }
            else
            {
                damageCooldown = 0; // Reset cooldown if not in darkness
            }
        }

        private bool IsPlayerInDarkness()
        {
            int playerX = (int)(Player.position.X + Player.width / 2);
            int playerY = (int)(Player.position.Y + Player.height / 2);

            var lightingColor = Lighting.GetColor(playerX / 16, playerY / 16);
            float brightness = (lightingColor.R + lightingColor.G + lightingColor.B) / 765f;
            return brightness < 0.2f;
        }
    }
}