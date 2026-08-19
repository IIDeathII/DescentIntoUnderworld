using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DescentIntoUnderworld.Content.Buff;

namespace DescentIntoUnderworld.Content.Projectiles
{
    public class UnderMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.MinionShot[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            if (!player.HasBuff(ModContent.BuffType<UnderMinionBuff>()))
            {
                Projectile.Kill();
                return;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];
            }

            // Busca enemigos cercanos (300 píxeles)
            NPC targetNPC = null;
            float distanceToTarget = 300f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(Projectile))
                {
                    float distance = Projectile.Distance(npc.Center);
                    if (distance < distanceToTarget)
                    {
                        distanceToTarget = distance;
                        targetNPC = npc;
                    }
                }
            }

            // Movimiento hacia el objetivo o hacia el jugador
            float speed = 6f;
            float acceleration = 0.3f;

            if (targetNPC != null)
            {
                // Persigue al enemigo
                Vector2 direction = Projectile.DirectionTo(targetNPC.Center);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * speed, acceleration);
            }
            else
            {
                Vector2 directionToPlayer = Projectile.DirectionTo(player.Center);
                if (Projectile.Distance(player.Center) > 200f)
                {
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, directionToPlayer * speed, acceleration);
                }
                else
                {
                    Projectile.velocity *= 0.95f;
                }
            }
        }

        public override bool? CanDamage()
        {
            return null;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }
    }
}