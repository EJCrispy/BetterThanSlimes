using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace BetterThanSlimes.Content.Items.Accessories
{
    // This example attempts to showcase most of the common boot accessory effects.
    [AutoloadEquip(EquipType.Shoes)]
    public class RunningShoes : ModItem
    {
        public static readonly int MoveSpeedBonus = 1;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 1); // Equivalent to Item.buyPrice(0, 1, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // These 2 stat changes are equal to the Lightning Boots
            player.moveSpeed += MoveSpeedBonus / 40f; // Modifies the player movement speed bonus.
            player.accRunSpeed = 3.7f; // Sets the players sprint speed in boots.

            // player.maxRunSpeed and player.runAcceleration are usually not set by boots and should not be changed in UpdateAccessory due to the logic order. See ExampleStatBonusAccessoryPlayer.PostUpdateRunSpeeds for an example of adjusting those speed stats.

            // Determines whether the boots count as rocket boots
            // 0 - These are not rocket boots
            // Anything else - These are rocket boots
            player.rocketBoots = 0;

            // Sets which dust and sound to use for the rocket flight
            // 1 - Rocket Boots
            // 2 - Fairy Boots, Spectre Boots, Lightning Boots
            // 3 - Frostspark Boots
            // 4 - Terrraspark Boots
            // 5 - Hellfire Treads
            player.vanityRocketBoots = 1;

            player.waterWalk2 = false; // Allows walking on all liquids without falling into it
            player.waterWalk = false; // Allows walking on water, honey, and shimmer without falling into it
            player.iceSkate = false; // Grant the player improved speed on ice and not breaking thin ice when falling onto it
            player.desertBoots = false; // Grants the player increased movement speed while running on sand
            player.fireWalk = false; // Grants the player immunity from Meteorite and Hellstone tile damage
            player.noFallDmg = false; // Grants the player the Lucky Horseshoe effect of nullifying fall damage

            // player.DoBootsEffect(player.DoBootsEffect_PlaceFlowersOnTile); // Spawns flowers when walking on normal or Hallowed grass
        }

        public override void UpdateVanity(Player player)
        {
            // Optional: Custom vanity effects here
        }
    }
}
