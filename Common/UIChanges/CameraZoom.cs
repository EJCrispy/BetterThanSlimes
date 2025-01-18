using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace BetterThanSlimes
{
    public class CameraZoom : ModSystem
    {
        public override void PreUpdatePlayers()
        {
            Main.GameZoomTarget = 2f;
        }
    }
}
