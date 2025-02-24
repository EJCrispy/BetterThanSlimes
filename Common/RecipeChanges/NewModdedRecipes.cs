using BetterThanSlimes.Content.Items.Materials;
using BetterThanSlimes.Content.Items.Weapons;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class CustomRecipes : ModSystem
{
    public override void PostAddRecipes()
    {
        Mod.Logger.Info("Adding custom recipes...");

        AddWoodPlatformRecipe();
        AddWorkbenchRecipe();
        AddWoodenSwordRecipe();
        AddTorchRecipe();
        AddSpearRecipe();
        AddClayBrickRecipe();
        AddLooseStoneRecipe();
        AddSlingshotRecipe();
    }

    private void AddSlingshotRecipe()
    {
        Recipe recipe = Recipe.Create(ModContent.ItemType<Slingshot>());
        recipe.AddIngredient(ModContent.ItemType<Shaft>());
        recipe.AddIngredient(ModContent.ItemType<Twine>());
        recipe.AddIngredient(ModContent.ItemType<Twig>(), 3);
        recipe.Register();
    }

    private void AddLooseStoneRecipe()
    {
        Recipe recipe = Recipe.Create(ModContent.ItemType<LooseStone>(), 4);
        recipe.AddIngredient(ItemID.StoneBlock);
        recipe.Register();
    }

    private void AddClayBrickRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.RedBrick);
        recipe.AddIngredient(ItemID.ClayBlock, (2));
        recipe.AddTile(TileID.Campfire);
        recipe.Register();
    }

    private void AddSpearRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.Spear);
        recipe.AddIngredient(ModContent.ItemType<SpearHead>());
        recipe.AddIngredient(ModContent.ItemType<MetalRod>());
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    private void AddWoodenSwordRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WoodenSword); // Creates a recipe for 1 Wooden Sword
        recipe.AddIngredient(ItemID.Wood, 20); // Requires 20 Wood as ingredient
        recipe.AddTile(TileID.WorkBenches); // Requires a Work Bench to craft
        recipe.Register(); // Registers the recipe into the game
    }

    private void AddWoodPlatformRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WoodPlatform); // Creates a recipe for 1 Wood Platform
        recipe.AddIngredient(ItemID.Wood, 1); // Requires 1 Wood as ingredient
        recipe.AddTile(TileID.WorkBenches); // Requires a Work Bench to craft
        recipe.Register(); // Registers the recipe into the game
    }

    private void AddWorkbenchRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.WorkBench); // Creates a recipe for 1 Work Bench
        recipe.AddIngredient(ItemID.Wood, 30); // Requires 30 Wood as ingredient
        recipe.Register(); // Registers the recipe into the game
    }
    private void AddTorchRecipe()
    {
        Recipe recipe = Recipe.Create(ItemID.Torch);
        recipe.AddIngredient(ModContent.ItemType<Twig>());
        recipe.AddIngredient(ModContent.ItemType<RedGel>());
        recipe.Register();
    }


}
