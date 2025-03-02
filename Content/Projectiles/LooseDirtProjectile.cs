using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BetterThanSlimes.Content.ModdedTiles;
using Terraria.DataStructures;

namespace BetterThanSlimes.Content.Projectiles
{
    public class LooseDirtProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true; // Set to false to prevent harming players by default
            Projectile.friendly = false;
            Projectile.penetrate = 8;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1; // Disable default AI to manually control acceleration
            Projectile.damage = 5; // Damage value (used when harming the player)
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 0.5f; // Set knockback to match sand (0.5f is weak knockback)
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

            // Check for player collisions manually
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead && player.Hitbox.Intersects(Projectile.Hitbox))
                {
                    // Apply damage and knockback
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, player.direction, false);

                    // Apply knockback (same as sand)
                    Vector2 knockbackDirection = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                    player.velocity += knockbackDirection * Projectile.knockBack;

                    Projectile.penetrate--;
                    if (Projectile.penetrate <= 0)
                        Projectile.Kill();
                    break; // Exit after hitting one player
                }
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

            // Check if the tile is hammered (sloped or half-brick)
            Tile targetTile = Framing.GetTileSafely(i, j);
            bool isHammered = targetTile.IsHalfBlock || targetTile.Slope != SlopeType.Solid;

            // Place the loose dirt tile if the ground is empty and not hammered
            if (!targetTile.HasTile && !isHammered)
            {
                WorldGen.PlaceTile(i, j, ModContent.TileType<LooseDirtTile>(), true, true);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, i, j, 1); // Sync the tile placement in multiplayer
                }
            }
        }
    }
}