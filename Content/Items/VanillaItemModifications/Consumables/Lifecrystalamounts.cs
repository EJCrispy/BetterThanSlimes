using Terraria;
using Terraria.ModLoader;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Mono.Cecil;

namespace BetterThanSlimes.Content.Items.VanillaItemModifications.Consumables
{
    public class LifeCrystalDetour : GlobalItem
    {
        private ILHook ilHook;

        public override bool InstancePerEntity => true;

        public override void Load()
        {
            ilHook = new ILHook(
                typeof(Player).GetMethod("ConsumeItem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public),
                new ILContext.Manipulator(ConsumeItemDetour)
            );
        }

        public override void Unload()
        {
            ilHook?.Dispose();
        }

        private void ConsumeItemDetour(ILContext il)
        {
            var c = new ILCursor(il);

            // Find the spot where the game sets the player's max life
            if (c.TryGotoNext(i => i.MatchLdcI4(20)))
            {
                c.Next.Operand = 10; // Change 20 to 10
            }
        }
    }
}
