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
        }

        // Emit light when the item is in the player's inventory
        public override void UpdateInventory(Item item, Player player)
        {
            if (AppliesToEntity(item, false))
            {
                // Adding white light around the player holding the item
                Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
