using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using BetterThanSlimes.Content.Items.Placeable;

namespace BetterThanSlimes.Content.ModdedTiles
{
    public class LooseDirtTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(151, 107, 75), Language.GetText("Loose Dirt"));

            DustType = DustID.Dirt; // Determines the type of dust the tile generates when broken
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!noItem && !effectOnly)
            {
                // Prevent the default item drop
                noItem = true;
                // Drop the custom item (Loose Dirt)
                Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<LooseDirtItem>());
            }
        }
    }
}