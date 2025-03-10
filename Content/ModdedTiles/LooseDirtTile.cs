﻿using Terraria;
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
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
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
                        new Vector2(0f, 3.3f), // Increased downward velocity for faster fall
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
                    // Check if there is any nearby LooseDirtTile
                    bool hasNearbyLooseDirt = false;
                    for (int l = 0; l < 4; l++)
                    {
                        int checkX = x + offsetsX[l];
                        int checkY = y + offsetsY[l];
                        Tile checkTile = Framing.GetTileSafely(checkX, checkY);
                        if (checkTile.HasTile && checkTile.TileType == ModContent.TileType<LooseDirtTile>())
                        {
                            hasNearbyLooseDirt = true;
                            break;
                        }
                    }

                    // Only convert to LooseDirtTile if there is no nearby LooseDirtTile
                    if (!hasNearbyLooseDirt)
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

        public override void NearbyEffects(int i, int j, bool closer)
        {
            // Check if the player is inside the tile
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player player = Main.player[k];
                if (player.active && !player.dead)
                {
                    // Calculate the tile's world position
                    Vector2 tilePosition = new Vector2(i * 16, j * 16);

                    // Check if the player is inside the tile's bounds
                    if (player.Hitbox.Intersects(new Rectangle((int)tilePosition.X, (int)tilePosition.Y, 16, 16)))
                    {
                        // Apply the Suffocation debuff
                        player.AddBuff(BuffID.Suffocation, 10); // 60 ticks = 1 second
                    }
                }
            }
        }
    }
}