using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Generation;
using Terraria.IO;

namespace BetterThanSlimes.Common.Worldgen
{
    // This forces all ores to spawn on world gen
    public class ForceOres : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int oreGenIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (oreGenIndex != -1)
            {
                tasks.Insert(oreGenIndex + 1, new PassLegacy("CustomOreGeneration", GenerateOres));
            }
        }

        private void GenerateOres(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Spawning all ores...";

            // Example of placing ores manually
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    if (WorldGen.genRand.NextBool(1000)) // Adjust chance as needed
                    {
                        int oreType = GetRandomOreType();
                        WorldGen.PlaceTile(i, j, oreType, true, true);
                    }
                }
            }
        }

        private int GetRandomOreType()
        {
            int[] ores = new int[]
            {
                TileID.Copper,
                TileID.Iron,
                TileID.Silver,
                TileID.Gold,
                TileID.Tin,
                TileID.Lead,
                TileID.Tungsten,
                TileID.Platinum,
                // Add other ores as needed
            };
            return ores[WorldGen.genRand.Next(ores.Length)];
        }
    }
}
