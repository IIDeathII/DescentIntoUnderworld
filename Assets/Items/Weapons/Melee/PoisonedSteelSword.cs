using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DescentIntoUnderworld.Assets.Items.Weapons.Melee
{
    public class PoisonedSteelSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // If you want to use a tooltip here, prefer localization files.
            // Tooltip.SetDefault("A sword forged from poisoned steel.");
        }

        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Poisoned, 0f, 0f, 100, default, 1f);
            }
            // Add light at the swing center so the hit area glows briefly
            Vector2 center = new Vector2(hitbox.X + hitbox.Width * 0.5f, hitbox.Y + hitbox.Height * 0.5f);
            // Tono verdoso (veneno)
            Lighting.AddLight(center, 0.15f, 0.8f, 0.15f);
        }

        public override void HoldItem(Player player)
        {
            // Only emit light while the player is actually swinging the weapon
            // player.itemAnimation > 0 indicates the use animation is active
            if (player.itemAnimation > 0 && player.HeldItem.type == Item.type)
            {
                Vector2 pos = player.Center + new Vector2(player.direction * 16f, -6f);
                // Tono verdoso mientras se ataca
                Lighting.AddLight(pos, 0.15f, 0.8f, 0.15f);
            }
        }

    // GlobalItem to make the dropped PoisonedSteelSword emit light while in the world
    public class PoisonedSteelGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void PostUpdate(Item item)
        {
            // Check by type to avoid affecting other items
            if (item.type == ModContent.ItemType<PoisonedSteelSword>())
            {
                // Añadir luz verdosa para la espada dropeada
                Lighting.AddLight(item.Center, 0.15f, 0.6f, 0.15f);
            }
        }
    }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 25% chance to apply poisoned buff for 5 seconds
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Poisoned, 300);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 7)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
