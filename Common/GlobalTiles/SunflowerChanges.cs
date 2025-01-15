using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.GlobalTiles
{
    public class SunflowerChanges : ModPlayer
    {
        public override void PreUpdateBuffs()
        {
            Player.ClearBuff(BuffID.Sunflower);
        }
            // Remove the Happy buff from the player permanently
        }
    }

    public class RemoveHappyEffect : GlobalTile
    {
        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (type == TileID.Sunflower)
            {
                // Do nothing - this prevents the Sunflower from applying the Happy buff.
                return;
            }
        }
    }