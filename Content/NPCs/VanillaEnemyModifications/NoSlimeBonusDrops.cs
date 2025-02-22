using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModName
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        // List of NetIDs for slimes
        private static readonly HashSet<int> SlimeNetIDs = new HashSet<int>
        {
            NPCID.BlueSlime,        // NetID: 1
            NPCID.GreenSlime,       // NetID: -2 (handled separately)
            NPCID.PurpleSlime,      // NetID: -3 (handled separately)
            NPCID.RedSlime,         // NetID: -4 (handled separately)
            NPCID.YellowSlime,      // NetID: -5 (handled separately)
            NPCID.BlackSlime,       // NetID: -6 (handled separately)
            NPCID.MotherSlime,      // NetID: -7 (handled separately)
            NPCID.LavaSlime,        // NetID: -8 (handled separately)
            NPCID.JungleSlime,      // NetID: -9 (handled separately)
            NPCID.SpikedIceSlime,   // NetID: -10 (handled separately)
            NPCID.SpikedJungleSlime,// NetID: -11 (handled separately)
            NPCID.SandSlime,        // NetID: -12 (handled separately)
            NPCID.CorruptSlime,     // NetID: -13 (handled separately)
            NPCID.Slimer,          // NetID: -14 (handled separately)
            NPCID.Slimer2,         // NetID: -15 (handled separately)
            NPCID.IceSlime,        // NetID: -16 (handled separately)
            NPCID.UmbrellaSlime,   // NetID: -17 (handled separately)
            NPCID.DungeonSlime,    // NetID: -18 (handled separately)
            NPCID.RainbowSlime,    // NetID: -19 (handled separately)
            NPCID.Gastropod,       // NetID: -20 (handled separately)
            // Add more slime NetIDs here if needed
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
            // Check if the NPC is a slime by its NetID
            if (IsSlime(npc))
            {
                // Remove the specified items from the slime's drop list
                npcLoot.RemoveWhere(rule => IsRuleDroppingForbiddenItem(rule));
            }
        }

        private bool IsSlime(NPC npc)
        {
            // Check if the NPC's NetID is in the SlimeNetIDs list
            return SlimeNetIDs.Contains(npc.netID);
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