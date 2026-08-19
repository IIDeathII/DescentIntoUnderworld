using Microsoft.Xna.Framework;
using DescentIntoUnderworld.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DescentIntoUnderworld.Content.Items.Weapons.Ranger
{
    public class UnderPistol : ModItem
    {
        public override void SetDefaults() {
            // Common Properties
            Item.width = 62;
            Item.height = 32;
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Green;

            // Use Properties
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // Sound
            Item.UseSound = SoundID.Item11; 

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.knockBack = 5f;
            Item.noMelee = true;

            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override Vector2? HoldoutOffset() {
            return new Vector2(2f, -2f);
        }
    }
}
