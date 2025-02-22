using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace BetterThanSlimes.Common.VanillaItemChanges.Tools
{
    public class BlueBerries : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.BlueBerries)
            {
                // Make the mushroom consumable and set its use properties
                item.consumable = true;
                item.useTime = 15; // Time in frames to consume the item
                item.useAnimation = 15; // Animation time in frames
                item.useStyle = ItemUseStyleID.EatFood; // Use the eating animation
                item.UseSound = SoundID.Item2; // Play the eating sound
            }
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.BlueBerries)
            {
                // Apply the Well Fed buff for 60 seconds (3600 ticks)
                player.AddBuff(BuffID.WellFed, 120 * 60); //first number is seconds of buff
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.BlueBerries)
            {
                // Find the existing tooltip line (if any) and modify it
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0") // Check for the default tooltip line
                    {
                        line.Text = "The juice gets everywhere. Much on them to satiate yourself for 2 minutes."; // Change the tooltip text
                        break;
                    }
                }
            }
        }
    }
}