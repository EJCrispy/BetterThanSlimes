using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

public class DebugRubbleTiles : ModPlayer
{
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (Main.mouseRight)
        {
            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            Tile tile = Main.tile[i, j];

            if (tile != null && IsBoulderTile(tile))
            {
                Console.WriteLine($"Rubble Tile Frame: X = {tile.TileFrameX}, Y = {tile.TileFrameY}");
                WorldGen.KillTile(i, j, noItem: true);
                for (int k = 0; k < 10; k++)
                {
                    Item.NewItem(new EntitySource_TileInteraction(Player, i, j), i * 16, j * 16, 16, 16, ItemID.StoneBlock);
                }
            }
        }
    }

    private bool IsBoulderTile(Tile tile)
    {
        return tile.TileFrameX == 144 && tile.TileFrameY == 108; // 
    }
}
