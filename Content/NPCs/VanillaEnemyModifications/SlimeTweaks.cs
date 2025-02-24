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
        // Dictionary to store the lifetime of each Red Slime
        private static System.Collections.Generic.Dictionary<int, int> redSlimeTimers = new System.Collections.Generic.Dictionary<int, int>();

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

                // Track the Red Slime's lifetime
                if (!redSlimeTimers.ContainsKey(npc.whoAmI))
                {
                    // Initialize the timer if it doesn't exist
                    redSlimeTimers[npc.whoAmI] = 0;
                }

                // Increment the timer
                redSlimeTimers[npc.whoAmI]++;

                // Check if 45 seconds (2700 ticks) have passed
                if (redSlimeTimers[npc.whoAmI] >= 2700)
                {
                    // Kill the Red Slime
                    npc.StrikeInstantKill();

                    // Remove the timer entry
                    redSlimeTimers.Remove(npc.whoAmI);
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
                // Remove the timer entry if the Red Slime dies naturally
                if (redSlimeTimers.ContainsKey(npc.whoAmI))
                {
                    redSlimeTimers.Remove(npc.whoAmI);
                }

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