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

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                // Temporarily reduce max life to negate the default increase
                player.statLifeMax2 -= 20;

                // Apply our desired max health increase
                player.statLifeMax2 += LifePerCrystal;

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
            return base.UseItem(item, player); // Default behavior for other items
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 >= MaxLifeCap)
            {
                return false; // Prevent using Life Crystal if max life is at or above 250
            }
            return base.CanUseItem(item, player);
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
