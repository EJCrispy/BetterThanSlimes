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
                // Explosion parameters
                int explosionRadius = 48; // Radius in pixels (adjust as needed)
                Vector2 explosionCenter = npc.Center;

                // Create dust effects for the explosion
                for (int i = 0; i < 100; i++) // Increased dust count for better visibility
                {
                    Vector2 dustPosition = explosionCenter + Main.rand.NextVector2Circular(explosionRadius, explosionRadius);
                    Dust dust = Dust.NewDustPerfect(dustPosition, DustID.Torch, Main.rand.NextVector2Circular(3f, 3f), 150, Color.OrangeRed, 2f);
                    dust.noGravity = true; // Make the dust float
                    dust.fadeIn = 1.5f; // Add a fade-in effect
                }

                // Add gore for larger visual chunks
                for (int i = 0; i < 5; i++)
                {
                    Vector2 goreVelocity = Main.rand.NextVector2Circular(5f, 5f);
                    Gore.NewGore(npc.GetSource_Death(), explosionCenter, goreVelocity, GoreID.Smoke1);
                }

                // Damage players in the explosion radius
                foreach (Player player in Main.player)
                {
                    if (player.active && !player.dead)
                    {
                        float distanceToPlayer = Vector2.Distance(player.Center, explosionCenter);
                        if (distanceToPlayer <= explosionRadius)
                        {
                            // Damage the player
                            int explosionDamage = 30; // Adjust damage as necessary
                            player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), explosionDamage, 3);
                        }
                    }
                }

                // Destroy blocks in the explosion radius (add this functionality)
                for (int x = (int)(explosionCenter.X - explosionRadius); x < (int)(explosionCenter.X + explosionRadius); x++)
                {
                    for (int y = (int)(explosionCenter.Y - explosionRadius); y < (int)(explosionCenter.Y + explosionRadius); y++)
                    {
                        // Check if the coordinates are within valid tile bounds
                        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
                        {
                            Vector2 blockPosition = new Vector2(x, y);
                            if (Vector2.Distance(blockPosition, explosionCenter) <= explosionRadius)
                            {
                                // Destroy the block (check if it's not a tile that's immune to destruction)
                                if (Main.tile[x, y] != null && !Main.tile[x, y].HasTile)
                                {
                                    WorldGen.KillTile(x, y); // This will destroy the block
                                }
                            }
                        }
                    }
                }

                // Play explosion sound
                SoundEngine.PlaySound(SoundID.Item14, explosionCenter);

                // Create a screen shake effect for added impact (optional)
                if (Main.netMode != NetmodeID.Server)
                {
                    // Implement screen shake here if desired
                }
            }
        }
    }
}