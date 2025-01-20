using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

public class RemoveRecipes : ModSystem
{
    public override void AddRecipes()
    {
        RemoveSpecificRecipes();
    }

    private void RemoveSpecificRecipes()
    {
        // Define the item IDs of the recipes you want to remove
        int[] itemsToRemove = new int[]
        {
            ItemID.WoodenHammer,
            ItemID.WoodenBow,
            ItemID.Torch,
            ItemID.CactusPickaxe,
            ItemID.CactusHelmet,
            ItemID.CactusBreastplate,
            ItemID.CactusLeggings,
            ItemID.WoodWall
        };

        // Iterate through recipes and remove the ones that match the target items
        for (int i = Main.recipe.Length - 1; i >= 0; i--)
        {
            Recipe recipe = Main.recipe[i];
            if (recipe.createItem.type != ItemID.None && itemsToRemove.Contains(recipe.createItem.type))
            {
                recipe.DisableRecipe();  // Disable the recipe without removing it entirely
            }
        }
    }
}
