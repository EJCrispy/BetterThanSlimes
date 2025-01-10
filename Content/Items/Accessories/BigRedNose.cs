using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Add this line for Vector2
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
    }

    public class MyGlobalNPC : GlobalNPC
    {
        private Dictionary<int, int> npcImmunity = new Dictionary<int, int>();

        public override bool InstancePerEntity => true; // Add this line

        public override void OnKill(NPC npc)
        {
            if (npc.life <= 0)
            {
                Player player = Main.player[npc.lastInteraction];
                if (player.GetModPlayer<MyPlayer>().bigRedNose)
                {
                    // Adjust the y-coordinate to spawn the projectile higher
                    Vector2 spawnPosition = npc.position;
                    spawnPosition.Y -= 45; // Adjust the value as needed

                    // Create the explosion projectile
                    int proj = Projectile.NewProjectile(npc.GetSource_Death(), spawnPosition, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT1Explosion, 40, 10, player.whoAmI);

                    // Set local immunity for the projectile
                    Main.projectile[proj].usesLocalNPCImmunity = true;
                    Main.projectile[proj].localNPCHitCooldown = 10; // Adjust the cooldown value as needed
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);

            // Update the immunity frames for each NPC
            foreach (int npcIndex in npcImmunity.Keys.ToArray())
            {
                if (npcImmunity[npcIndex] > 0)
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
