using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace BetterThanSlimes.Content.NPCs
{
    public class RedSlimeSpawnGlobalNPC : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            // Check if the player is in the Forest biome and it's nighttime
            if (spawnInfo.Player.ZoneForest && !Main.dayTime)
            {
                // Add Red Slime to the spawn pool with a 10% chance (adjust as needed)
                if (!pool.ContainsKey(NPCID.RedSlime))
                {
                    pool.Add(NPCID.RedSlime, 0.5f); // 10% chance to spawn
                }
            }
        }
    }
}