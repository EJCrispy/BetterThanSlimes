using BetterThanSlimes.Content.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.GlobalTiles
{
    public class DirtTileGlobal : GlobalTile
    {
        // A flag to prevent recursive calls
        private bool isConvertingNeighbors = false;

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            // Only proceed if the broken tile is a vanilla dirt tile, the break is successful, and we're not already converting neighbors
            if (type == TileID.Dirt && !fail && !effectOnly && !isConvertingNeighbors)
            {
                // Prevent the default dirt drop
                noItem = true;

                // Set the flag to prevent recursive calls
                isConvertingNeighbors = true;

                // Convert neighboring dirt tiles to loose dirt
                ConvertNeighborsToLooseDirt(i, j);

                // Spawn the LooseDirtItem at the broken tile's position
                if (Main.netMode != NetmodeID.MultiplayerClient) // Ensure this only runs on the server or single-player
                {
                    Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<LooseDirtItem>());
                }

                // Reset the flag after conversion is complete
                isConvertingNeighbors = false;
            }
        }

        private void ConvertNeighborsToLooseDirt(int i, int j)
        {
            // Define the four orthogonal directions (left, right, above, below)
            int[] offsetsX = { -1, 1, 0, 0 };
            int[] offsetsY = { 0, 0, -1, 1 };

            for (int k = 0; k < 4; k++)
            {
                int x = i + offsetsX[k];
                int y = j + offsetsY[k];

                // Safely get the tile at the neighboring position
                Tile tile = Framing.GetTileSafely(x, y);

                // Check if the tile exists and is a vanilla dirt tile (ignore loose dirt tiles)
                if (tile.HasTile && tile.TileType == TileID.Dirt)
                {
                    // Break the dirt tile
                    WorldGen.KillTile(x, y, false, false, true); // Set 'noItem' to true to prevent drops

                    // Place a loose dirt tile in its place
                    WorldGen.PlaceTile(x, y, ModContent.TileType<ModdedTiles.LooseDirtTile>(), true, true);

                    // Sync the tile change in multiplayer
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 1);
                    }
                }
            }
        }
    }
}