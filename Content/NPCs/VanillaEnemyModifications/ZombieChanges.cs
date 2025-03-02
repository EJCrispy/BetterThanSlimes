using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BetterThanSlimes.Content.NPCs.VanillaEnemyModifications
{
    public class ZombieBlockBreaker : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.Zombie;
        }

        public override void AI(NPC npc)
        {
            base.AI(npc); // Keep original Zombie AI
            BreakBlocks(npc);
        }

        private void BreakBlocks(NPC npc)
        {
            const int cooldown = 5 * 60; // 5 seconds in ticks (60 ticks = 1 second)

            // Check cooldown first
            if (npc.ai[1] > 0)
            {
                npc.ai[1]--; // Decrement cooldown
                return; // Exit if cooldown is active
            }

            Player player = Main.player[npc.target];
            
            // Check tiles in 3 directions:
            // 1. Horizontal (left/right based on facing direction)
            // 2. Above horizontal tile
            // 3. Below horizontal tile
            int baseTileX = (int)(npc.Center.X / 16) + npc.direction;
            int baseTileY = (int)(npc.Center.Y / 16);

            bool brokeBlock = false;

            // Check horizontal tile
            brokeBlock |= TryBreakSingleTile(npc, baseTileX, baseTileY);
            
            // Check tile above
            brokeBlock |= TryBreakSingleTile(npc, baseTileX, baseTileY - 1);
            
            // Check tile below
            brokeBlock |= TryBreakSingleTile(npc, baseTileX, baseTileY + 1);

            // If any block was broken, start cooldown
            if (brokeBlock)
            {
                npc.ai[1] = cooldown; // Universal 5-second cooldown
            }
        }

        private bool TryBreakSingleTile(NPC npc, int tileX, int tileY)
        {
            Tile tile = Framing.GetTileSafely(tileX, tileY);

            // Block breaking conditions
            bool isSolid = tile.HasTile && Main.tileSolid[tile.TileType];
            bool isNotPlatform = !TileID.Sets.Platforms[tile.TileType];
            bool isNotLivingWood = tile.TileType != TileID.LivingWood && tile.TileType != TileID.LeafBlock;

            // Copper Pickaxe check (mine resistance <= 35)
            float mineResistance = GetMineResistance(tile.TileType);
            bool canBreak = mineResistance > 35f; // Zombie can't break what a Copper Pickaxe can

            if (isSolid && isNotPlatform && isNotLivingWood && canBreak)
            {
                WorldGen.KillTile(tileX, tileY); // Break instantly
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tileX, tileY);
                }
                return true;
            }
            return false;
        }

        private float GetMineResistance(int tileType)
        {
            // Vanilla tile resistances (simplified)
            return tileType switch
            {
                TileID.Dirt => 0f,
                TileID.Stone => 35f,
                TileID.Ebonstone => 50f,
                TileID.Crimstone => 50f,
                TileID.Meteorite => 65f,
                _ => 35f // Default value
            };
        }
    }
}