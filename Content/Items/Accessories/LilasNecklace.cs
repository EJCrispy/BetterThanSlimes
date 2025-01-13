using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BetterThanSlimes.Content.Items.Accessories
{
    public class LilasNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            // The display name and tooltip will be set using the .lang file
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
            Item.vanity = false;
            Item.rare = ItemRarityID.Blue; // Set the rarity to blue
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.15f; // Increases movement speed by 15%
            player.jumpSpeedBoost += 0.20f; // Increases jump speed by 20%
            player.jumpBoost = true; // Enables increased jump height
        }
    }
}
