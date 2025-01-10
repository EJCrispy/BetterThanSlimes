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

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.statLifeMax < 500)
            {
                player.statLifeMax2 += LifePerFruit;
                player.statLife += LifePerFruit;
                return true;
            }
            return false;
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax < 500)
            {
                player.statLifeMax += LifePerFruit;
                player.statLife += LifePerFruit;
                Main.NewText("Increased max life by 10!", 255, 240, 20);
            }
        }
    }
}
