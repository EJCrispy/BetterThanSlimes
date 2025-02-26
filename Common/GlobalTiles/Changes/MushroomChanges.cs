using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles.Changes
{
    public class MushroomChanges : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];

            // Ensure only tiles of the desired type can be broken
            if (tile.TileType == 3 || tile.TileType == 82 || tile.TileType == 83)
            {
                WorldGen.KillTile(i, j);
            }

        }
    }
}
