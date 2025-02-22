using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.VanillaItemChanges.Tools
{
    public class Mushroom : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Mushroom)
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
            if (item.type == ItemID.Mushroom)
            {
                // Apply the Well Fed buff for 60 seconds (3600 ticks)
                player.AddBuff(BuffID.WellFed, 60 * 60);
            }
        }
    }
}