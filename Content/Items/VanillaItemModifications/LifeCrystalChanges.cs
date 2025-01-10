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
                int initialMaxLife = player.statLifeMax2;
                int newMaxLife = initialMaxLife + LifePerFruit;

                // Ensure the max life does not exceed 500
                if (newMaxLife > 500)
                {
                    newMaxLife = 500;
                }

                player.statLifeMax2 = newMaxLife;

                // Ensure current life does not exceed the new max life
                if (player.statLife > player.statLifeMax2)
                {
                    player.statLife = player.statLifeMax2;
                }
            }
        }
    }
}
