using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class ZombieBlockBreaker : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Zombie;
        }

        public override void AI(NPC npc)
        {
            base.AI(npc); // Keep original Zombie AI

        }
    }
}
