using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

public class BigRedNose : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.value = 10000;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }
}

public class BigRedNoseGlobalNPC : GlobalNPC
{
    public override void OnKill(NPC npc)
    {
        if (Main.player[Main.myPlayer].GetModPlayer<MyModPlayer>().HasBigRedNose)
        {
            Projectile.NewProjectile(null, npc.position, new Microsoft.Xna.Framework.Vector2(0, 0), ModContent.ProjectileType<DD2ExplosiveTrapT1Explosion>(), npc.damage, 0, Main.myPlayer);
        }
    }
}

public class DD2ExplosiveTrapT1Explosion : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 3;
        Projectile.light = 1f;
        Projectile.extraUpdates = 1;
        Projectile.ignoreWater = true;
    }

    public override void AI()
    {
        // Explosion logic goes here
    }
}

public class MyModPlayer : ModPlayer
{
    public bool HasBigRedNose;

    public override void ResetEffects()
    {
        HasBigRedNose = false;
    }

    public override void UpdateEquips()
    {
        if (Player.HasItem(ModContent.ItemType<BigRedNose>()))
        {
            HasBigRedNose = true;
        }
    }
}
