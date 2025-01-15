using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;

namespace BetterThanSlimes.Common.CustomUI
{
    public class MyMod : ModSystem
    {
        public UserInterface GuidebookUI;

        public override void Load()
        {
            if (!Main.dedServ) // Only load UI on the client
            {
                GuidebookUI = new UserInterface();
                GuidebookUI.SetState(new GuidebookUIState());
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            GuidebookUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (index != -1 && GuidebookUI?.CurrentState != null)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "MyMod: Guidebook",
                    delegate
                    {
                        GuidebookUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}