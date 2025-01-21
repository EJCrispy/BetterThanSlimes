using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace YourModNamespace
{
    public class LineOfSightSystem : ModSystem
    {
        public override void PostDrawTiles()
        {
            Player player = Main.LocalPlayer;
            int maxDistance = 30; // Maximum raycast distance

            for (int angle = 0; angle < 360; angle += 2)
            {
                Vector2 direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(angle)), (float)Math.Sin(MathHelper.ToRadians(angle)));
                CastRay(player.Center, direction, maxDistance);
            }
        }

        private void CastRay(Vector2 origin, Vector2 direction, int maxDistance)
        {
            for (int i = 0; i < maxDistance; i++)
            {
                Vector2 position = origin + direction * (i * 16);
                int tileX = (int)(position.X / 16);
                int tileY = (int)(position.Y / 16);

                if (Main.tile[tileX, tileY].HasTile && Main.tileSolid[Main.tile[tileX, tileY].TileType])
                {
                    // If a solid tile is encountered, darken beyond it and stop ray
                    DarkenBeyond(tileX, tileY, direction, maxDistance - i * 16);
                    break;
                }
            }
        }

        private void DarkenBeyond(int startX, int startY, Vector2 direction, int remainingDistance)
        {
            for (int i = 1; i <= remainingDistance; i++)
            {
                Vector2 darkenPosition = new Vector2(startX * 16, startY * 16) + direction * (i * 16);
                int darkenX = (int)(darkenPosition.X / 16);
                int darkenY = (int)(darkenPosition.Y / 16);

                if (darkenX >= 0 && darkenX < Main.maxTilesX && darkenY >= 0 && darkenY < Main.maxTilesY)
                {
                    Lighting.AddLight(new Vector2(darkenX * 16, darkenY * 16), 0.1f, 0.1f, 0.1f);
                }
            }
        }
    }
}
