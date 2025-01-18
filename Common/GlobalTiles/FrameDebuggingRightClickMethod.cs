using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles
{
    public class FrameDebuggingRightClickMethod : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];

            Main.NewText($"Right-clicked tile at ({i}, {j}), TileType: {tile.TileType}, FrameX: {tile.TileFrameX}, FrameY: {tile.TileFrameY}");
        }
    }
}