using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items
{
    public class Guidebook : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 30;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanRightClick()
        {
            return true; // Allow right-click functionality
        }

        public override void RightClick(Player player)
        {
            if (!Main.dedServ) // Ensure this runs on the client
            {
                Main.playerInventory = true; // Open the inventory
                Main.npcChatText = "";       // Clear any chat text
                Main.npcChatCornerItem = 0;  // Reset the corner item
                Main.HoverItem = new Item(); // Clear any hovered item
                Main.npcChatRelease = true;  // Prevent lingering interactions

                // Open the crafting UI as if the Guide was interacted with
                Recipe.FindRecipes();
                Main.guideItem.type = ItemID.None; // Clear any previous "guide help" item
                Main.guideItem.SetDefaults(); // Reset guide item
                Main.InGuideCraftMenu = true; // Enable Guide crafting functionality
            }
        }
    }
}
