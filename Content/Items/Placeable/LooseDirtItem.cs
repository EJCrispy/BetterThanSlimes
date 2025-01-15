using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class LooseDirtItem : ModItem
{
    public override void SetStaticDefaults()
    {
        
    }

    public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 12;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<LooseDirtTile>();
    }
}
