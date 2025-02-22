using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModName
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Check if the NPC is a slime
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.GreenSlime || npc.type == NPCID.PurpleSlime /* Add other slime types here */)
            {
                // Remove all existing drop rules for slimes
                npcLoot.RemoveWhere(rule => rule is SlimeBodyItemDropRule);

                // Add a new drop rule that ensures no bonus drops
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