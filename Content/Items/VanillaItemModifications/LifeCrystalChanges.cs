using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int MaxExampleLifeFruits = 10;
        public static readonly int LifePerFruit = 10;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
        }

        public override bool CanUseItem(Item item, Player player)
        {
            // Check if the player's max health is below the maximum allowed health.
            if (player.statLifeMax < (500 + MaxExampleLifeFruits * LifePerFruit))
            {
                // Increase the player's max health by LifePerFruit when the item is used.
                player.statLifeMax2 += LifePerFruit;
                return true;
            }
            return false;
        }
    }
}
