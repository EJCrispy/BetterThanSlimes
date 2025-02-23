using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class EnemyReplacement : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            // Apply this GlobalNPC only to the Worm critter (NPC ID 357)
            return npc.type == NPCID.Worm;
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            // Replace the Worm critter with the Giant Worm enemy (NPC ID 10)
            int giantWorm = NPCID.GiantWormHead;
            NPC.NewNPC(source, (int)npc.position.X, (int)npc.position.Y, giantWorm);

            // Despawn the original Worm critter
            npc.active = false;
        }
    }
}