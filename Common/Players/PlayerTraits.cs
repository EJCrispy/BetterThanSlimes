using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BetterThanSlimes.Common.Players
{
    public class PlayerTraits : ModPlayer
    {
        public bool slimeCoreConsumed;

        // This section of code reduces base player movement speed, acceleration speed, max jump height, and jump speed.
        public override void PostUpdateMiscEffects()
        {
            base.PostUpdateMiscEffects();

            if (slimeCoreConsumed)
            {
                Player.maxRunSpeed -= 1.425f;
                Player.accRunSpeed -= 1.425f;
                Player.jumpHeight -= 5;
                Player.maxFallSpeed -= -10;
                Player.jumpSpeed -= -0.0075f;

                // decrease ALL range by 2 tiles (make sure to play around this)
                Player.tileRangeX -= 2;
                Player.tileRangeY -= 2;

                // decrease building speed.
                Player.tileSpeed += 2f;
            }
            else
            {
                Player.maxRunSpeed -= 1.925f;
                Player.accRunSpeed -= 1.925f;
                Player.jumpHeight -= 10;
                Player.maxFallSpeed -= -10;
                Player.jumpSpeed -= -0.0075f;

                // decrease ALL range by 2 tiles (make sure to play around this)
                Player.tileRangeX -= 2;
                Player.tileRangeY -= 2;

                // decrease building speed.
                Player.tileSpeed += 2f;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["slimeCoreConsumed"] = slimeCoreConsumed;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("slimeCoreConsumed"))
            {
                slimeCoreConsumed = tag.GetBool("slimeCoreConsumed");
            }
        }
    }
}
