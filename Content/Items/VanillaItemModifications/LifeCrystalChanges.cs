using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerCrystal = 10;
        public const int MaxLifeCap = 250;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                // Apply our desired max health increase directly
                player.statLifeMax2 += LifePerCrystal - 20;

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

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax >= MaxLifeCap)
            {
                return false; // Prevent using Life Crystal if max life is at or above 250
            }
            return base.CanUseItem(item, player);
        }
    }
}
