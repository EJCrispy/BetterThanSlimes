using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Items.Accessories
{
    public class Kapala : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Localization handled through localization file
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 1);
            Item.rare = ItemRarityID.Green; // You can change the rarity if desired
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 1; // Grants an additional summon slot
            player.statLifeMax2 += 30; // Increases maximum life by 30
            player.GetModPlayer<KapalaPlayer>().kapalaEquipped = true;
        }
    }

    public class KapalaPlayer : ModPlayer
    {
        public bool kapalaEquipped = false;

        public override void ResetEffects()
        {
            kapalaEquipped = false;
        }

        public void HealOnKill(int healAmount)
        {
            Player.statLife += healAmount;
            Player.HealEffect(healAmount);
        }
    }

    public class ExampleGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (Main.LocalPlayer.GetModPlayer<KapalaPlayer>().kapalaEquipped)
            {
                Main.LocalPlayer.GetModPlayer<KapalaPlayer>().HealOnKill(10);
            }
        }
    }
}
