using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

public class HighSpeedCollisionSystem : ModSystem
{
    private const float HorizontalSpeedThreshold = 13f; // Approximate Shield of Cthulhu dash speed

    public override void PostUpdatePlayers()
    {
        foreach (Player player in Main.player)
        {
            if (player.active && !player.dead && Math.Abs(player.velocity.X) >= HorizontalSpeedThreshold)
            {
                Vector2 nextPosition = player.position + new Vector2(player.velocity.X, 0); // Check only horizontal movement

                // Check for collision with solid tiles horizontally
                if (Collision.SolidCollision(nextPosition, player.width, player.height))
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} hit a fat splat."), player.statLife + 1, 0);
                }
            }
        }
    }
}
