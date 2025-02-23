using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModName.Common.Worldgen
{
    public class LifeFruitGrowthSystem : ModSystem
    {
        public override void Load()
        {
            // Hook into the grass growth update system.
            On_WorldGen.UpdateWorld_GrassGrowth += FasterLifeFruitGrowth;
        }

        public override void Unload()
        {
            // Unhook to avoid memory leaks or unintended behavior.
            On_WorldGen.UpdateWorld_GrassGrowth -= FasterLifeFruitGrowth;
        }

        private void FasterLifeFruitGrowth(On_WorldGen.orig_UpdateWorld_GrassGrowth orig, int i, int j, int minI, int maxI, int minJ, int maxJ, bool underground)
        {
            // Call the original method to maintain vanilla behavior.
            orig(i, j, minI, maxI, minJ, maxJ, underground);

            // Modify Life Fruit growth behavior within the specified area.
            for (int x = minI; x < maxI; x++)
            {
                for (int y = minJ; y < maxJ; y++)
                {
                    // Check if the tile is Life Fruit.
                    if (Main.tile[x, y].TileType == TileID.LifeFruit)
                    {
                        AttemptFasterLifeFruitGrowth(x, y);
                    }
                }
            }
        }

        private void AttemptFasterLifeFruitGrowth(int x, int y)
        {
            // Simulate 100,000 growth attempts for Life Fruit.
            // To optimize performance, we'll use a loop with a smaller number of iterations
            // but increase the likelihood of growth in each iteration.
            for (int i = 0; i < 1000; i++) // Reduced iterations for performance
            {
                // Check if Life Fruit can grow at this location.
                if (CanLifeFruitGrow(x, y))
                {
                    // Place Life Fruit at the location with a high probability.
                    if (Main.rand.Next(100) < 100) // 100% chance to grow in each iteration
                    {
                        WorldGen.PlaceTile(x, y, TileID.LifeFruit, mute: true, forced: false);
                    }
                }
            }
        }

        private bool CanLifeFruitGrow(int x, int y)
        {
            // Check if the tile is in the Jungle biome and meets Life Fruit growth conditions.
            return Main.tile[x, y].TileType == TileID.JungleGrass && // Must be on Jungle Grass
                   !Main.tile[x, y].HasTile && // Tile must be empty
                   Main.tile[x, y - 1].HasTile && // Tile above must be solid
                   Main.tile[x, y - 1].TileType != TileID.LifeFruit; // Tile above must not be Life Fruit
        }
    }
}