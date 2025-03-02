using BetterThanSlimes.Content.Items;
using BetterThanSlimes.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles.Changes
{
    public class RubbleChanges : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];

            // Ensure only tiles of the desired type can be broken
            if (tile.TileType != 185) // Replace 185 with your specific tile type
            {
                return; // Exit the method if the tile is not the correct type
            }

            // Valid frame positions for your tile
            int[] validFrameXFor1x1 = [0, 18, 36, 54, 72, 90];
            int[] validFrameYFor1x1 = [0];
            int[] validFrameXFor2x1 = [0, 18, 36, 54, 72, 90, 1368, 1386, 1440, 1458, 1404, 1422];
            int[] validFrameYFor2x1 = [18];

            // Check if the clicked tile's frames are valid
            if (validFrameXFor1x1.Contains(tile.TileFrameX) && validFrameYFor1x1.Contains(tile.TileFrameY))
            {
                WorldGen.KillTile(i, j);
            }
            else if (validFrameXFor2x1.Contains(tile.TileFrameX) && validFrameYFor2x1.Contains(tile.TileFrameY))
            {
                WorldGen.KillTile(i, j);
            }
        }

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            // Ensure only tiles of the desired type can be broken
            if (type != 185) // Replace 185 with your specific tile type
            {
                return; // Exit the method if the tile is not the correct type
            }

            Tile tile = Main.tile[i, j];

            // Valid frame positions for your tile
            int[] validFrameXFor1x1 = [0, 18, 36, 54, 72, 90];
            int[] validFrameYFor1x1 = [0];
            int[] validFrameXFor2x1 = [0, 18, 36, 54, 72, 90, 1368, 1386, 1440, 1458, 1404, 1422];
            int[] validFrameYFor2x1 = [18];

            // Check if the tile's frames are valid
            if (validFrameXFor1x1.Contains(tile.TileFrameX) && validFrameYFor1x1.Contains(tile.TileFrameY))
            {
                Item.NewItem(null, new Vector2(i * 16 + 8, j * 16), ModContent.ItemType<Rock>()); // Spawn 1 Loose Stone
            }
            else if (validFrameXFor2x1.Contains(tile.TileFrameX) && validFrameYFor2x1.Contains(tile.TileFrameY))
            {
                Item.NewItem(null, new Vector2(i * 16 + 8, j * 16), ModContent.ItemType<Rock>()); // Spawn 2 Loose Stones
            }
        }

        public override void MouseOver(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];

            // Ensure only tiles of the desired type can be interacted with
            if (tile.TileType != 185) // Replace 185 with your specific tile type
            {
                return; // Exit the method if the tile is not the correct type
            }

            // Valid frame positions for your tile
            int[] validFrameXFor1x1 = [0, 18, 36, 54, 72, 90];
            int[] validFrameYFor1x1 = [0];
            int[] validFrameXFor2x1 = [0, 18, 36, 54, 72, 90, 1368, 1386, 1440, 1458, 1404, 1422];
            int[] validFrameYFor2x1 = [18];

            // Check if the tile's frames are valid
            if ((validFrameXFor1x1.Contains(tile.TileFrameX) && validFrameYFor1x1.Contains(tile.TileFrameY)) ||
                (validFrameXFor2x1.Contains(tile.TileFrameX) && validFrameYFor2x1.Contains(tile.TileFrameY)))
            {
                // Set the cursor to the inventory cursor
                Main.cursorOverride = 2; // 2 represents the inventory cursor

                // Draw the LooseStone sprite at a larger scale
                Main.HoverItem = new Item(ModContent.ItemType<Rock>());
                Main.instance.LoadItem(ModContent.ItemType<Rock>());
                Texture2D texture = Terraria.GameContent.TextureAssets.Item[ModContent.ItemType<Rock>()].Value;
                Vector2 origin = texture.Size() / 2f;
                Vector2 position = Main.MouseScreen + new Vector2(26f, 26f); 
            }
        }
    }
}