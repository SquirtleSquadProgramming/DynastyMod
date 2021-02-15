using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DynastyMod.Items.Weapons.Ranged
{
    public class ScrollGunBasic : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots scrolls");
		}
		public int lastDam = 2;
		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.magic = true;
			item.mana = 2;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shootSpeed = 16f;
			item.useAmmo = mod.ProjectileType("Scroll");
            //item.shoot = ProjectileID.CursedFlameFriendly;
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ExampleItem>(), 10);
			recipe.AddTile(ModContent.TileType<ExampleWorkbench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/


		//Called just before the bullet is made - meant to be used for speacal bullet effects
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{	
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack);
			mod.Logger.Info($"{type} {ProjectileID.CursedFlameFriendly}");
			lastDam = damage - 20;
			return true;
		}
		//Called all the time - modify mana by the damage done by the gun before it shoot - will be changed later when sean tells me how he wants it changed
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) => mult = lastDam / 2;
	}
}
