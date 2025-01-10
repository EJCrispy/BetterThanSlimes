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
            if (item.type == ItemID.LifeCrystal && player.statLifeMax < 500)
            {
                player.statLifeMax += LifePerFruit;
                player.statLife += LifePerFruit;

                // Reduce the default increase by the difference (10) to give net increase of 10
                player.statLifeMax -= 10;
                player.statLife -= 10;
            }
        }
    }
}
