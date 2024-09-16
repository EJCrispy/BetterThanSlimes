using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


//This section of code removes all copper tools from the starting inventory of the player, and (IN THE FUTURE, NOT IMPLEMENTED YET) grants the player the Guidebook item. 
namespace TutorialMod.Common.Players
{
    public class InventoryPlayer : ModPlayer
    {
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperShortsword);
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperPickaxe);
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperAxe);
        }
    }
}