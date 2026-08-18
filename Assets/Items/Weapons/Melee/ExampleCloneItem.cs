using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DescentIntoUnderworld.Assets.Items.Weapons.Melee
{
	public class ExampleCloneItem : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault removed; use localization files if needed
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.Starfury);
			Item.shootSpeed *= 0.75f;
			Item.damage = (int)(Item.damage * 1.5);
			
		}

		// The Starfury clone uses the cloned defaults; no custom Shoot override to avoid referencing missing projectiles.

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Starfury)
				.AddIngredient(ItemID.IronBar, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}