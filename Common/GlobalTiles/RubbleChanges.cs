using BetterThanSlimes.Content.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles
{
    public class RubbleChanges : ModTile
    {
        public override void MouseOver(int i, int j)
        {

            // Show a little loose stone icon on the mouse indicating you're hovering over it.

            Main.LocalPlayer.cursorItemIconEnabled = true;
            Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<LooseStone>();
        }
    }
}