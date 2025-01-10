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

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax >= MaxLifeCap)
            {
                return false; // Prevent using Life Crystal if max life is at or above 250
            }
            return base.CanUseItem(item, player);
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                // Reduce the player's max health by 10 (instead of increasing by 20)
                player.statLifeMax -= 10;

                // Ensure player's max health does not exceed 250
                if (player.statLifeMax > MaxLifeCap)
                {
                    player.statLifeMax = MaxLifeCap;
                }

                // Ensure player's current health does not exceed max health
                if (player.statLife > player.statLifeMax)
                {
                    player.statLife = player.statLifeMax;
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
    }
}
