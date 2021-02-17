using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DynastyMod.Items.Weapons.Ranged
{
    public class JadeRepeater : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Half repeater, half flamethrower!\nInflicts On Fire! when using most arrows.");
		}
        public override void SetDefaults()
		{
			item.damage = 78;
			item.ranged = true;
			item.width = 64;
			item.height = 32;
            item.useTime = 33;
			item.useAnimation = 33;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 100f;
			item.useAmmo = AmmoID.Arrow;
		}
		public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
        {
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.InvisableDebuffProjectile>(), damage, knockBack, player.whoAmI);
			return true;
		}
    }
}
