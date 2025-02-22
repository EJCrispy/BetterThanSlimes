using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class NoBonusSlimeDrops : GlobalNPC
    {
        // List of NetIDs for slimes
        private static readonly HashSet<int> SlimeNetIDs = new HashSet<int>
        {
            NPCID.BlueSlime,        // NetID: 1
            NPCID.GreenSlime,       // NetID: -2
            NPCID.PurpleSlime,      // NetID: -3
            NPCID.RedSlime,         // NetID: -4
            NPCID.YellowSlime,      // NetID: -5
            NPCID.BlackSlime,       // NetID: -6
            NPCID.MotherSlime,      // NetID: -7
            NPCID.LavaSlime,        // NetID: -8
            NPCID.JungleSlime,      // NetID: -9
            NPCID.SpikedIceSlime,   // NetID: -10
            NPCID.SpikedJungleSlime,// NetID: -11
            NPCID.SandSlime,        // NetID: -12
            NPCID.CorruptSlime,     // NetID: -13
            NPCID.Slimer,          // NetID: -14
            NPCID.Slimer2,         // NetID: -15
            NPCID.IceSlime,        // NetID: -16
            NPCID.UmbrellaSlime,   // NetID: -17
            NPCID.DungeonSlime,    // NetID: -18
            NPCID.RainbowSlime,    // NetID: -19
            NPCID.Gastropod,       // NetID: -20
            NPCID.KingSlime,       // NetID: 50
            // Add more slime NetIDs here if needed
        };

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Check if the NPC is a slime by its NetID
            if (SlimeNetIDs.Contains(npc.netID))
            {
                // Clear all existing loot rules
                npcLoot.RemoveWhere(rule => true);

                // Add custom loot rules
                switch (npc.netID)
                {
                    default:
                        // Default behavior: Drop Gel
                        npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 2)); // Drops 1-2 Gel
                        break;
                }
            }
        }
    }
}
