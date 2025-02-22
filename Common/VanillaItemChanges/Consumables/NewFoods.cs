using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace BetterThanSlimes.Common.VanillaItemChanges.Tools
{
    public class NewFoods : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                item.consumable = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.useStyle = ItemUseStyleID.EatFood;
                item.UseSound = SoundID.Item2;
            }

            // Blue Berries
            if (item.type == ItemID.BlueBerries)
            {
                item.consumable = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.useStyle = ItemUseStyleID.EatFood;
                item.UseSound = SoundID.Item2;
            }
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                player.AddBuff(BuffID.WellFed, 60 * 60); // 1 minute
            }

            // Blue Berries
            if (item.type == ItemID.BlueBerries)
            {
                player.AddBuff(BuffID.WellFed, 120 * 60); // 2 minutes
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "Grants 1 minute of food";
                        break;
                    }
                }
            }

            // Blue Berries
            if (item.type == ItemID.BlueBerries)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "The juice gets everywhere. Much on them to satiate yourself for 2 minutes.";
                        break;
                    }
                }
            }
        }
    }
}