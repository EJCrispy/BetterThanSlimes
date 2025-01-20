using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace BetterThanSlimes.Common.VanillaItemChanges.Consumables
{
    public class LifeCrystalDetour : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 >= 500)
            {
                return false; // Prevent usage if max life is 500 or more, I dont know if this is needed but i'm too scared to change it.
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
        public int lifeCrystalsUsed = 10; // this just makes people start with 100 hp, if it isn't set to 10 peoople spawn with that many life crystals of hp

        public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
        {
            base.ModifyMaxStats(out health, out mana);
            health = health.CombineWith(new StatModifier(1f, 0f, lifeCrystalsUsed * 10)); // Combine base health with Life Crystal bonus, pretty simple
        }

        public override void SaveData(TagCompound tag)
        {
            tag["lifeCrystalsUsed"] = lifeCrystalsUsed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("lifeCrystalsUsed"))
            {
                lifeCrystalsUsed = tag.GetInt("lifeCrystalsUsed"); // this is just to make everything fit btw
            }
            else
            {
                lifeCrystalsUsed = 0;
            }
        }
    }
}
