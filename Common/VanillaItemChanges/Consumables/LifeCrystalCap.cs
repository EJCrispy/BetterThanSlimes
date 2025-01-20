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
        public const int MaxLifeCrystalsUsed = 20; // Define the max life crystals used
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
                if (type == ItemID.LifeCrystal && self.GetModPlayer<ModPlayerWithLifeCrystals>().lifeCrystalsUsed < MaxLifeCrystalsUsed)
                {
                    self.GetModPlayer<ModPlayerWithLifeCrystals>().lifeCrystalsUsed++;
                }
            });
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal && player.GetModPlayer<ModPlayerWithLifeCrystals>().lifeCrystalsUsed >= MaxLifeCrystalsUsed)
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
    }

    public class ModPlayerWithLifeCrystals : ModPlayer
    {
        public int lifeCrystalsUsed = 0;

        public override void Initialize()
        {
            lifeCrystalsUsed = 0;
        }

        public override void ResetEffects()
        {
            // Any reset logic if needed
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            ModPlayerWithLifeCrystals clone = targetCopy as ModPlayerWithLifeCrystals;
            clone.lifeCrystalsUsed = lifeCrystalsUsed;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            var packet = Mod.GetPacket();
            packet.Write((byte)ModMessageType.SyncPlayer);
            packet.Write((byte)Player.whoAmI);
            packet.Write(lifeCrystalsUsed);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            var modPlayer = clientPlayer as ModPlayerWithLifeCrystals;
            if (modPlayer.lifeCrystalsUsed != lifeCrystalsUsed)
            {
                // Send changes to the server
                var packet = Mod.GetPacket();
                packet.Write((byte)ModMessageType.SyncPlayer);
                packet.Write((byte)Player.whoAmI);
                packet.Write(lifeCrystalsUsed);
                packet.Send();
            }
        }
    }

    public enum ModMessageType : byte
    {
        SyncPlayer
    }
}
