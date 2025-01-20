using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class CustomRecipes : ModSystem
{
    public override void AddRecipes()
    {
        AddWoodPlatformRecipe();
        AddWorkbenchRecipe();
        AddWoodenSwordRecipe();
    }

    private void AddWoodenSwordRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WoodenSword, 1); // Creates a recipe for 1 Wood Platform
        recipe.AddIngredient(ItemID.Wood, 20); // Requires 1 Wood as ingredient
        recipe.AddTile(TileID.WorkBenches); // Requires a Work Bench to craft
        recipe.Register(); // Registers the recipe into the game
    }

    private void AddWoodPlatformRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WoodPlatform, 1); // Creates a recipe for 1 Wood Platform
        recipe.AddIngredient(ItemID.Wood, 1); // Requires 1 Wood as ingredient
        recipe.AddTile(TileID.WorkBenches); // Requires a Work Bench to craft
        recipe.Register(); // Registers the recipe into the game
    }

    private void AddWorkbenchRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WorkBench, 1); // Creates a recipe for 1 Work Bench
        recipe.AddIngredient(ItemID.Wood, 30); // Requires 30 Wood as ingredient
        recipe.Register(); // Registers the recipe into the game
    }
}
