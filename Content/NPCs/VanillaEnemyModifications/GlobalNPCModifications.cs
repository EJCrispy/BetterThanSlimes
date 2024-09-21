using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

// This section of code reshapes enemy properties such as drop rates.

//This section removes hearts from enemy drops.
namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class GlobalNPCModifications : GlobalItem
    {
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (item.type == ItemID.Heart)
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
        }
    }
}

//This Section adds the following drops to the Doctor Bones enemy: Leather Whip (1), Rope coil (3), and Bombs (3).
namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class DoctorBonesModifications : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.DoctorBones)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BlandWhip, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.RopeCoil, 1, 3, 3));
                npcLoot.Add(ItemDropRule.Common(ItemID.Bomb, 1, 3, 3));
            }
        }
    }
}