using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterThanSlimes.Common.UIChanges
{
    public class MoveTrashSlotOffScreen : ModSystem
    {
   public bool DrawInventoryWithTrashSlotOffScreen()
        {
            // Move the trash slot off-screen
            Main.trashSlotOffset = new Point16(-1000, -1000);

            // Draw the vanilla inventory
            // Note: We cannot directly call Main.DrawInventory() because it is private.
            // Instead, we rely on the vanilla UI system to draw the inventory.
            return true;
        }
    }
}