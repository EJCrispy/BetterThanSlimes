using Terraria;
using Terraria.ModLoader;

public class RemoveBaseCrit : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        // Check if the item is a weapon (melee, ranged, magic, summon)
        if (item.damage > 0 && (item.DamageType.CountsAsClass(DamageClass.Melee) ||
                                item.DamageType.CountsAsClass(DamageClass.Ranged) ||
                                item.DamageType.CountsAsClass(DamageClass.Magic) ||
                                item.DamageType.CountsAsClass(DamageClass.Summon)))
        {
            item.crit -= 4;  // Remove the default 4% crit chance
        }
    }
}

