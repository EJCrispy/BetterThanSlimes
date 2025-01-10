using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications.Consumables
{
    public class LifeCrystalDetour : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 >= 500)
            {
                return false; // Prevent usage if max life is 500 or more
            }
            return base.CanUseItem(item, player);
        }

        public override void OnConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 < 500)
            {
                player.GetModPlayer<LifeCrystalModPlayer>().lifeCrystalsUsed++;
            }
        }
    }

    public class LifeCrystalModPlayer : ModPlayer
    {
        public int lifeCrystalsUsed = 0;

        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            base.ModifyMaxStats(out health, out mana);
            health = new StatModifier(1f, 0f, lifeCrystalsUsed * 10); // Increase max health by 10 per Life Crystal used
        }
    }
}
