using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BetterThanSlimes.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class SlimeDrops : GlobalNPC
    {
        public bool isRedSlime = false;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            if (npc.type == NPCID.Pinky)
            {
                isRedSlime = true;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (isRedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RedGel>(), 1, 1, 2)); // Drops 1-2 RedGel items
            }
        }
    }
}
