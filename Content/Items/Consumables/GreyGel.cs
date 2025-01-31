using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BetterThanSlimes.Content.Items.Consumables
{
    public class SummonSlimeRain : ModItem
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
            // Only useable during rain
            if (!Main.raining) return false;

            // Check if slime rain is not already happening
            if (!Main.slimeRain)
            {
                Main.StartSlimeRain(true); // Start slime rain
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 50); // Requires 50 gel
            recipe.AddTile(TileID.WorkBenches); // Crafted at a workbench
            recipe.Register();
        }
    }
}
