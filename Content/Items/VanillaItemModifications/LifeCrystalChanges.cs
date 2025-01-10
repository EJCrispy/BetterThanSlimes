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

                // Remove the default HP increase
                player.statLifeMax2 -= defaultIncrease;
                player.statLife -= defaultIncrease;

                // Apply the custom HP increase
                player.statLifeMax2 += LifePerFruit;
                player.statLife += LifePerFruit;

                // Ensure player's current health doesn't exceed max health
                if (player.statLife > player.statLifeMax2)
                {
                    player.statLife = player.statLifeMax2;
                }

                // Ensure player's max health does not exceed 500
                if (player.statLifeMax2 > 500)
                {
                    player.statLifeMax2 = 500;
                    player.statLife = player.statLifeMax2;
                }
            }
        }
    }
}
