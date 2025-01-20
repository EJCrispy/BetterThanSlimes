using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Materials
{
    public class Twig : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 18;

            Item.rare = ItemRarityID.White;
            Item.maxStack = 9999;
            Item.value = 0;
        }
    }
}
