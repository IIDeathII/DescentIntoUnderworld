using DescentIntoUnderworld.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DescentIntoUnderworld.Content.Items.Weapons.Summon
{
    public class UnderSummonWhip : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<UnderSummonWhipProjectile>(), 20, 2, 4);
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 1);
            Item.damage = 15;
            Item.knockBack = 2f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float swingDirection = 0.6f + (0.4f * Main.rand.NextFloat());
            if (Main.rand.NextBool(3))
            {
                swingDirection *= -2.5f;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, swingDirection);
            return false;
        }

        // Efectos visuales cuando usas el látigo
        public override void UseItemFrame(Player player)
        {
            // Spawn partículas mágicas más intensas
            if (Main.rand.NextBool(2))
            {
                int dustType = DustID.PurpleMoss;
                Vector2 dustPos = player.itemLocation + new Vector2(Main.rand.Next(-30, 30), Main.rand.Next(-30, 30));
                Dust dust = Dust.NewDustDirect(dustPos, 8, 8, dustType, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, Color.Magenta);
                dust.noGravity = true;
                dust.scale = 1.2f;
            }
        }

        // Brillo adicional más fuerte
        public override void HoldItem(Player player)
        {
            // Luz más intensa y brillante
            Lighting.AddLight(player.itemLocation, 0.8f, 0.4f, 1.2f); // Mucho más brillante

            // Partículas constantes mientras lo sostienes
            if (Main.rand.NextBool(4))
            {
                int dustType = DustID.MagicMirror;
                Vector2 dustPos = player.itemLocation + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15));
                Dust dust = Dust.NewDustDirect(dustPos, 6, 6, dustType, 0, 0, 200, Color.Magenta * 0.9f);
                dust.noGravity = true;
                dust.scale = 1.5f;
            }
        }
    }
}