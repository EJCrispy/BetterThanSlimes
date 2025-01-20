using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.Systems
{
    public class DisableMapSystem : ModSystem
    {
        public override void PostUpdateEverything()
        {
            {
                Main.mapEnabled = false;
            }
        }
    }
}
