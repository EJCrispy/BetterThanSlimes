using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int MaxExampleLifeFruits = 15;
        public static readonly int LifePerCrystal = 10;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal; // Apply to Life Crystal only
        }

        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (player.statLifeMax < (500 + MaxExampleLifeFruits * LifePerCrystal))
            {
                player.UseHealthMaxIncreasingItem(LifePerCrystal); // Increase max health on consumption
            }
        }
    }
}