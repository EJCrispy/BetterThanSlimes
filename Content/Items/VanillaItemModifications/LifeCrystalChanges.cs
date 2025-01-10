using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerFruit = 10;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                int defaultIncrease = 20; // Default HP increase by Life Crystal

                // Adjust player's max health
                player.statLifeMax2 += LifePerFruit - defaultIncrease;

                // Ensure player's max health does not exceed 500
                if (player.statLifeMax2 > 500)
                {
                    player.statLifeMax2 = 500;
                }
            }
        }
    }
}
