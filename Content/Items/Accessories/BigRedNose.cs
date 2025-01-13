using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

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
            Item.rare = ItemRarityID.Blue; // Set the rarity to blue
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

        public override void OnHurt(Player.HurtInfo info)
        {
            if (bigRedNose && info.Damage > 30)
            {
                Vector2 spawnPosition = Player.position;
                spawnPosition.Y -= 45; // Adjust the value as needed

                // Create the explosion projectile
                int proj = Projectile.NewProjectile(Player.GetSource_Misc("BigRedNoseExplosion"), spawnPosition, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 70, 10, Player.whoAmI);

                // Set local immunity for the projectile
                Main.projectile[proj].usesLocalNPCImmunity = true;
                Main.projectile[proj].localNPCHitCooldown = 20; // Adjust the cooldown value as needed
            }
        }
    }

    public class MyGlobalNPC : GlobalNPC
    {
        private Dictionary<int, int> npcImmunity = new Dictionary<int, int>();

        public override bool InstancePerEntity => true;

        public override void OnKill(NPC npc)
        {
            if (npc.life <= 0 && !npc.friendly && npc.catchItem <= 0 && npc.type != NPCID.Bee)
            {
                Player player = Main.player[npc.lastInteraction];
                if (player.GetModPlayer<MyPlayer>().bigRedNose)
                {
                    // Adjust the y-coordinate to spawn the projectile higher
                    Vector2 spawnPosition = npc.position;
                    spawnPosition.Y -= 45; // Adjust the value as needed

                    // Create the explosion projectile
                    int proj = Projectile.NewProjectile(npc.GetSource_Death(), spawnPosition, Vector2.Zero, ProjectileID.DaybreakExplosion, 35, 10, player.whoAmI);

                    // Set local immunity for the projectile
                    Main.projectile[proj].usesLocalNPCImmunity = true;
                    Main.projectile[proj].localNPCHitCooldown = 20; // Adjust the cooldown value as needed
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);

            // Update the immunity frames for each NPC
            foreach (int npcIndex in npcImmunity.Keys.ToArray())
            {
                if (npcImmunity[npcIndex] > 1)
                {
                    npcImmunity[npcIndex]--;
                }
                else
                {
                    npcImmunity.Remove(npcIndex);
                }
            }
        }
    }
}
