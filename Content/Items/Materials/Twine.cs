using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Materials
{
    public class Twine : ModItem
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
