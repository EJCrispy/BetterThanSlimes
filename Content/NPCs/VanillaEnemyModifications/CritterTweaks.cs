using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    internal class CritterTweaks : GlobalNPC
    {
        // Constants for critter flee behavior
        private const float DETECTION_RANGE = 11f * 16f; // 11 blocks in pixels (16 pixels per block)
        private const float FLEE_SPEED_MULTIPLIER = 1.5f; // Reduced multiplier to make critters slightly slower than player

        // Player movement speed is roughly 3.5-4 tiles/second normally
        // With hermes boots it's around 6.75 tiles/second (roughly 8 mph)
        // We'll aim for around 7 mph or ~5.9 tiles/second flee speed

        // List of critter types to apply behavior to
        private static readonly HashSet<int> CritterTypes = new HashSet<int>
        {
            NPCID.Bunny, NPCID.Squirrel, NPCID.SquirrelRed, NPCID.PartyBunny,
            NPCID.BunnySlimed, NPCID.BunnyXmas, NPCID.Duck, NPCID.Duck2,
            NPCID.DuckWhite, NPCID.DuckWhite2, NPCID.Grebe, NPCID.Grebe2,
            NPCID.Owl, NPCID.Penguin, NPCID.Seagull, NPCID.Goldfish,
            NPCID.GoldfishWalker,
        };

        // Dictionary to store base speeds for different critter types
        private static readonly Dictionary<int, float> BaseSpeedMap = new Dictionary<int, float>
        {
            // Ground critters
            { NPCID.Bunny, 1.0f },
            { NPCID.BunnyXmas, 1.0f },
            { NPCID.BunnySlimed, 0.8f },
            { NPCID.PartyBunny, 1.2f },
            { NPCID.Squirrel, 1.1f },
            { NPCID.SquirrelRed, 1.1f },
            
            // Birds
            { NPCID.Duck, 1.3f },
            { NPCID.Duck2, 1.3f },
            { NPCID.DuckWhite, 1.3f },
            { NPCID.DuckWhite2, 1.3f },
            { NPCID.Grebe, 1.3f },
            { NPCID.Grebe2, 1.3f },
            { NPCID.Owl, 1.4f },
            { NPCID.Seagull, 1.4f },
            
            // Other types
            { NPCID.Penguin, 0.9f },
            { NPCID.Goldfish, 1.2f },
            { NPCID.GoldfishWalker, 0.7f },
            { NPCID.SnowFlinx, 1.5f }
        };

        // Fine-tune the max speed to ensure critters are slightly slower than player
        private const float MAX_FLEE_SPEED = 5.5f; // Slightly lower than player with boots (about 7 mph)

        public override bool InstancePerEntity => false;

        public override void AI(NPC npc)
        {
            // Only apply to the critters in our list
            if (!CritterTypes.Contains(npc.type))
                return;

            // Find the nearest player
            Player nearestPlayer = null;
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    // Only consider horizontal distance for detection
                    float horizontalDistance = Math.Abs(player.Center.X - npc.Center.X);
                    float verticalDistance = Math.Abs(player.Center.Y - npc.Center.Y);

                    // Only detect if within horizontal range and not too far above/below
                    if (horizontalDistance < DETECTION_RANGE && verticalDistance < 10 * 16f) // Within 10 blocks vertically
                    {
                        float totalDistance = Vector2.Distance(npc.Center, player.Center);
                        if (totalDistance < shortestDistance)
                        {
                            shortestDistance = totalDistance;
                            nearestPlayer = player;
                        }
                    }
                }
            }

            if (nearestPlayer == null)
                return;

            // Check if player is within horizontal detection range
            float xDistance = Math.Abs(nearestPlayer.Center.X - npc.Center.X);
            if (xDistance <= DETECTION_RANGE)
            {
                // Calculate horizontal direction away from player
                float directionX = npc.Center.X - nearestPlayer.Center.X;
                float directionSign = directionX > 0 ? 1f : -1f;

                // Get the base speed for this critter type, or default to 1.0f
                float baseSpeed = 1.0f;
                if (BaseSpeedMap.ContainsKey(npc.type))
                {
                    baseSpeed = BaseSpeedMap[npc.type];
                }

                // Apply flee movement with a cap to ensure it's slightly slower than player
                float fleeSpeed = baseSpeed * FLEE_SPEED_MULTIPLIER;
                fleeSpeed = Math.Min(fleeSpeed, MAX_FLEE_SPEED);

                // Apply the speed only to X velocity
                npc.velocity.X = directionSign * fleeSpeed;

                // Make critter face the direction it's fleeing
                npc.direction = directionSign > 0 ? 1 : -1;

                // Handle natural movement behavior without vertical pushing
                HandleNaturalMovement(npc);
            }
        }

        private void HandleNaturalMovement(NPC npc)
        {
            // Handle hopping for ground-based critters (preserves natural behavior)
            if (npc.type == NPCID.Bunny || npc.type == NPCID.BunnyXmas ||
                npc.type == NPCID.BunnySlimed || npc.type == NPCID.PartyBunny ||
                npc.type == NPCID.Squirrel || npc.type == NPCID.SquirrelRed)
            {
                // If on ground and random chance, hop naturally
                if (npc.velocity.Y == 0 && Main.rand.NextBool(20))
                {
                    npc.velocity.Y = -4f; // Standard hop height
                }
            }
            // For birds, maintain their natural flying mechanics without forced upward push
            else if (npc.type == NPCID.Duck || npc.type == NPCID.Duck2 ||
                     npc.type == NPCID.DuckWhite || npc.type == NPCID.DuckWhite2 ||
                     npc.type == NPCID.Grebe || npc.type == NPCID.Grebe2 ||
                     npc.type == NPCID.Owl || npc.type == NPCID.Seagull)
            {
                // Only apply natural flapping if the bird is falling
                if (npc.velocity.Y > 0 && Main.rand.NextBool(30))
                {
                    npc.velocity.Y = -1.5f; // Just enough to simulate wing flap
                }
            }
            // For SnowFlinx, maintain jumping behavior without forced upward push
            }
        }
    }
