using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.VanillaItemChanges.Accessories
{
    internal class BrainOfConfusionChanges : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            // Check if the item is the Brain of Confusion
            if (item.type == ItemID.BrainOfConfusion)
            {
                // Enable the Brain of Confusion's dodge effect
                player.onHitDodge = true;

                // Track whether the player is wearing the Brain of Confusion
                player.GetModPlayer<BrainOfConfusionPlayer>().hasBrainOfConfusion = true;
            }
        }
    }

    public class BrainOfConfusionPlayer : ModPlayer
    {
        public bool hasBrainOfConfusion;

        public override void ResetEffects()
        {
            // Reset the flag every frame
            hasBrainOfConfusion = false;
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            // Check if the player is wearing the Brain of Confusion and the dodge procs
            if (hasBrainOfConfusion && Player.onHitDodge)
            {
                // Apply debuffs to the player when the dodge procs
                Player.AddBuff(BuffID.Confused, 25); // Confused debuff for 1 second (60 frames)
                Player.AddBuff(BuffID.VortexDebuff, 18);

            }
            else
            {
                return;
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            // Check if the player is wearing the Brain of Confusion and the dodge procs
            if (hasBrainOfConfusion && Player.onHitDodge)
            {
                // Apply debuffs to the player when the dodge procs
                Player.AddBuff(BuffID.Confused, 25); // Confused debuff for 1 second (60 frames)
                Player.AddBuff(BuffID.VortexDebuff, 18);
            }
            else
            {
                return;
            }
        }
    }
}