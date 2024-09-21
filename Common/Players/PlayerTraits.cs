using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


//This section of code reduces base player movement speed, acceleration speed, max jump height, and jump speed.
namespace TutorialMod.Common.Players
{
    public class PlayerTraits : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();
            Player.maxRunSpeed-= 1.325f;
            Player.accRunSpeed-= 1.325f;
            Player.jumpHeight -= 20;
            Player.maxFallSpeed -= -10;
            Player.jumpSpeed -= -0.0075f;
        }
    }
}