using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Common.Players
{
    public class PlayerTraits : ModPlayer
    {
        // This section of code reduces base player movement speed, acceleration speed, max jump height, and jump speed.
        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();
            Player.maxRunSpeed -= 0.925f;
            Player.accRunSpeed -= 0.925f;
            Player.jumpHeight -= 7;
            Player.maxFallSpeed -= -10;
            Player.jumpSpeed -= -0.0075f;

            // New code to decrease building range by 2 tiles.
            Player.tileRangeX -= 2;
            Player.tileRangeY -= 2;

            // New code to decrease building speed.
            Player.tileSpeed += 1.6f; // 
        }
    }
}
