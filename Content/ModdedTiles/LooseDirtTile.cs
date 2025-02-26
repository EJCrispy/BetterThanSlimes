using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.DataStructures;
using BetterThanSlimes.Content.Projectiles;

namespace BetterThanSlimes.Content.ModdedTiles
{
    public class LooseDirtTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(151, 107, 75), Language.GetText("Loose Dirt"));

            DustType = DustID.Dirt;
            MineResist = 0.25f;
        }

        public override bool CanDrop(int i, int j)
        {
            // Return false to prevent dropping any item when the tile is broken
            return false;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            base.PlaceInWorld(i, j, item);

            // Check if the tile below is empty and convert to projectile if necessary
            Tile belowTile = Framing.GetTileSafely(i, j + 1);
            if (!belowTile.HasTile)
            {
                WorldGen.KillTile(i, j);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(
                        new EntitySource_TileBreak(i, j),
                        new Vector2(i * 16 + 8, j * 16 + 8),
                        new Vector2(0f, 5.8f), // Increased downward velocity for faster fall
                        ModContent.ProjectileType<LooseDirtProjectile>(),
                        0,
                        0f
                    );
                }
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail && !effectOnly)
            {
                // Convert neighboring tiles to loose dirt
                ConvertNeighborsToLooseDirt(i, j);
            }
        }

        private void ConvertNeighborsToLooseDirt(int i, int j)
        {
            // Check the four orthogonal directions
            int[] offsetsX = { -1, 1, 0, 0 };
            int[] offsetsY = { 0, 0, -1, 1 };

            for (int k = 0; k < 4; k++)
            {
                int x = i + offsetsX[k];
                int y = j + offsetsY[k];

                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.HasTile && tile.TileType == TileID.Dirt)
                {
                    WorldGen.KillTile(x, y); // Break the dirt tile
                    WorldGen.PlaceTile(x, y, ModContent.TileType<LooseDirtTile>(), true, true); // Place loose dirt
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 1); // Sync the tile change in multiplayer
                    }
                }
            }
        }
    }
}