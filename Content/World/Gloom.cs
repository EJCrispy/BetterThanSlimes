using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using BetterThanSlimes;
using BetterThanSlimes.Content.Buffs; // Add this namespace to reference your custom buff

namespace BetterThanSlimes
{
    public class DarknessDamagePlayer : ModPlayer
    {
        private int darknessTimer = 0; // Tracks how long the player has been in darkness
        private int damageCooldown = 0; // Cooldown between damage ticks
        private int damageAmount = 1; // Initial damage amount
        private float zoomLevel = 2f; // Current camera zoom level (base zoom is 2x)
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

                // Apply the custom Fear debuff while in darkness
                Player.AddBuff(ModContent.BuffType<Fear>(), 1); // 1 ticks (1/60th of a second) to ensure it's reapplied continuously

                // Delay the zoom-in by 2 seconds (120 ticks)
                if (darknessTimer > 30)
                {
                    // Start the zoom-in after the delay
                    zoomDelayTimer++;

                    // Gradually zoom in the camera over 2 seconds (120 ticks)
                    if (zoomDelayTimer <= 30)
                    {
                        // Use a smooth step function for gradual zooming
                        float progress = zoomDelayTimer / 30f;
                        zoomLevel = SmoothStep(2f, 3f, progress); // Zoom from 2x to 3x
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
                        // Apply damage without immunity frames
                        Player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{Player.name} succumbed to the darkness."), damageAmount, 0, false);

                        // Disable immunity frames by setting cooldownCounter to 0
                        Player.immune = false;
                        Player.immuneTime = 0;

                        // Increase damage by 5 for the next tick
                        damageAmount += 5;

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

                // Remove the custom Fear debuff when leaving darkness
                if (Player.HasBuff(ModContent.BuffType<Full>()))
                {
                    Player.ClearBuff(ModContent.BuffType<Fear>());
                }

                // If the player was zooming in, smoothly transition to zooming out
                if (isZoomingIn || isFullyZoomedIn)
                {
                    // Calculate the progress of the zoom-out based on the outOfDarknessTimer
                    float progress = outOfDarknessTimer / 30f; // Use the same 2-second duration for zoom-out
                    zoomLevel = SmoothStep(3f, 2f, progress); // Smoothly interpolate back to 2x zoom

                    // If the zoom-out is complete, reset the zoom state
                    if (zoomLevel <= 2f)
                    {
                        isZoomingIn = false; // No longer zooming in
                        isFullyZoomedIn = false; // No longer fully zoomed in
                        zoomLevel = 2f; // Ensure the zoom level is exactly 2x
                    }
                }

                // Reset everything if the player is no longer in darkness and the zoom is back to normal
                if (zoomLevel <= 2f)
                {
                    darknessTimer = 0;
                    damageCooldown = 0;
                    zoomDelayTimer = 0; // Reset the zoom delay timer
                }

                // Reset damage ramping after 10 seconds of not being in darkness
                if (outOfDarknessTimer > 600) // 10 seconds (600 ticks)
                {
                    damageAmount = 20; // Reset damage to initial value
                }
            }

            // Apply the current zoom level to the camera (uncapped)
            Main.GameZoomTarget = zoomLevel;
            Main.GameViewMatrix.Zoom = new Vector2(zoomLevel); // Directly set the zoom level
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
            return brightness < 0.01f; // Adjusted threshold for stricter darkness (5% brightness)
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