using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class SpawnPool : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            // Check if the Worm critter is in the spawn pool
            if (pool.ContainsKey(NPCID.Worm))
            {
                // Remove the rain condition for the Worm critter
                // This allows it to spawn at any time
                pool[NPCID.Worm] = 1f; // Set spawn chance to 100% (or adjust as needed)
            }
        }
    }
}