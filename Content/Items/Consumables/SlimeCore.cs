using BetterThanSlimes.Common.Players;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;


namespace BetterThanSlimes.Content.Items.Consumables;
public class SlimeCore : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.useStyle = ItemUseStyleID.EatFood;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item2;
        Item.rare = ItemRarityID.Expert;
        Item.consumable = true;
        Item.maxStack = 1;
    }

    public override bool? UseItem(Player player)
    {
        var modPlayer = player.GetModPlayer<PlayerTraits>();
        if (!modPlayer.slimeCoreConsumed)
        {
            modPlayer.slimeCoreConsumed = true;
            Player.jumpHeight += 4;
            player.maxRunSpeed += 0.5f;
            player.accRunSpeed += 0.5f;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, player.whoAmI);
            }
            return true;
        }
        return false;
    }
}