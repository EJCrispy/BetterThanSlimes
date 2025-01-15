using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace BetterThanSlimes.Common.Worldgen
{
    // this removes the guide on world gen
    public class RemoveGuide : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int GuideIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Guide"));
            if (GuideIndex != -1)
            {
                tasks.RemoveAt(GuideIndex);
            }
        }
    }
}
