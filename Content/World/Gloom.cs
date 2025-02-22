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
        private int zoomDelayTimer = 0; // Tracks how long the player has been in darkness before zooming starts
        private bool isZoomingIn = false; // Tracks whether the camera is currently zooming in
        private bool isFullyZoomedIn = false; // Tracks whether the camera has fully zoomed in

        public override void PostUpdate()
        {
            if (IsPlayerInDarkness())
            {
                // Reset the out-of-darkness timer
                outOfDarknessTimer = 0;

                // Increment the darkness timer
                darknessTimer++;

                // Delay the zoom-in by 4 seconds (240 ticks)
                if (darknessTimer > 240)
                {
                    // Start the zoom-in after the delay
                    zoomDelayTimer++;

                    // Gradually zoom in the camera over 4 seconds (240 ticks)
                    if (zoomDelayTimer <= 240)
                    {
                        // Use a smooth step function for gradual zooming
                        float progress = zoomDelayTimer / 240f;
                        zoomLevel = SmoothStep(1f, 2f, progress); // Zoom from 1x to 2x
                        isZoomingIn = true; // Mark that we're zooming in
                    }
                    else
                    {
                        // Camera has fully zoomed in
                        isFullyZoomedIn = true;
                        isZoomingIn = false;
                    }
                }

                // Start dealing damage only after the camera has fully zoomed in
                if (isFullyZoomedIn)
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

                // If the player was zooming in, smoothly transition to zooming out
                if (isZoomingIn || isFullyZoomedIn)
                {
                    // Calculate the progress of the zoom-out based on the outOfDarknessTimer
                    float progress = outOfDarknessTimer / 240f; // Use the same 4-second duration for zoom-out
                    zoomLevel = SmoothStep(2f, 1f, progress); // Smoothly interpolate back to 1x zoom

                    // If the zoom-out is complete, reset the zoom state
                    if (zoomLevel <= 1f)
                    {
                        isZoomingIn = false; // No longer zooming in
                        isFullyZoomedIn = false; // No longer fully zoomed in
                        zoomLevel = 1f; // Ensure the zoom level is exactly 1x
                    }
                }

                // Reset everything if the player is no longer in darkness and the zoom is back to normal
                if (zoomLevel <= 1f)
                {
                    darknessTimer = 0;
                    damageCooldown = 0;
                    zoomDelayTimer = 0; // Reset the zoom delay timer
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
            return brightness < 0.1f; // Adjusted threshold for stricter darkness (10% brightness)
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