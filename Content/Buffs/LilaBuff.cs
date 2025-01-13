using Terraria;
using Terraria.ModLoader;

namespace BetterThanSlimes.Content.Players
{
    public class MyModPlayer : ModPlayer
    {
        public bool attackSpeedBuff;

        public override void ResetEffects()
        {
            attackSpeedBuff = false;
        }

        public override void UpdateDead()
        {
            attackSpeedBuff = false;
        }

        public override void PostUpdate()
        {
            if (attackSpeedBuff)
            {
                // Reference the player variable correctly within the class
                Player player = this.Player;
                // Increase attack speed for all weapons
                player.GetAttackSpeed(DamageClass.Melee) += 0.16f;
                player.GetAttackSpeed(DamageClass.Ranged) += 0.16f;
                player.GetAttackSpeed(DamageClass.Magic) += 0.16f;
                player.GetAttackSpeed(DamageClass.Summon) += 0.16f;
                player.GetAttackSpeed(DamageClass.Throwing) += 0.16f;
            }
        }
    }
}
