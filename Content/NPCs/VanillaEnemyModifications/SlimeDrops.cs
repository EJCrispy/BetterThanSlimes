using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BetterThanSlimes.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class SlimeDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.netID == NPCID.Pinky)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RedGel>(), 1, 1, 2)); // Drops 1-2 RedGel items
            }
        }
    }
}
