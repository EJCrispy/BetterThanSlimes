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
        //This section of code reduces base player movement speed, acceleration speed, max jump height, and jump speed.
        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();
            Player.maxRunSpeed -= 1.125f;
            Player.accRunSpeed -= 1.125f;
            Player.jumpHeight -= 3;
            Player.maxFallSpeed -= -10;
            Player.jumpSpeed -= -1.0195f;

        }
    }
}