using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace YourModName.Items
{
    public class ModifiedHeartHairpin : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.HeartHairpin)
            {
                item.accessory = true;
                item.rare = ItemRarityID.Blue;
                item.value = Item.sellPrice(silver: 50);
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.HeartHairpin)
            {
                player.lifeRegen += 1; // Half of Band of Regeneration's 2 lifeRegen
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.HeartHairpin)
            {
                // Remove the "Vanity" tooltip line by its identifier
                for (int i = 0; i < tooltips.Count; i++)
                {
                    // The "Vanity" tooltip line has the Name "Social"
                    if (tooltips[i].Name == "Social" && tooltips[i].Mod == "Terraria")
                    {
                        tooltips.RemoveAt(i); // Remove the vanity line
                        break; // Exit the loop after removal
                    }
                }

                // Add a new tooltip line for the life regen effect
                TooltipLine regenLine = new TooltipLine(Mod, "LifeRegen", "Very slowly regenerates life");
                tooltips.Add(regenLine);
            }
        }
    }
}