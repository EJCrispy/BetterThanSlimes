using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using System.Reflection;
using System;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerCrystal = 10;
        public const int MaxLifeCap = 200;

        private static Hook _consumeItemHook;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override void Load()
        {
            MethodInfo method = typeof(Player).GetMethod("ConsumeItem", BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
            {
                _consumeItemHook = new Hook(method, new ConsumeItemDelegate(Player_ConsumeItem));
            }
            else
            {
                throw new Exception("Failed to find method: ConsumeItem");
            }
        }

        public override void Unload()
        {
            _consumeItemHook?.Dispose();
        }

        private delegate void ConsumeItemDelegate(Player self, int type);

        private void Player_ConsumeItem(Player self, int type)
        {
            if (type == ItemID.LifeCrystal)
            {
                var modPlayer = self.GetModPlayer<MyModPlayer>();

                if (modPlayer.MaxLife < MaxLifeCap)
                {
                    modPlayer.MaxLife += LifePerCrystal - 20; // Increase max life by 10 instead of 20

                    if (modPlayer.MaxLife > MaxLifeCap)
                    {
                        modPlayer.MaxLife = MaxLifeCap;
                    }

                    if (self.statLife > modPlayer.MaxLife)
                    {
                        self.statLife = modPlayer.MaxLife;
                    }
                }
            }
            else
            {
                typeof(Player).GetMethod("ConsumeItem", BindingFlags.Instance | BindingFlags.Public).Invoke(self, new object[] { type });
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            var modPlayer = player.GetModPlayer<MyModPlayer>();

            if (item.type == ItemID.LifeCrystal && modPlayer.MaxLife >= MaxLifeCap)
            {
                return false; // Prevent using Life Crystal if max life is at or above 200
            }
            return base.CanUseItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                return true; // Indicate that the item was successfully used
            }
            return base.UseItem(item, player); // Default behavior for other items
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                item.healLife = LifePerCrystal; // Modify healing effect to 10 HP
            }
        }
    }

    public class MyModPlayer : ModPlayer
    {
        public int MaxLife { get; set; }

        public override void ResetEffects()
        {
            MaxLife = Player.statLifeMax2;
        }

        public override void UpdateDead()
        {
            MaxLife = Player.statLifeMax2;
        }
    }
}
