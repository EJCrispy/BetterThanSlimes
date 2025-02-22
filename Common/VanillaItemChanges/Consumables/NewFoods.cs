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
                item.consumable = true; // Ensure it's consumable
                item.useTime = 15;
                item.useAnimation = 15;
                item.useStyle = ItemUseStyleID.EatFood; // Set to EatFood style
                item.UseSound = SoundID.Item2; // Use the eating sound
                item.createTile = -1; // Disable tile placement
                item.noMelee = true; // Ensure it doesn't act as a melee weapon
                item.noUseGraphic = false; // Ensure the use animation is visible
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            // Blue Berries
            if (item.type == ItemID.BlueBerries)
            {
                // Apply the Well Fed buff for 2 minutes (120 seconds)
                player.AddBuff(BuffID.WellFed, 120 * 60);
                return true; // Return true to confirm consumption
            }

            // Mushroom
            if (item.type == ItemID.Mushroom)
            {
                // Apply the Well Fed buff for 1 minute (60 seconds)
                player.AddBuff(BuffID.WellFed, 60 * 60);
                return true; // Return true to confirm consumption
            }

            return base.UseItem(item, player);
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