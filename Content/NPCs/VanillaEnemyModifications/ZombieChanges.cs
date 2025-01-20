using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Common.NPCs
{
    public class ZombieChanges : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            // Check if the NPC is a zombie
            if (npc.type == NPCID.Zombie) // Add more zombie types if needed
            {
                // Check for the tile type (Loose Dirt, TileID.Dirt is used as an example)
                int i = (int)(npc.Center.X / 16);
                int j = (int)(npc.Center.Y / 16);

                Tile tile = Main.tile[i, j];

                // Check if the tile is loose dirt
                if (tile.TileType == TileID.Dirt)  // Replace with your custom Loose Dirt tile ID if needed
                {
                    // Check if the zombie is close to the tile
                    if (Vector2.Distance(npc.Center, new Vector2(i * 16, j * 16)) < 50f) // 50 pixels range for the zombie
                    {
                        // Handle breaking the tile with a delay (5 seconds per hit)
                        if (npc.ai[0] == 0f) // First time it "hits"
                        {
                            npc.ai[0] = 1f;
                            npc.ai[1] = (float)(Main.time + 300); // 300 frames (5 seconds)
                        }
                        else if (npc.ai[0] == 1f && Main.time >= npc.ai[1]) // Once 5 seconds have passed
                        {
                            WorldGen.KillTile(i, j); // Break the tile
                            npc.ai[0] = 2f;
                        }
                        else if (npc.ai[0] == 2f) // After breaking the tile, check if we should hit again
                        {
                            npc.ai[0] = 0f; // Reset to hit again after 5 seconds
                        }
                    }
                }
            }
        }
    }
}

