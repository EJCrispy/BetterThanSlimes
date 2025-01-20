using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil.Cil; // Add this directive for OpCodes
using System.Reflection;
using System;

namespace BetterThanSlimes.Common.VanillaItemChanges.Consumables
{
    public class LifeCrystalGlobalItem : GlobalItem
    {
        public static readonly int LifePerCrystal = 10; // this shit doesn't work lol, see LifeCrystalAmounts.cs for the actual code. This .cs is literally only for the cap.
        public const int MaxLifeCap = 200;

        private static ILHook _consumeItemILHook;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.LifeCrystal;
        }

        public override void Load()
        {
            MethodInfo method = typeof(Player).GetMethod("ConsumeItem", BindingFlags.Instance | BindingFlags.Public);
            if (method != null)
            {
                _consumeItemILHook = new ILHook(method, Player_ConsumeItemIL);
            }
            else
            {
                throw new Exception("Failed to find method: ConsumeItem");
            }
        }

        public override void Unload()
        {
            _consumeItemILHook?.Dispose();
        }

        private void Player_ConsumeItemIL(ILContext il)
        {
            var c = new ILCursor(il);
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldarg_1);
            c.EmitDelegate<Action<Player, int>>((self, type) =>
            {
                if (type == ItemID.LifeCrystal && self.statLifeMax2 < MaxLifeCap)
                {
                    self.statLifeMax2 += LifePerCrystal - 20; //again, this shit is useless

                    if (self.statLifeMax2 > MaxLifeCap)
                    {
                        self.statLifeMax2 = MaxLifeCap;
                    }

                    if (self.statLife > self.statLifeMax2)
                    {
                        self.statLife = self.statLifeMax2;
                    }
                }
            });
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.statLifeMax2 >= MaxLifeCap) // no worky
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
