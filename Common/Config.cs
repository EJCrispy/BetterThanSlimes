using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterThanSlimes.Common
{
    public class BetterThanSlimesConfig : ModConfig
    {
        public static BetterThanSlimesConfig Instance;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [Label("Disable Map Visibility")]
        [Tooltip("If enabled, the map and minimap will be disabled while this mod is active.")]
        public bool DisableMap;

        public override void OnLoaded()
        {
            Instance = this;
        }
    }
}
