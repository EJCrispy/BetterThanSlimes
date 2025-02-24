using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

public class HighSpeedCollisionSystem : ModSystem
{
    private const float HorizontalSpeedThreshold = 10f; // Minimum speed to start considering damage

    public override void PostUpdatePlayers()
    {
        foreach (Player player in Main.player)
        {
            if (player.active && !player.dead)
            {
                float horizontalSpeed = Math.Abs(player.velocity.X) * 10f; // Convert Terraria units to mph approximation

                if (horizontalSpeed >= HorizontalSpeedThreshold)
                {
                    Vector2 nextPosition = player.position + new Vector2(player.velocity.X, 0); // Check only horizontal movement

                    // Check for collision with solid tiles horizontally
                    if (Collision.SolidCollision(nextPosition, player.width, player.height))
                    {
                        int damage = CalculateCollisionDamage(horizontalSpeed);

                        // Reduce damage to 1/4 if player has an active hook
                        if (player.grappling[0] != -1)
                        {
                            damage = (int)(damage / 1f);
                        }

                        if (damage > 0)
                        {
                            player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} slammed into a wall."), damage, Math.Sign(player.velocity.X));
                        }
                    }
                }
            }
        }
    }

    private int CalculateCollisionDamage(float mph)
    {
        if (mph < 30) return 0;
        else if (mph > 35) return 10;
        else if (mph > 40) return 20;
        else if (mph > 50) return 30;
        else if (mph > 100) return 40;
        else if (mph == 100) return 50;
        else return (int)mph; // Above 100 mph, damage equals the mph value
    }
}
