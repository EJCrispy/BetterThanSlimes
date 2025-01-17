using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModNamespace
{
    public class YourGlobalTile : GlobalTile
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if (type == TileID.WoodBlock)
            {
                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 100)
                {
                    // Prevents breaking the wood block
                    return false;
                }
            }

            // Allow other tiles to break normally
            return base.CanExplode(i, j, type);
        }
    }
}
