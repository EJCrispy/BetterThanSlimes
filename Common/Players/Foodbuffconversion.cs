using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Players
{
    public class FoodBuffConversionPlayer : ModPlayer
    {
        public override void UpdateEquips()
        {
            ConvertFoodBuffsToFull();
        }

        private void ConvertFoodBuffsToFull()
        {
            int[] foodBuffIDs = [BuffID.WellFed, BuffID.WellFed2, BuffID.WellFed3]; // IDs for Well Fed, Plenty Satisfied, Exquisitely Stuffed
            int fullBuffID = ModContent.BuffType<Buffs.Full>();

            foreach (int foodBuffID in foodBuffIDs)
            {
                if (Player.HasBuff(foodBuffID))
                {
                    int foodBuffIndex = Player.FindBuffIndex(foodBuffID);
                    if (foodBuffIndex != -1)
                    {
                        // Add the remaining time of the food buff to the Full buff
                        int fullBuffIndex = Player.FindBuffIndex(fullBuffID);
                        if (fullBuffIndex == -1)
                        {
                            // If the Full buff isn't active, add it with the remaining time
                            Player.AddBuff(fullBuffID, Player.buffTime[foodBuffIndex]);
                        }
                        else
                        {
                            // If the Full buff is already active, add the remaining time to it
                            Player.buffTime[fullBuffIndex] += Player.buffTime[foodBuffIndex];
                        }

                        // Remove the food buff
                        Player.DelBuff(foodBuffIndex);
                    }
                }
            }
        }
    }
}