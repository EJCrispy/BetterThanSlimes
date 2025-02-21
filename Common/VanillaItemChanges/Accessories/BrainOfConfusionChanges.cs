using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;

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

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Check if the item is the Brain of Confusion
            if (item.type == ItemID.BrainOfConfusion)
            {
                // Create a new tooltip line
                TooltipLine newTooltip = new TooltipLine(Mod, "BrainOfConfusionDebuff", "When hit, you now become confused and distorted");

                // Add the new tooltip line to the list of tooltips
                tooltips.Add(newTooltip);
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
                Player.AddBuff(BuffID.VortexDebuff, 18); // Vortex Debuff for 18 frames
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
                Player.AddBuff(BuffID.VortexDebuff, 18); // Vortex Debuff for 18 frames
            }
            else
            {
                return;
            }
        }
    }
}