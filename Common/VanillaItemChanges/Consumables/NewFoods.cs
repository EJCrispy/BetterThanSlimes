using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace BetterThanSlimes.Common.VanillaItemChanges.Tools
{
    public class FoodItems : GlobalItem
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
                item.createTile = -1; // Disable tile placement
                item.noMelee = true;
                item.noUseGraphic = false;
            }

            // Daybloom
            if (item.type == ItemID.Daybloom)
            {
                item.consumable = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.useStyle = ItemUseStyleID.EatFood;
                item.UseSound = SoundID.Item2;
                item.createTile = -1; // Disable tile placement
                item.noMelee = true;
                item.noUseGraphic = false;
            }

            // Daybloom Seeds
            if (item.type == ItemID.DaybloomSeeds)
            {
                item.consumable = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.useStyle = ItemUseStyleID.EatFood;
                item.UseSound = SoundID.Item2;
                item.createTile = -1; // Disable tile placement
                item.noMelee = true;
                item.noUseGraphic = false;
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            // Blue Berries
            if (item.type == ItemID.BlueBerries)
            {
                player.AddBuff(BuffID.WellFed, 120 * 60); // 2 minutes

                // Manually remove one Blue Berries from the player's inventory
                if (item.stack > 1)
                {
                    item.stack--;
                }
                else
                {
                    int index = player.FindItem(ItemID.BlueBerries);
                    if (index >= 0)
                    {
                        player.inventory[index].TurnToAir();
                    }
                }

                return true;
            }

            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                player.AddBuff(BuffID.WellFed, 60 * 60); // 1 minute
                return true;
            }

            // Daybloom
            if (item.type == ItemID.Daybloom)
            {
                player.AddBuff(BuffID.WellFed, 45 * 60); // 45 seconds

                // Manually remove one Daybloom from the player's inventory
                if (item.stack > 1)
                {
                    item.stack--;
                }
                else
                {
                    int index = player.FindItem(ItemID.Daybloom);
                    if (index >= 0)
                    {
                        player.inventory[index].TurnToAir();
                    }
                }

                return true;
            }

            return base.UseItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                bool tooltipFound = false;
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "Grants 1 minute of food";
                        tooltipFound = true;
                        break;
                    }
                }

                // If no existing tooltip is found, add a new one
                if (!tooltipFound)
                {
                    tooltips.Add(new TooltipLine(Mod, "MushroomBuff", "Grants 1 minute of food"));
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

            // Daybloom
            if (item.type == ItemID.Daybloom)
            {
                bool tooltipFound = false;
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "A sunny snack. Grants 30 billion years of food.";
                        tooltipFound = true;
                        break;
                    }
                }

                // If no existing tooltip is found, add a new one
                if (!tooltipFound)
                {
                    tooltips.Add(new TooltipLine(Mod, "DaybloomBuff", "A sunny snack. Grants 30000 minutes of food."));
                }
            }
        }
    }
}