using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModName
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        // List of item IDs to remove from slime drops
        private static readonly HashSet<int> ItemsToRemove = new HashSet<int>
        {
            ItemID.SwiftnessPotion,
            ItemID.IronskinPotion,
            ItemID.SpelunkerPotion,
            ItemID.MiningPotion,
            ItemID.RecallPotion,
            ItemID.WormholePotion,
            ItemID.Torch,
            ItemID.Bomb,
            ItemID.Rope,
            ItemID.Heart,
            ItemID.CopperOre,
            ItemID.TinOre,
            ItemID.IronOre,
            ItemID.LeadOre,
            ItemID.TungstenOre,
            ItemID.SilverOre,
            ItemID.GoldOre,
            ItemID.PlatinumOre
        };

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Check if the NPC is a slime by its AI style
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                // Remove the specified items from the slime's drop list
                npcLoot.RemoveWhere(rule => IsRuleDroppingForbiddenItem(rule));
            }
        }

        private bool IsRuleDroppingForbiddenItem(IItemDropRule rule)
        {
            // Check if the rule is a CommonDrop or another type that directly drops an item
            if (rule is CommonDrop commonDrop)
            {
                return ItemsToRemove.Contains(commonDrop.itemId);
            }

            // Check if the rule is a DropBasedOnExpertMode or similar
            if (rule is DropBasedOnExpertMode expertDrop)
            {
                return IsRuleDroppingForbiddenItem(expertDrop.ruleForNormalMode) ||
                       IsRuleDroppingForbiddenItem(expertDrop.ruleForExpertMode);
            }

            // Check if the rule is a OneFromOptionsDropRule
            if (rule is OneFromOptionsDropRule oneFromOptionsDrop)
            {
                foreach (int itemId in oneFromOptionsDrop.dropIds)
                {
                    if (ItemsToRemove.Contains(itemId))
                    {
                        return true;
                    }
                }
            }

            // Add more rule types here if needed

            return false;
        }
    }
}