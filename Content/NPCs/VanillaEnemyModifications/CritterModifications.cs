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
               
if (npc.netID == NPCID.Bunny)
if (npc.netID == NPCID.Squirrel)
if (npc.netID == NPCID.SquirrelRed)
if (npc.netID == NPCID.PartyBunny)
if (npc.netID == NPCID.BunnySlimed)
if (npc.netID == NPCID.BunnyXmas)
if (npc.netID == NPCID.Duck)
if (npc.netID == NPCID.Duck2)
if (npc.netID == NPCID.DuckWhite)
if (npc.netID == NPCID.DuckWhite2)
if (npc.netID == NPCID.Grebe)
if (npc.netID == NPCID.Grebe2)
if (npc.netID == NPCID.Owl)
if (npc.netID == NPCID.Penguin)
if (npc.netID == NPCID.Seagull)
if (npc.netID == NPCID.Goldfish)
if (npc.netID == NPCID.GoldfishWalker)
if (npc.netID == NPCID.RedSlime)
                                                                                { 
// Add the new loot rule
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Morsel>(), 1, 1, 10));

                }
            }
        }
    }
