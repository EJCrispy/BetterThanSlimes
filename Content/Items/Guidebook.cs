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

        public override bool? UseItem(Player player)
        {
            if (!Main.dedServ) // Ensure this runs on the client
            {
                GuidebookUIState.ToggleUI(); // Toggle the guidebook UI
            }
            return true;
        }

        public override bool CanRightClick()
        {
            return true; // Enable right-click functionality
        }

        public override void RightClick(Player player)
        {
            if (!Main.dedServ) // Ensure this runs on the client
            {
                // Open the crafting UI as if interacting with the Guide
                Main.playerInventory = true; // Open the inventory window
                Main.InGuideCraftMenu = true; // Enable the Guide's crafting menu

                // Clear the guide item and reset related states
                Main.guideItem.SetDefaults();
                Main.guideItem.type = ItemID.None; // Ensure no item is selected
                Main.npcChatText = ""; // Clear chat text
                Main.npcChatCornerItem = 0; // Clear corner item
                Main.HoverItem = new Item(); // Clear any hovered item
                Main.npcChatRelease = true; // Prevent lingering interactions

                // Refresh crafting recipes to ensure it's up-to-date
                Recipe.FindRecipes();
            }
        }
    }
}