using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BetterThanSlimes.Content.Items.Consumables
{
    public class GreyGel : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            // Only usable during rain
            if (!Main.raining) return false;

            // End rain event
            Main.StopRain();

            // Check if slime rain is not already happening
            if (!Main.slimeRain)
            {
                Main.StartSlimeRain(true); // Start slime rain
                // Ensure that slime rain event starts
                Main.slimeRain = true;

                // Calculate the remaining time until 12:00 AM
                int timeLeftUntilMidnight = (int)((24 * 3600 - Main.time) % 86400);
                if (!Main.dayTime) timeLeftUntilMidnight -= 54000; // Subtract night time if it's already night

                Main.slimeRainTime = timeLeftUntilMidnight; // Set slime rain duration until 12:00 AM
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 50);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddTile(TileID.WorkBenches); // Crafted at a workbench
            recipe.Register();
        }
    }
}
