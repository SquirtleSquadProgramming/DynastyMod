using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using DynastyMod.Projectiles;

namespace DynastyMod.Items.Weapons
{
    public class RopeDart : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sexy rope and dart");
		}
        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.White;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 4f;
			item.damage = 9;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<Projectiles.RopeDartProjectile>();
			item.shootSpeed = 15.1f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.crit = 9;
			item.channel = true;
		}
		/*public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddIngredient(ModContent.ItemType<ExampleItem>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
