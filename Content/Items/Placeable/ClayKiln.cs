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
            Main.tileLavaDeath[Type] = true; // Optional: Breaks in lava.

            // Prevents the tile from dropping the default item
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);

            var name = CreateMapEntryName();
            AddMapEntry(new Color(200, 100, 50), name);

            DustType = DustID.RedMoss;
            AnimationFrameHeight = 38;

            // Set the tile as a crafting station with the same effects as a furnace
            AdjTiles = new int[] { TileID.Furnaces };
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameYOffset = Main.tileFrameCounter[type] % 12 * AnimationFrameHeight;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.LocalPlayer.adjTile[TileID.Furnaces] = true;

                // Emits furnace-like glow
                Vector2 position = new Vector2(i * 16, j * 16);
                Lighting.AddLight(position, 1.0f, 0.5f, 0.2f);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // Only drop 6 Red Bricks, no longer drop the tile itself
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemID.RedBrick, 6);
            }
        }

        public override bool CanDrop(int i, int j)
        {
            return false;   
        }

        public override void RandomUpdate(int i, int j)
        {
            // Emits sparks like a furnace
            if (Main.rand.NextBool(10))
            {
                int dust = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Torch, 0f, -1f, 100, default, 1.5f);
                Main.dust[dust].velocity *= 0.5f;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}