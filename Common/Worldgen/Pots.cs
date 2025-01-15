using System.Collections.Generic;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace BetterThanSlimes.Common.Worldgen
{ // ty pepperoni, this just removes all pots on world gen
    public class RemovePots : ModSystem
    {
        public static LocalizedText WorldGenTutorialOresPassMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            WorldGenTutorialOresPassMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(WorldGenTutorialOresPassMessage)}"));
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {

            int PotsIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));
            if (PotsIndex != -1)
            {

                tasks.RemoveAt(PotsIndex);

                
            }
        }
    }
}