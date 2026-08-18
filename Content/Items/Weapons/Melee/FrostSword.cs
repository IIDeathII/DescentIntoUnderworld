using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DescentIntoUnderworld.Content.Items.Weapons.Melee
{
    public class FrostSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip translation should go in localization files
        }

        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(silver: 6);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Spawn cold particles on swing
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Snow, 0f, 0f, 100, default, 1f);
            }

            // Add icy blue light at swing center
            Vector2 center = new Vector2(hitbox.X + hitbox.Width * 0.5f, hitbox.Y + hitbox.Height * 0.5f);
            // Tono más blanquecino (azul con mayor componente roja/verde)
            Lighting.AddLight(center, 0.6f, 0.7f, 1.0f);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 40% chance to apply Frostburn for 5 seconds
            if (Main.rand.NextBool(5) == false) // 4/5 leads to true? better use NextFloat
            {
                // Using a random check for ~40%: NextBool(5) is 1/5 true, so invert
                // Use NextFloat for clearer probability
            }
            if (Main.rand.NextFloat() < 0.4f)
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }

        public override void HoldItem(Player player)
        {
            // Emit light only while swinging
            if (player.itemAnimation > 0 && player.HeldItem.type == Item.type)
            {
                Vector2 pos = player.Center + new Vector2(player.direction * 16f, -6f);
                // Luz más blanquecina mientras se ataca
                Lighting.AddLight(pos, 0.6f, 0.7f, 1.0f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ItemID.SnowBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    // Make FrostSword emit light when dropped in the world
    public class FrostSwordGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void PostUpdate(Item item)
        {
            if (item.type == ModContent.ItemType<FrostSword>())
            {
                // Luz de la espada dropeada más blanquecina
                Lighting.AddLight(item.Center, 0.55f, 0.65f, 0.95f);
            }
        }
    }
}
