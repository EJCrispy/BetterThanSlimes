using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;

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
                player.statLifeMax2 += 10; // Directly increase max life by 10
                player.statLife += 10; // Increase current life by 10
                if (Main.myPlayer == player.whoAmI)
                {
                    player.HealEffect(10, true); // Show the healing effect
                }
            }
        }
    }

    public class LifeCrystalModPlayer : ModPlayer
    {
        public int lifeCrystalsUsed = 0;

        public override void SaveData(TagCompound tag)
        {
            tag["lifeCrystalsUsed"] = lifeCrystalsUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("lifeCrystalsUsed"))
            {
                lifeCrystalsUsed = tag.GetInt("lifeCrystalsUsed");
            }
            else
            {
                lifeCrystalsUsed = 0;
            }
        }
    }
}
