using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications.Consumables
{
    public class LifeCrystalDetour : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                item.healLife = 10; // Change the heal amount to 10
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 < 500)
            {
                player.statLifeMax2 += 10; // Increase max life by 10
                player.statLife += 10; // Also increase current life by 10
                if (Main.myPlayer == player.whoAmI)
                {
                    player.HealEffect(10, true); // Show the healing effect
                }
                return true; // Prevent default behavior
            }
            return base.UseItem(item, player);
        }
    }
}
