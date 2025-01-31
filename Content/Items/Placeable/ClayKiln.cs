using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
            Main.tileLavaDeath[Type] = true; // Optional, if you want it to break in lava.

            // Tile size and style setup
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);

            // Map entry
            var name = CreateMapEntryName();
            AddMapEntry(new Color(200, 100, 50), name);

            DustType = DustID.RedMoss; // Optional: Change to a different dust type if needed
            AnimationFrameHeight = 38; // Each frame height (sprite's height divided by 12 for 12 frames)
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            // Calculate frameYOffset based on the tile's frame
            frameYOffset = Main.tileFrameCounter[type] % 12 * AnimationFrameHeight;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.LocalPlayer.adjTile[TileID.Furnaces] = true;

                // Emit light (optional)
                Lighting.AddLight(new Vector2(i * 16, j * 16), 1.0f, 0.5f, 0.2f);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<ClayKilnItem>());
        }

        public override void RandomUpdate(int i, int j)
        {
            // Emit small sparks similar to a furnace
            if (Main.rand.NextBool(10)) // Adjust the chance to emit particles
            {
                int dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Torch, 0f, -1f, 100, default, 1.5f);
                Main.dust[dust].velocity *= 0.5f;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
