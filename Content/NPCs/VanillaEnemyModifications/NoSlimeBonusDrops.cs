using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System.Reflection;
using System;

namespace BetterThanSlimes.Common.VanillaNPCChanges
{
    public class SlimeGlobalNPC : GlobalNPC
    {
        private static ILHook _slimeAIHook;

        public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
        {
            // Only apply to slimes with base ID 1
            return npc.type == NPCID.BlueSlime || npc.type == NPCID.GreenSlime; // Add other slime types as needed
        }

        public override void Load()
        {
            // Find the AI_001_Slimes method
            MethodInfo method = typeof(NPC).GetMethod("AI_001_Slimes", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                // Create an IL hook for the method
                _slimeAIHook = new ILHook(method, ModifySlimeAI);
            }
            else
            {
                throw new Exception("Failed to find method: AI_001_Slimes");
            }
        }

        public override void Unload()
        {
            // Dispose of the hook when the mod is unloaded
            _slimeAIHook?.Dispose();
        }

        private void ModifySlimeAI(ILContext il)
        {
            var c = new ILCursor(il);

            // Locate the part of the method where the bonus drop chance is checked
            // This will depend on the specific IL code in the AI_001_Slimes method
            // For example, look for a comparison with 0.05 (5% chance)
            if (c.TryGotoNext(MoveType.Before, x => x.MatchLdcR4(0.05f)))
            {
                // Replace the 5% chance with 0% chance
                c.Next.Operand = 0f; // Set the chance to 0%
            }
            else
            {
                throw new Exception("Failed to find the bonus drop chance check in AI_001_Slimes");
            }
        }
    }
}