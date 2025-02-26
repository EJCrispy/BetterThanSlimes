using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using BetterThanSlimes.Content.ModdedTiles;

namespace BetterThanSlimes.Content.Projectiles;

public class LooseDirtProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = false;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.timeLeft = 300;
        Projectile.aiStyle = -1; // Disable default AI to manually control acceleration
    }

    public override void AI()
    {
        // Replicate sand projectile gravity and acceleration
        const float gravity = 0.3f; // Gravity strength (same as sand)
        const float maxFallSpeed = 10f; // Maximum fall speed (same as sand)

        // Apply gravity
        Projectile.velocity.Y += gravity;

        // Cap the fall speed
        if (Projectile.velocity.Y > maxFallSpeed)
        {
            Projectile.velocity.Y = maxFallSpeed;
        }

        // Spawn dirt particles while falling
        if (Main.rand.NextBool(3)) // Adjust the frequency of particles
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Dirt, 0f, 0f, 100, default, 0.6f); // Smaller scale (0.6f)
            dust.alpha = 128; // More transparent (0 = fully opaque, 255 = fully transparent)
        }
    }

    public override void Kill(int timeLeft)
    {
        int i = (int)(Projectile.Center.X / 16f);
        int j = (int)(Projectile.Center.Y / 16f);

        // Break grass, torches, and ambient objects
        Tile tile = Framing.GetTileSafely(i, j);
        if (tile.HasTile)
        {
            // Check for grass, torches, or other breakable tiles
            if (tile.TileType == TileID.Grass || tile.TileType == TileID.Torches || tile.TileType == TileID.Plants || tile.TileType == TileID.Plants2 || tile.TileType == TileID.Platforms)
            {
                WorldGen.KillTile(i, j); // Break the tile
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j, 1); // Sync the tile break in multiplayer
                }
            }
        }

        // Spawn dirt particles when hitting the ground
        for (int k = 0; k < 10; k++) // Adjust the number of particles
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Dirt, 0f, 0f, 100, default, 0.6f); // Smaller scale (0.6f)
            dust.alpha = 128; // More transparent (0 = fully opaque, 255 = fully transparent)
        }

        // Place the loose dirt tile if the ground is empty
        if (!Framing.GetTileSafely(i, j).HasTile)
        {
            WorldGen.PlaceTile(i, j, ModContent.TileType<LooseDirtTile>(), true, true);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, i, j, 1); // Sync the tile placement in multiplayer
            }
        }
    }
}