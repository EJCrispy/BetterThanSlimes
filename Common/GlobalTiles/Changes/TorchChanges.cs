using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace BetterThanSlimes.Common.GlobalTiles.Changes
{
    internal class TorchChanges : GlobalTile
    {
        // Dictionary to keep track of when torches were placed
        private static Dictionary<Point16, int> torchPlacementTimes = new Dictionary<Point16, int>();

        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            // Check if the placed tile is a torch
            if (type == TileID.Torches)
            {
                // Record the time the torch was placed
                torchPlacementTimes[new Point16(i, j)] = (int)Main.time;
            }
        }

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            // Check if the broken tile is a torch
            if (type == TileID.Torches)
            {
                // Prevent the torch from dropping itself
                noItem = true;
            }
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            // Iterate through all recorded torches
            foreach (var torch in torchPlacementTimes.ToList())
            {
                int elapsedTime = (int)Main.time - torch.Value;

                // Check if 30 minutes (1800 seconds) have passed
                if (elapsedTime >= 1200 * 60) // Change to 1800 * 60 for 30 minutes
                {
                    // Get the tile at the torch's position
                    Tile tile = Main.tile[torch.Key.X, torch.Key.Y];

                    // Check if the tile is still a torch
                    if (tile.HasTile && tile.TileType == TileID.Torches)
                    {
                        // Set the torch to its deactivated state by modifying its frameX
                        tile.TileFrameX += 66; // Adjust frameX to the deactivated state
                    }

                    // Remove the torch from the tracking dictionary
                    torchPlacementTimes.Remove(torch.Key);
                }
            }
        }
    }
}