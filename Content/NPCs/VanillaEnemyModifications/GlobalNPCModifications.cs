using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using BetterThanSlimes.Content.Items.Accessories;
using System.Security.Cryptography.X509Certificates;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class GlobalNPCModifications : GlobalItem
    {
        // all code below makes items despawn when spawned in.
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (item.type == ItemID.CopperCoin || item.type == ItemID.SilverCoin || item.type == ItemID.GoldCoin || item.type == ItemID.PlatinumCoin)
            {
                item.TurnToAir();
            }

            if (item.type == ItemID.Heart && !Main.LocalPlayer.GetModPlayer<KapalaPlayer>().kapalaHeart)
            {
                item.TurnToAir();
            }
            if (item.type == ItemID.CandyApple)
            {
                item.TurnToAir();
            }
            if (item.type == ItemID.CandyCane)
            {
                item.TurnToAir();
            }
            if (item.type == ItemID.SoulCake)
            {
                item.TurnToAir();
            }
            if (item.type == ItemID.Star)
            {
                item.TurnToAir();
            }
            if (item.type == ItemID.SugarPlum)
            {
                item.TurnToAir();
            }
        }
    }
}


// This Section adds the following drops to the Doctor Bones enemy: Leather Whip (1), Rope coil (3), and Bombs (3).
namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
    {
        using Terraria.ModLoader;
        using Terraria;
        using Terraria.ID;
        using Terraria.GameContent.ItemDropRules;
        using static global::BetterThanSlimes.BetterThanSlimes.Content.Items.Accessories;

        public class MinibossModifications : GlobalNPC
        {
            public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
            {
                if (npc.type == NPCID.DoctorBones)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemID.BlandWhip, 1));
                    npcLoot.Add(ItemDropRule.Common(ItemID.RopeCoil, 1, 3, 3));
                    npcLoot.Add(ItemDropRule.Common(ItemID.Bomb, 1, 3, 3));
                    npcLoot.Add(ItemDropRule.Common(itemId: 5547,
                                                    1)); //should drop kapala
                }

                if (npc.type == NPCID.Clown)
                {
                    npcLoot.Add(ItemDropRule.Common(itemId: 5546,
                                                    1)); //should make clowns drop red nose
                }
            if (npc.type == NPCID.Bunny)
                if (npc.type == NPCID.Squirrel)
                    if (npc.type == NPCID.SquirrelRed)
                        if (npc.type == NPCID.PartyBunny)
                            if (npc.type == NPCID.BunnySlimed)
                                if (npc.type == NPCID.BunnyXmas)
                                    if (npc.type == NPCID.Duck)
                                        if (npc.type == NPCID.Duck2)
                                            if (npc.type == NPCID.DuckWhite)
                                                if (npc.type == NPCID.DuckWhite2)
                                                    if (npc.type == NPCID.Grebe)
                                                        if (npc.type == NPCID.Grebe2)
                                                            if (npc.type == NPCID.Owl)
                                                                if (npc.type == NPCID.Penguin)
                                                                    if (npc.type == NPCID.Seagull)
                                                                        if (npc.type == NPCID.Goldfish)
                                                                            if (npc.type == NPCID.GoldfishWalker)
                                                                                if (npc.type == NPCID.SnowFlinx)
                                                                                    npcLoot.Add(ItemDropRule.Common(itemId: 5546,
                                1)); //should make clowns drop red nose
        }
        }
    }

