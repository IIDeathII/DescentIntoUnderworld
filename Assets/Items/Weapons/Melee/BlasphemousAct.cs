using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DescentIntoUnderworld.Assets.Items.Weapons.Melee
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class BlasphemousAct : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.DescentIntoUnderworld.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 2222;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			// Spawn fire dust occasionally on swing
			if (Main.rand.NextBool(3))
			{
				// Use Torch dust for visible flame effect
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 100, default, 1f);
			}

			// Add orange light at swing center
			Vector2 center = new Vector2(hitbox.X + hitbox.Width * 0.5f, hitbox.Y + hitbox.Height * 0.5f);
			Lighting.AddLight(center, 1.0f, 0.45f, 0.08f);
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Apply On Fire debuff for 4 seconds
			if (Main.rand.NextBool(2)) // 50% chance
			{
				target.AddBuff(BuffID.OnFire, 240);
			}
		}

	// Make BlasphemousAct emit light when dropped in the world
	public class BlasphemousGlobal : GlobalItem
	{
		public override bool InstancePerEntity => true;

		public override void PostUpdate(Item item)
		{
			if (item.type == ModContent.ItemType<BlasphemousAct>())
			{
				Lighting.AddLight(item.Center, 0.6f, 0.28f, 0.06f);
			}
		}
	}

		public override void HoldItem(Player player)
		{
			// Only emit light while swinging
			if (player.itemAnimation > 0 && player.HeldItem.type == Item.type)
			{
				Vector2 pos = player.Center + new Vector2(player.direction * 16f, -6f);
				Lighting.AddLight(pos, 0.6f, 0.28f, 0.06f);
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
