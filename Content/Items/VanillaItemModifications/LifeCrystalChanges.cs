using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerCrystal = 10;

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
                player.statLifeMax += LifePerCrystal - defaultIncrease;

                // Ensure player's max health does not exceed 250
                if (player.statLifeMax > 250)
                {
                    player.statLifeMax = 250;
                }
            }
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                item.healLife = LifePerCrystal; // Modify healing effect to 10 HP
                item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (player.statLifeMax > 250)
            {
                player.statLifeMax = 250;
            }
        }
    }
}
