using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BetterThanSlimes.Content.Projectiles;
using Terraria.DataStructures;

namespace BetterThanSlimes.Content.Systems
{
    public class LooseDirtTileSystem : ModSystem
    {
        public override void PostUpdateEverything()
        {
            // Only check tiles near players to reduce lag
            for (int p = 0; p < Main.maxPlayers; p++)
            {
                Player player = Main.player[p];
                if (player.active)
                {
                    // Define a region around the player to check for loose dirt tiles
                    int startX = (int)(player.position.X / 16) - 50;
                    int endX = (int)((player.position.X + player.width) / 16) + 50;
                    int startY = (int)(player.position.Y / 16) - 50;
                    int endY = (int)((player.position.Y + player.height) / 16) + 50;

                    // Clamp the region to world boundaries
                    startX = Utils.Clamp(startX, 0, Main.maxTilesX - 1);
                    endX = Utils.Clamp(endX, 0, Main.maxTilesX - 1);
                    startY = Utils.Clamp(startY, 0, Main.maxTilesY - 1);
                    endY = Utils.Clamp(endY, 0, Main.maxTilesY - 1);

                    // Check tiles in the defined region
                    for (int i = startX; i < endX; i++)
                    {
                        for (int j = startY; j < endY; j++)
                        {
                            Tile tile = Main.tile[i, j];
                            if (tile.HasTile && tile.TileType == ModContent.TileType<ModdedTiles.LooseDirtTile>())
                            {
                                // Check if the tile below is empty
                                Tile belowTile = Main.tile[i, j + 1];
                                if (!belowTile.HasTile)
                                {
                                    // Convert the tile into a projectile
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
                        }
                    }
                }
            }
        }
    }
}