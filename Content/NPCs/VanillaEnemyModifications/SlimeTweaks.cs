using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class RedSlimeGlobalNPC : GlobalNPC
    {
        // Dictionary to store the lifetime and state of each Red Slime
        private static System.Collections.Generic.Dictionary<int, (int timer, bool isWarningPhase)> redSlimeData = new System.Collections.Generic.Dictionary<int, (int, bool)>();

        public override void AI(NPC npc)
        {
            // Check if the NPC is a Red Slime
            if (npc.netID == NPCID.RedSlime)
            {
                // Ensure the Red Slime is always aggroed on the player
                if (npc.target == -1 || !Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    // Set the target to the nearest player
                    npc.target = FindClosestPlayer(npc.Center);
                }

                // Force the Red Slime to be aggro'd
                if (npc.target >= 0 && npc.target < Main.player.Length && Main.player[npc.target].active && !Main.player[npc.target].dead)
                {
                    Player target = Main.player[npc.target];
                    npc.ai[0] = 2f; // Set the AI state to aggro'd
                    npc.ai[1] = 0f; // Reset any other AI states if necessary

                    // Generate flame-like dust particles around the slime
                    if (Main.rand.NextBool(3)) // 1/3 chance per frame
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }

                // Track the Red Slime's lifetime and state
                if (!redSlimeData.ContainsKey(npc.whoAmI))
                {
                    // Initialize the timer and warning phase state
                    redSlimeData[npc.whoAmI] = (timer: 0, isWarningPhase: false);
                }

                // Increment the timer
                var data = redSlimeData[npc.whoAmI];
                data.timer++;
                redSlimeData[npc.whoAmI] = data;

                // Check if 45 seconds (2700 ticks) have passed
                if (data.timer >= 2700 && !data.isWarningPhase)
                {
                    // Enter the warning phase
                    data.isWarningPhase = true;
                    redSlimeData[npc.whoAmI] = data;

                    // Stop the slime's movement
                    npc.velocity = Vector2.Zero;
                    npc.aiStyle = -1; // Disable default AI
                }

                // If in the warning phase, change color to orange and wait for a short time
                if (data.isWarningPhase)
                {
                    // Change the slime's color to orange
                    npc.color = Color.Orange;

                    // Wait for 60 ticks (1 second) before dying
                    if (data.timer >= 2760)
                    {
                        // Kill the Red Slime
                        npc.StrikeInstantKill();

                        // Remove the data entry
                        redSlimeData.Remove(npc.whoAmI);
                    }
                }
            }
        }

        private int FindClosestPlayer(Vector2 position)
        {
            int closestPlayer = -1;
            float closestDistance = float.MaxValue;
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    float distance = Vector2.Distance(position, player.Center);
                    if (distance < closestDistance)
                    {
                        closestPlayer = player.whoAmI;
                        closestDistance = distance;
                    }
                }
            }
            return closestPlayer;
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            // Check if the NPC is a Red Slime
            if (npc.netID == NPCID.RedSlime)
            {
                // Inflict OnFire! debuff on the player
                target.AddBuff(BuffID.OnFire, 150); // 300 ticks = 5 seconds
            }
        }

        public override void OnKill(NPC npc)
        {
            // Check if the NPC is a Red Slime
            if (npc.netID == NPCID.RedSlime)
            {
                // Remove the data entry if the Red Slime dies naturally
                if (redSlimeData.ContainsKey(npc.whoAmI))
                {
                    redSlimeData.Remove(npc.whoAmI);
                }

                // Check if the Red Slime's health was above 1 when it died
                if (npc.life > 1)
                {
                    // Spawn a bomb at the slime's position
                    int bombType = ProjectileID.Bomb; // Use the standard bomb projectile
                    Vector2 spawnPosition = npc.Center; // Spawn at the slime's center

                    // Spawn the bomb projectile
                    int bombIndex = Projectile.NewProjectile(
                        npc.GetSource_Death(), // Source of the projectile (the slime's death)
                        spawnPosition,        // Position to spawn the bomb
                        Vector2.Zero,         // Initial velocity (zero for stationary spawn)
                        bombType,             // Projectile type (bomb)
                        30,                   // Damage (adjust as needed)
                        3f,                   // Knockback (adjust as needed)
                        Main.myPlayer         // Owner (the player who triggered the death)
                    );

                    // Make the bomb explode instantly
                    if (bombIndex != Main.maxProjectiles && Main.projectile[bombIndex] != null)
                    {
                        Main.projectile[bombIndex].timeLeft = 1; // Set timeLeft to 1 to make it explode instantly
                    }

                    // Optional: Add some visual or sound effects for flavor
                    SoundEngine.PlaySound(SoundID.NPCDeath1, npc.Center); // Play a death sound
                    for (int i = 0; i < 10; i++) // Spawn some dust for effect
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), 150, Color.OrangeRed, 1.5f);
                    }
                }
            }
        }
    }
}