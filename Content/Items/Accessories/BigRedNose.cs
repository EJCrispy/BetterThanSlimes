using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Accessories
{
    public class BigRedNose : ModItem
    {
        public override void SetStaticDefaults()
        {
            // The display name and tooltip will be set using the .lang file
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 1);
            Item.accessory = true;
            Item.vanity = false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().bigRedNose = true;
        }
    }

    public class MyPlayer : ModPlayer
    {
        public bool bigRedNose;

        public override void ResetEffects()
        {
            bigRedNose = false;
        }
    }

    public class MyGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.life <= 0)
            {
                Player player = Main.player[npc.lastInteraction];
                if (player.GetModPlayer<MyPlayer>().bigRedNose)
                {
                    Projectile.NewProjectile(npc.GetSource_Death(), npc.position, npc.velocity, ProjectileID.DD2ExplosiveTrapT1Explosion, 0, 0, player.whoAmI);
                }
            }
        }
    }
}
