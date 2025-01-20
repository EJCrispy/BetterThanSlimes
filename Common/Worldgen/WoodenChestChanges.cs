using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.DataStructures;
using BetterThanSlimes.Content.Items.Weapons;
using BetterThanSlimes.Content.Items.Materials;

namespace BetterThanSlimes.Common.Worldgen
{
    public class ChestItemWorldGen : ModSystem
    {
        // We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
        public override void PostWorldGen()
        {
            // Loop over all chests and modify their contents
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }

                Tile chestTile = Main.tile[chest.x, chest.y];
                // Check if the chest is a Wooden Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 0)  // 0 corresponds to Wooden Chests
                {
                    // Loop through all items in the chest and clear the slots (remove vanilla items)
                    for (int i = 0; i < Chest.maxItems; i++)
                    {
                        chest.item[i] = new Item();  // Clear the chest's inventory slot
                    }

                    // Now add the new items with their respective chances
                    AddItemToChest(chest, ModContent.ItemType<LooseStone>(), WorldGen.genRand.Next(2, 7)); // Loose Stones (2-6)
                    AddItemToChest(chest, ModContent.ItemType<Twine>(), 1, 0.25f); // Twine (1) with 25% chance
                    AddItemToChest(chest, ModContent.ItemType<Twig>(), WorldGen.genRand.Next(2, 5)); // Twigs (2-4)
                    AddItemToChest(chest, ItemID.Gel, WorldGen.genRand.Next(1, 3), 0.5f); // Gel (1-2) with 50% chance
                }
            }
        }

        // Helper function to add items to the chest with optional chance
        private void AddItemToChest(Chest chest, int itemType, int amount, float chance = 1f)
        {
            if (WorldGen.genRand.NextFloat() <= chance)  // Check if we roll the chance
            {
                for (int i = 0; i < Chest.maxItems; i++)
                {
                    if (chest.item[i].type == ItemID.None)
                    {
                        chest.item[i].SetDefaults(itemType);  // Set the item type
                        chest.item[i].stack = amount;  // Set the random amount of the item
                        break;  // Stop after placing the item
                    }
                }
            }
        }
    }
}
