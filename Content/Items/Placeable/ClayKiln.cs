using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace BetterThanSlimes.Content.Items.Placeable
{
    public class ClayKiln : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.addTile(Type);

            var name = CreateMapEntryName(); // Localized name
            AddMapEntry(new Color(200, 100, 50), name);
            DustType = DustID.RedMoss;
        }

        public override bool RightClick(int i, int j)
        {
            Main.LocalPlayer.adjTile[TileID.Furnaces] = true;
            return true; // Indicate the right-click action was handled
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.LocalPlayer.adjTile[TileID.Furnaces] = true;
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<ClayKilnItem>());
        }
    }
}