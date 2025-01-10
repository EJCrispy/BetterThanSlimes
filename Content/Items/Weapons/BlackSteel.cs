using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class BlackSteel : ModItem
    {
        public override void SetDefaults()
        {

            //Common Properties
            Item.rare = ItemRarityID.Blue;
            Item.value = 40462;
            Item.maxStack = 1;

            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = false;

            // Weapon Properties
            Item.damage = 15;
            Item.crit = 96;
            Item.knockBack = 2;
            Item.noUseGraphic = false;
            Item.noMelee = false;
            Item.DamageType = DamageClass.Melee;

        }
    }
}
