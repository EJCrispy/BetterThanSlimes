using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Common.VanillaItemChanges
{
    public class FallenStarLight : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.FallenStar;
        }

        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);

            item.useTime = 70;
            item.useAnimation = 70;
            item.autoReuse = true; // Enable autofire
        }

        // Emit light when the item is used
        public override bool? UseItem(Item item, Player player)
        {
            if (AppliesToEntity(item, false))
            {
                Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f); // White light
            }
            return base.UseItem(item, player);
        }
    }
}
