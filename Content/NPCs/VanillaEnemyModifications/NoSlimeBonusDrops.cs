using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModName
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        // List of NPC IDs for slimes
        private static readonly HashSet<int> SlimeNPCIDs = new HashSet<int>
        {
            NPCID.BlueSlime,
            NPCID.GreenSlime,
            NPCID.PurpleSlime,
            NPCID.RedSlime,
            NPCID.YellowSlime,
            NPCID.BlackSlime,
            NPCID.MotherSlime,
            NPCID.LavaSlime,
            NPCID.JungleSlime,
            NPCID.SpikedIceSlime,
            NPCID.SpikedJungleSlime,
            NPCID.SandSlime,
            NPCID.CorruptSlime,
            NPCID.Slimer,
            NPCID.Slimer2,
            NPCID.IceSlime,
            NPCID.UmbrellaSlime,
            NPCID.DungeonSlime,
            NPCID.RainbowSlime,
            NPCID.Gastropod
            // Add more slime NPC IDs here if needed
        };

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
            // Check if the NPC is a slime by its NPC ID
            if (SlimeNPCIDs.Contains(npc.type))
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