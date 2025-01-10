using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Content.Items.Weapons
{
    public class BlackSteel : ModItem
    {
        public override void SetDefaults()
        {
            // Common Properties
            Item.rare = ItemRarityID.Blue;
            Item.value = 40464;
            Item.maxStack = 1;

            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = false;

            // Weapon Properties
            Item.damage = 15;
            Item.crit = 96;
            Item.knockBack = 2;
            Item.noUseGraphic = false;
            Item.noMelee = false;
            Item.DamageType = DamageClass.Melee;
        }

        public override void HoldItem(Player player)
        {
            // Calculate the bonus damage from critical strike chance above 100%
            float critChance = player.GetCritChance(DamageClass.Melee);
            if (critChance > 100)
            {
                float extraCritChance = critChance - 100;
                float bonusDamage = extraCritChance / 100f;
                player.GetDamage(DamageClass.Melee) += bonusDamage;

                // Debug output
                Main.NewText("Crit Chance: " + critChance + "%");
                Main.NewText("Bonus Damage: " + bonusDamage * 100 + "%");
            }
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            // Limit the critical strike chance to 100%
            if (crit > 100)
            {
                crit = 100;
            }
        }
    }
}

namespace BetterThanSlimes.Content
{
    public class GlobalNPCMod : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            // Check if the NPC was killed by your custom weapon and is not a critter
            if (npc.catchItem <= 0 && player.HeldItem.type == ModContent.ItemType<Items.Weapons.BlackSteel>())
            {
                int projectileID = ModContent.ProjectileType<Projectiles.VengefulSpirit>();
                Vector2 spawnPosition = npc.Center;
                Projectile.NewProjectile(npc.GetSource_Death(), spawnPosition, Vector2.Zero, projectileID, 15, 2, Main.myPlayer);
            }
        }
    }
}
