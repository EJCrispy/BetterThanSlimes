using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Materials
{
    public class SpearHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.value = 10;
            Item.rare = ItemRarityID.White;
        }
    }
}