using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes
{
    public class DarknessDamagePlayer : ModPlayer
    {
        private int darknessTimer = 0; // Tracks how long the player has been in darkness
        private int damageCooldown = 0; // Cooldown between damage ticks
        private int damageAmount = 1; // Initial damage amount
        private float zoomLevel = 1f; // Current camera zoom level

        public override void PostUpdate()
        {
            if (IsPlayerInDarkness())
            {
                // Increment the darkness timer
                darknessTimer++;

                // Gradually zoom in the camera over 5 seconds (300 ticks)
                if (darknessTimer <= 300)
                {
                    zoomLevel = MathHelper.Lerp(1f, 2f, darknessTimer / 300f); // Zoom from 1x to 2x
                    Main.GameZoomTarget = zoomLevel;
                }

                // After 5 seconds, start dealing damage
                if (darknessTimer > 300)
                {
                    if (damageCooldown <= 0)
                    {
                        // Apply damage
                        Player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{Player.name} succumbed to the darkness."), damageAmount, 0);

                        // Increase damage by 3 for the next tick
                        damageAmount += 3;

                        // Reset cooldown (e.g., 1 second cooldown)
                        damageCooldown = 60;
                    }
                    else
                    {
                        damageCooldown--;
                    }
                }
            }
            else
            {
                // Reset everything if the player is no longer in darkness
                darknessTimer = 0;
                damageCooldown = 0;
                damageAmount = 1;
                zoomLevel = 1f;
                Main.GameZoomTarget = 1f; // Reset camera zoom
            }
        }

        private bool IsPlayerInDarkness()
        {
            // Get the player's position in world coordinates
            int playerX = (int)(Player.position.X + Player.width / 2);
            int playerY = (int)(Player.position.Y + Player.height / 2);

            // Get the lighting color at the player's position
            var lightingColor = Lighting.GetColor(playerX / 16, playerY / 16);

            // Check if the light level is below a certain threshold (e.g., very dark)
            float brightness = (lightingColor.R + lightingColor.G + lightingColor.B) / 765f; // 765 = 255 * 3
            return brightness < 0.2f; // Adjust this threshold as needed
        }
    }
}