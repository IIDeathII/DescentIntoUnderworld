using DescentIntoUnderworld.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace DescentIntoUnderworld.Content.Buff
{
    public class UnderMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<UnderMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
                player.maxMinions++;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}