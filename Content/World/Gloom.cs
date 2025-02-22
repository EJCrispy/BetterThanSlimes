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
        private int outOfDarknessTimer = 0; // Tracks how long the player has been out of darkness

        public override void PostUpdate()
        {
            if (IsPlayerInDarkness())
            {
                // Reset the out-of-darkness timer
                outOfDarknessTimer = 0;

                // Increment the darkness timer
                darknessTimer++;

                // Gradually zoom in the camera over 5 seconds (300 ticks)
                if (darknessTimer <= 300)
                {
                    // Use a smooth step function for gradual zooming
                    float progress = darknessTimer / 300f;
                    zoomLevel = SmoothStep(1f, 2f, progress); // Zoom from 1x to 2x
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
                // Increment the out-of-darkness timer
                outOfDarknessTimer++;

                // Gradually zoom out the camera if the player is no longer in darkness
                if (zoomLevel > 1f)
                {
                    float progress = outOfDarknessTimer / 300f; // Use the same 5-second duration for zoom-out
                    zoomLevel = SmoothStep(2f, 1f, progress); // Smoothly interpolate back to 1x zoom
                }

                // Reset everything if the player is no longer in darkness and the zoom is back to normal
                if (zoomLevel <= 1f)
                {
                    darknessTimer = 0;
                    damageCooldown = 0;
                    zoomLevel = 1f;
                }

                // Reset damage ramping after 10 seconds of not being in darkness
                if (outOfDarknessTimer > 600) // 10 seconds (600 ticks)
                {
                    damageAmount = 1; // Reset damage to initial value
                }
            }

            // Apply the current zoom level to the camera
            Main.GameZoomTarget = zoomLevel;
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

        // SmoothStep function for smoother interpolation
        private float SmoothStep(float start, float end, float amount)
        {
            amount = MathHelper.Clamp(amount, 0f, 1f);
            amount = amount * amount * (3f - 2f * amount); // SmoothStep formula
            return MathHelper.Lerp(start, end, amount);
        }
    }
}