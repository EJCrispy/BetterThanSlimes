using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using BetterThanSlimes.Content.Items.Materials;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class SlimeDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == -8 || npc.type == NPCID.RedSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RedGel>(), 1, 1, 2)); // Drops 1-2 RedGel items
            }
        }
    }
}
