using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DescentIntoUnderworld.Assets.Items.Weapons.Melee
{
	public class ExampleDualUseWeapon : ModItem
	{
		public override void SetStaticDefaults() {
			// Static defaults removed for compatibility with current tModLoader localization API
		}

		public override void SetDefaults() {
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bee;
			Item.shootSpeed = 5f;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.IronBar, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2) {
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.damage = 50;
				Item.shoot = ProjectileID.Bee;
			}
			else {
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 40;
				Item.useAnimation = 40;
				Item.damage = 100;
				Item.shoot = ProjectileID.None;
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
			if (player.altFunctionUse == 2) {
				target.AddBuff(BuffID.Ichor, 60);
			}
			else {
				target.AddBuff(BuffID.OnFire, 60);
			}
		}

		// MeleeEffects removed to avoid references to legacy Dust IDs

		// Custom shooting behavior removed to match current tModLoader API and avoid obsolete overrides
	}
}