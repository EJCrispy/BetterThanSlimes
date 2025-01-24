using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

public class KingSlimeTweaks : GlobalNPC
{
    private bool hasSpawnedSpikes = false;
    private int jumpCounter = 0;
    private float redColorProgress = 0f; // Tracks progress of the red color fade
    private bool isRed = false; // Tracks if King Slime is currently red

    public override bool InstancePerEntity => true;

    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.KingSlime;
    }

    public override void AI(NPC npc)
    {
        // Check if the world is using the "For the Worthy" seed
        if (!Main.getGoodWorld)
        {
            return; // Exit early if not in the "For the Worthy" seed
        }

        if (npc.type == NPCID.KingSlime)
        {
            // Custom slime spawn logic
            if (Main.rand.NextBool(100)) // Adjust spawn rate if needed
            {
                Vector2 spawnVelocityNW = new Vector2(-8f, -8f); // 45 degrees northwest
                Vector2 spawnVelocityNE = new Vector2(8f, -8f);  // 45 degrees northeast
                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.BlueSlime, 0, 0f, 0f, 0f, 0f, (int)spawnVelocityNW.Length());
                NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.BlueSlime, 0, 0f, 0f, 0f, 0f, (int)spawnVelocityNE.Length());
                Main.npc[Main.npc.Length - 2].velocity = spawnVelocityNW;
                Main.npc[Main.npc.Length - 1].velocity = spawnVelocityNE;
            }

            // Gradual bright red color fade after the third jump
            if (jumpCounter >= 3 && !isRed)
            {
                redColorProgress = Math.Min(redColorProgress + 0.02f, 1f); // Increment progress, max at 1
                npc.color = Color.Lerp(Color.White, new Color(255, 50, 50), redColorProgress); // More saturated bright red
            }

            // Custom slime spike spawn logic
            if (npc.velocity.Y < 0 && !hasSpawnedSpikes) // When King Slime jumps
            {
                hasSpawnedSpikes = true;
                jumpCounter++;
                int numSpikes = 8; // Number of spikes
                for (int i = 0; i < numSpikes; i++)
                {
                    float angle = MathHelper.ToRadians(360f / numSpikes * i);
                    Vector2 spikeVelocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 6f;
                    Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, spikeVelocity, ProjectileID.SpikedSlimeSpike, 15, 0f, Main.myPlayer);
                }

                // Spawn red slimes as projectiles every 4 jumps and create an explosion
                if (jumpCounter >= 4)
                {
                    jumpCounter = 0;
                    isRed = false;
                    redColorProgress = 0f; // Reset color fade
                    npc.color = Color.White; // Revert to original color

                    // Create explosion effect
                    CreateExplosion(npc);

                    Vector2 projectileVelocityNW = new Vector2(-12f, -12f); // 45 degrees northwest, high speed
                    Vector2 projectileVelocityNE = new Vector2(12f, -12f);  // 45 degrees northeast, high speed
                    int slime1 = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.RedSlime);
                    int slime2 = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, NPCID.RedSlime);
                    Main.npc[slime1].velocity = projectileVelocityNW;
                    Main.npc[slime2].velocity = projectileVelocityNE;
                }
            }
            else if (npc.velocity.Y == 0)
            {
                hasSpawnedSpikes = false;
            }
        }
    }

    private void CreateExplosion(NPC npc)
    {
        // Explosion dust effect
        int explosionRadius = 4; // Adjust radius as needed
        for (int x = -explosionRadius; x <= explosionRadius; x++)
        {
            for (int y = -explosionRadius; y <= explosionRadius; y++)
            {
                Vector2 position = npc.Center + new Vector2(x * 16, y * 16);
                float distance = Vector2.Distance(Vector2.Zero, new Vector2(x, y));
                if (distance <= explosionRadius && Main.rand.NextBool(2))
                {
                    Dust.NewDust(position, 16, 16, DustID.Smoke, Scale: 1.5f);
                }
            }
        }

        // Explosion damage to players nearby
        foreach (Player player in Main.player)
        {
            if (player.active && !player.dead && Vector2.Distance(player.Center, npc.Center) <= explosionRadius * 16)
            {
                player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), 50, 0); // Adjust damage as necessary
            }
        }
    }
}
