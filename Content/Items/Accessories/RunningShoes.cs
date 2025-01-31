using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Items.Accessories
{
    public class RunningShoes : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Check if the player has been moving horizontally for a short time.
            if (player.velocity.X > 0.5f || player.velocity.X < -0.5f)
            {
                player.GetModPlayer<RunningShoesPlayer>().runningTimer++;
            }
            else
            {
                player.GetModPlayer<RunningShoesPlayer>().runningTimer = 0;
            }

            // Activate the speed boost after a short delay of running.
            if (player.GetModPlayer<RunningShoesPlayer>().runningTimer > 30) // ~0.5 seconds
            {
                player.moveSpeed += 0.04f; // 2/3 of Hermes Boots effect
            }
        }
    }

    public class RunningShoesPlayer : ModPlayer
    {
        public int runningTimer;

        public override void ResetEffects()
        {
            runningTimer = 0;
        }
    }
}
