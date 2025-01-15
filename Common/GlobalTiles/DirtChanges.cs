using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class GlobalDirtTile : GlobalTile
{
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (type == TileID.Dirt && !noItem)
        {
            // Prevent the default item drop
            noItem = true;
            // Drop wood instead
            Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.Wood);
        }
    }
}
