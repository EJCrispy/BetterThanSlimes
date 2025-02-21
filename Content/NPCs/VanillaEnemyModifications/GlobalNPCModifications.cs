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
                    npcLoot.Add(ItemDropRule.Common(itemId: 5546,
                                                    1)); //should drop kapala
                }

                if (npc.type == NPCID.Clown)
                {
                    npcLoot.Add(ItemDropRule.Common(itemId: 5547,
                                                    1)); //should make clowns drop red nose
                }
            int[] npcTypes = {
    NPCID.Bunny, NPCID.Squirrel, NPCID.SquirrelRed, NPCID.PartyBunny,
    NPCID.BunnySlimed, NPCID.BunnyXmas, NPCID.Duck, NPCID.Duck2,
    NPCID.DuckWhite, NPCID.DuckWhite2, NPCID.Grebe, NPCID.Grebe2,
    NPCID.Owl, NPCID.Penguin, NPCID.Seagull, NPCID.Goldfish,
    NPCID.GoldfishWalker, NPCID.SnowFlinx
};

            if (npcTypes.Contains(npc.type))
            {
                npcLoot.Add(ItemDropRule.Common(itemId: 5553, 1));
            }

        }
    }
    }

