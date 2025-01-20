using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Materials
{
    public class RedGel : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 12;

            Item.rare = ItemRarityID.White;
            Item.maxStack = 9999;
            Item.value = 0;
        }

        public override void UpdateInventory(Player player)
        {
            player.AddBuff(BuffID.OnFire, 1); // This sets the player on fire for 1 second (60 ticks) while the item is in their inventory
        }
    }
}
