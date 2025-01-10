using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

public class MyModPlayer : ModPlayer
{
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (Main.mouseRight)
        {
            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            Tile tile = Main.tile[i, j];

            // Check for a specific tile frame that corresponds to rubble tiles
            if (tile != null && tile.TileFrameX == 144 && tile.TileFrameY == 108)
            {
                WorldGen.KillTile(i, j, noItem: true);
                for (int k = 0; k < 10; k++)
                {
                    Item.NewItem(new EntitySource_TileInteraction(Player, i, j), i * 16, j * 16, 16, 16, ItemID.StoneBlock);
                }
            }
        }
    }
}
