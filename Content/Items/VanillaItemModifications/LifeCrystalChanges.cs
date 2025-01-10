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
                int maxHealthGain = LifePerFruit; // This is the new HP gain value
                player.statLifeMax += maxHealthGain; // Increase max health
                player.statLife += maxHealthGain; // Increase current health
                Main.NewText($"Increased max life by {maxHealthGain}!", 255, 240, 20);

                // Cap the player's max health to 500 if it exceeds the limit.
                if (player.statLifeMax > 500)
                {
                    player.statLifeMax = 500;
                    Main.NewText("Max health capped at 500!", 255, 0, 0);
                }
            }
        }
    }
}
