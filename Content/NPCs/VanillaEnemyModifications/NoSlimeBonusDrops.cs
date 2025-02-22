using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Check if the NPC is a slime by its AI style
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                // Remove all existing drop rules for slimes
                npcLoot.RemoveWhere(rule => rule is SlimeBodyItemDropRule);

                // Add a custom rule that ensures no bonus drops
                npcLoot.Add(ItemDropRule.ByCondition(new NoSlimeBonusDrops(), ItemID.None));
            }
        }
    }

    public class NoSlimeBonusDrops : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            // Always return false to prevent bonus drops
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            // Return false to hide this rule in the UI
            return false;
        }

        public string GetConditionDescription()
        {
            // Optional: Provide a description for the condition
            return "No bonus drops from slimes";
        }
    }
}