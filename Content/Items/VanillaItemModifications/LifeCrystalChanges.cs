using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerCrystal = 10;
        public const int MaxLifeCap = 250;
        private bool customIncreaseApplied = false; // Flag to track custom increase

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax >= MaxLifeCap)
            {
                return false; // Prevent using Life Crystal if max life is at or above 250
            }
            return base.CanUseItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                if (!customIncreaseApplied)
                {
                    // Temporarily negate the default increase
                    player.statLifeMax2 -= 20;
                    player.statLifeMax2 += LifePerCrystal; // Apply custom increase
                    customIncreaseApplied = true; // Set flag to prevent re-application

                    // Ensure player's max health does not exceed 250
                    if (player.statLifeMax2 > MaxLifeCap)
                    {
                        player.statLifeMax2 = MaxLifeCap;
                    }

                    // Ensure player's current health does not exceed max health
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }

                    return true; // Indicate that the item was successfully used
                }
            }
            return base.UseItem(item, player); // Default behavior for other items
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            // Reset the flag to allow future Life Crystal uses
            customIncreaseApplied = false;
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                item.healLife = LifePerCrystal; // Modify healing effect to 10 HP
            }
        }
    }
}
