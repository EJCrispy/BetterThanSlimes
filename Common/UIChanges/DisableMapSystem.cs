using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.Systems
{
    public class DisableMapSystem : ModSystem
    { 
        public override void PostUpdateEverything()
        {
            // Disable map and minimap visibility
            Main.MapScale = 0;  // Disables full map
            Main.mapMinimapScale = 0;  // Disables minimap
            Main.mapFullscreenScale = 0;
            Main.mapEnabled = false;
        }
    }
}
