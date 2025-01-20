using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles.Changes
{
    public class PickaxePowerChanges : GlobalTile
    {
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (type == TileID.Iron)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 20)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.Lead)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 20)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.Silver)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 35)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.Tungsten)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 35)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.Gold)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 45)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.Platinum)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 45)
                {
                    // Prevents damaging the block
                    return false;
                }
            }
            if (type == TileID.LivingWood)
            {

                Player player = Main.LocalPlayer;
                if (player.HeldItem.pick <= 45)
                {
                    // Prevents damaging the block
                    return false;
                }
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
    }
}
