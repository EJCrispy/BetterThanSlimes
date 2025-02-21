using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Buffs
{
    public class Full : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false; // This is not a debuff
            Main.buffNoSave[Type] = true; // This buff won't save when exiting and rejoining the world
            Main.buffNoTimeDisplay[Type] = false; // Time will be displayed for this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // When the buff is about to run out, apply the Starvation debuff permanently
            if (player.buffTime[buffIndex] == 1) // 1 frame before the buff expires
            {
                player.AddBuff(ModContent.BuffType<Starvation>(), int.MaxValue); // Apply Starvation permanently
            }

            // Remove the Starvation debuff if it's active
            if (player.buffTime[buffIndex] > 1) // While the Full buff is active
            {
                player.ClearBuff(ModContent.BuffType<Starvation>());
            }

            // Apply defense bonuses based on the remaining time of the Full buff
            if (player.buffTime[buffIndex] > 60 * 8 * 60) // 8 minutes or more (480 seconds)
            {
                player.statDefense += 10; // +10 defense for 8+ minutes
            }
            else if (player.buffTime[buffIndex] > 60 * 4 * 60) // 4 minutes or more (240 seconds)
            {
                player.statDefense += 5; // +5 defense for 4+ minutes
            }
        }

        // Helper method to convert time from other buffs to the Full buff
        public static void ConvertFoodBuffsToFull(Player player)
        {
            int[] foodBuffIDs = [BuffID.WellFed, BuffID.WellFed2, BuffID.WellFed3]; // IDs for Well Fed, Plenty Satisfied, Exquisitely Stuffed
            int fullBuffID = ModContent.BuffType<Full>();

            foreach (int foodBuffID in foodBuffIDs)
            {
                if (player.HasBuff(foodBuffID))
                {
                    int foodBuffIndex = player.FindBuffIndex(foodBuffID);
                    if (foodBuffIndex != -1)
                    {
                        // Add the remaining time of the food buff to the Full buff
                        int fullBuffIndex = player.FindBuffIndex(fullBuffID);
                        if (fullBuffIndex == -1)
                        {
                            // If the Full buff isn't active, add it with the remaining time
                            player.AddBuff(fullBuffID, player.buffTime[foodBuffIndex]);
                        }
                        else
                        {
                            // If the Full buff is already active, add the remaining time to it
                            player.buffTime[fullBuffIndex] += player.buffTime[foodBuffIndex];
                        }

                        // Remove the food buff
                        player.DelBuff(foodBuffIndex);
                    }
                }
            }
        }
    }
}