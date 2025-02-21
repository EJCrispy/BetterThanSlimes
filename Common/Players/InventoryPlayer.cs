using BetterThanSlimes.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Common.Players
{
    public class InventoryPlayer : ModPlayer
    {
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            // Remove certain items from the starting inventory
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperShortsword);
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperPickaxe);
            itemsByMod["Terraria"].RemoveAll(item => item.type == ItemID.CopperAxe);

            // Add Steak to the starting inventory
            if (itemsByMod.TryGetValue("Terraria", out var items))
            {
                items.Add(new Item(ItemID.Steak, 1));
            }
        }
    }
}
