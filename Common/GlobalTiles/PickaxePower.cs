using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YourModNamespace
{
    public class YourGlobalTile : GlobalTile
    {
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (type == TileID.WoodBlock)
            {
                // Assuming there's a way to get the player's pickaxe power, like Player.pick
                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 100)
                {
                    // Prevents damaging the wood block
                    return false;
                }
            }

            // Allow other tiles to be damaged normally
            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
    }
}
