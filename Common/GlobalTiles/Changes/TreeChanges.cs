using BetterThanSlimes.Content.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace BetterThanSlimes.Common.GlobalTiles.Changes
{
    public class TreeChanges : GlobalTile
    {
        private static readonly Random random = new Random(); // Define the random instance

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Tile tile = Main.tile[i, j];

            if (tile.TileType == 5 && !fail && !noItem) // Replace 5 with your specific wood tile type
            {
                noItem = true;
                // 50% chance to drop custom wood
                if (random.NextDouble() < 0.5)
                {
                    Item.NewItem(null, new Vector2(i * 16, j * 16), ItemID.Wood);
                }
            }
            if ((type == TileID.Trees || type == TileID.PalmTree || type == TileID.MushroomTrees || type == TileID.VanityTreeYellowWillow || type == TileID.VanityTreeSakura) && !fail && !noItem)
            {
                // Prevent wood and coin drops
                noItem = true;

                // Allow specific drops by manually spawning them
                if (Main.rand.NextBool(10)) // Example: 10% chance for fruit
                {
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ItemID.Apple); // Replace with desired fruit ID
                }

                if (Main.rand.NextBool(5)) // Example: 20% chance for acorn
                {
                    Item.NewItem(null, i * 16, j * 16, 16, 16, ItemID.Acorn);
                }

                // Add more conditions for other desired items if needed
            }
        }
    }
    public class TreeShakeChanges : GlobalItem
    {
        public override void OnSpawn(Item item, IEntitySource source)
        {
            // Check if the source is from shaking a tree
            if (source is EntitySource_ShakeTree)
            {
                // Prevent wood and coins from spawning
                if (item.type == ItemID.Wood || item.type == ItemID.CopperCoin || item.type == ItemID.SilverCoin ||
                    item.type == ItemID.GoldCoin || item.type == ItemID.PlatinumCoin)
                {
                    item.TurnToAir(); // Remove the item
                }
            }
        }
    }
}
