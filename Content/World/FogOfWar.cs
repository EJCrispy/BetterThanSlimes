using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes
{
    public class BlackoutPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            // Apply the Blackout debuff (Buff ID 80) to the player constantly
            Player.AddBuff(80, 2); // 2 frames duration ensures it's constantly reapplied
        }
    }
}