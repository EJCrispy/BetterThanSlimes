using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using BetterThanSlimes.Content.Items.Materials;
using BetterThanSlimes.Content.Items.Consumables;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{ 

    public class CritterModificaitons : GlobalNPC
        {
            public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
            {
               
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


                                                                                {
                                                                                    // Clear the existing loot rules
                                                                                    npcLoot.RemoveWhere(rule => true);
                                                                                    // Add the new loot rule
                                                                                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Morsel>(), 1, 1, 10));

                }
            }
        }
    }
