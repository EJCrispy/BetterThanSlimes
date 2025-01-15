using BetterThanSlimes.Content.Items;
using BetterThanSlimes.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles
{
    public class RubbleChanges : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];

            // Ensure only tiles of the desired type can be broken
            if (tile.TileType != 185) // Replace 185 with your specific tile type
            {
                Main.NewText("You cannot break this tile.");
                return; // Exit the method if the tile is not the correct type
            }

            Main.NewText($"Right-clicked tile at ({i}, {j}), TileType: {tile.TileType}, FrameX: {tile.TileFrameX}, FrameY: {tile.TileFrameY}");

            // Valid frame positions for your tile
            int[] validFrameX = { 0, 18, 36, 54, 72, 90, 1368, 1386, 1440, 1458, 1404, 1422, 108, 180, 198, 144, 162};
            int[] validFrameY = { 0, 18 };
            int[] validFrameXForWood = { 2160, 2178, 1296, 2196, 2214 };
            int[] validFrameYForWood = { 0, 18 };

            // Check if the clicked tile's frames are valid
            if (validFrameX.Contains(tile.TileFrameX) && validFrameY.Contains(tile.TileFrameY))
            {
                WorldGen.KillTile(i, j);
                    Item.NewItem(null, new Vector2(i * 16, j * 16), ModContent.ItemType<LooseStone>(), 2);
            }
            else
            {
                Main.NewText("This tile cannot be broken."); // Feedback for invalid frames
            }
            if (tile.TileType != 185) // Replace 185 with your specific tile type
            {
                Main.NewText("You cannot break this tile.");
                return; // Exit the method if the tile is not the correct type
            }

            if (validFrameXForWood.Contains(tile.TileFrameX) && validFrameYForWood.Contains(tile.TileFrameY))
            {
                WorldGen.KillTile(i, j);
                Item.NewItem(null, new Vector2(i * 16, j * 16), ModContent.ItemType<LooseStone>(), 2);
            }
            else
            {
                Main.NewText("This tile cannot be broken."); // Feedback for invalid frames
            }
            if (tile.TileType != 185) // Replace 185 with your specific tile type
            {
                Main.NewText("You cannot break this tile.");
                return; // Exit the method if the tile is not the correct type
            }
        }
    }
}