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
                _consumeItemHook = new Hook(method, Player_ConsumeItem);
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

        private void Player_ConsumeItem(Action<Player, int> orig, Player self, int type)
        {
            if (type == ItemID.LifeCrystal && self.statLifeMax < MaxLifeCap)
            {
                self.statLifeMax += LifePerCrystal - 20;

                if (self.statLifeMax > MaxLifeCap)
                {
                    self.statLifeMax = MaxLifeCap;
                }

                if (self.statLife > self.statLifeMax)
                {
                    self.statLife = self.statLifeMax;
                }
            }
            else
            {
                orig(self, type);
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax >= MaxLifeCap)
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                return true;
            }
            return base.UseItem(item, player);
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.LifeCrystal)
            {
                item.healLife = LifePerCrystal;
            }
        }
    }
}
