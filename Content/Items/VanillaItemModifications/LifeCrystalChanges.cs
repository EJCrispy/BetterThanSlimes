using System;
using System.Collections.Generic;
using System.Text;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class VanillaConsumableModifications
    {
        public class LifeCrystal : GlobalItem
        {
            public override void SetDefaults(Item item)
            {
                if (item.type == ItemID.LifeCrystal)
                { }
            }

            public static readonly int UseHealthMaxIncreasingItem = 10;
        }

    }
}

