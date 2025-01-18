using BetterThanSlimes.Content.Items;
using Microsoft.Xna.Framework;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles
{
    public class BranchChanges : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];
            // Valid frame positions for your tile

            if (tile.TileType == 5)
            {

                if (tile.TileFrameX == 44 && tile.TileFrameY == 198 || tile.TileFrameX == 44 && tile.TileFrameY == 220 || tile.TileFrameX == 44 && tile.TileFrameY == 242 || tile.TileFrameX == 66 && tile.TileFrameY == 220 || tile.TileFrameX == 88 && tile.TileFrameY == 220 || tile.TileFrameX == 66 && tile.TileFrameY == 22 || tile.TileFrameX == 88 && tile.TileFrameY == 88 || tile.TileFrameX == 88 && tile.TileFrameY == 110 || tile.TileFrameX == 66 && tile.TileFrameY == 0 || tile.TileFrameX == 66 && tile.TileFrameY == 22 || tile.TileFrameX == 66 && tile.TileFrameY == 242 || tile.TileFrameX == 66 && tile.TileFrameY == 198 || tile.TileFrameX == 66 && tile.TileFrameY == 44 || tile.TileFrameX == 88 && tile.TileFrameY == 66)
                {

                    WorldGen.KillTile(i, j);
                    Item.NewItem(null, new Vector2(i * 16, j * 16), ModContent.ItemType<Twig>());

                }
                else
                {
                    return;
                }
            }
        }

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Tile tile = Main.tile[i, j];

            // Prevent default item drop for wood
            if (tile.TileType == 5) // Replace 5 with your specific wood tile type
            {
                if (tile.TileFrameX == 44 && tile.TileFrameY == 198 || tile.TileFrameX == 44 && tile.TileFrameY == 220 || tile.TileFrameX == 44 && tile.TileFrameY == 242 || tile.TileFrameX == 66 && tile.TileFrameY == 220 || tile.TileFrameX == 88 && tile.TileFrameY == 220 || tile.TileFrameX == 66 && tile.TileFrameY == 22 || tile.TileFrameX == 88 && tile.TileFrameY == 88 || tile.TileFrameX == 88 && tile.TileFrameY == 110 || tile.TileFrameX == 66 && tile.TileFrameY == 0 || tile.TileFrameX == 66 && tile.TileFrameY == 22 || tile.TileFrameX == 66 && tile.TileFrameY == 242 || tile.TileFrameX == 66 && tile.TileFrameY == 198 || tile.TileFrameX == 66 && tile.TileFrameY == 44 || tile.TileFrameX == 88 && tile.TileFrameY == 66)
                {
                    noItem = true; // Prevent items from dropping

                }
                else
                {
                    noItem = false;

                }
            }
        }
    }
}

