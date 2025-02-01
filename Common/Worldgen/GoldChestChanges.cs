using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.DataStructures;
using BetterThanSlimes.Content.Items.Weapons;
using BetterThanSlimes.Content.Items.Materials;
using BetterThanSlimes.Content.Items.Accessories;

namespace BetterThanSlimes.Common.Worldgen
{
    public class GoldChestChanges : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }

                Tile chestTile = Main.tile[chest.x, chest.y];
                // Check if the chest is a Gold Chest, Marble Chest, Granite Chest, Dead Man's Chest, or Rich Mahogany Chest
                if (chestTile.TileType == TileID.Containers &&
                    (chestTile.TileFrameX == 36 ||  // Gold Chest
                     chestTile.TileFrameX == 72 ||  // Marble Chest
                     chestTile.TileFrameX == 108 || // Granite Chest
                     chestTile.TileFrameX == 144 || // Dead Man's Chest
                     chestTile.TileFrameX == 180))  // Rich Mahogany Chest
                {
                    // Loop through all items in the chest and clear the slots (remove vanilla items)
                    for (int i = 0; i < Chest.maxItems; i++)
                    {
                        chest.item[i] = new Item();  // Clear the chest's inventory slot
                    }

                    // 33% chance to add RunningShoes, Bottle, or BrokenMirror
                    float roll = WorldGen.genRand.NextFloat();
                    if (roll < 0.33f)
                    {
                        chest.item[0].SetDefaults(ModContent.ItemType<RunningShoes>());
                    }
                    else if (roll < 0.66f)
                    {
                        chest.item[0].SetDefaults(ItemID.Bottle);
                    }
                    else
                    {
                        chest.item[0].SetDefaults(ModContent.ItemType<BrokenMirror>());
                    }
                }
            }
        }
    }
}
