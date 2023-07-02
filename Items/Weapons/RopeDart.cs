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
			Item.width = 22;
			Item.height = 20;
			Item.value = Item.sellPrice(silver: 5);
			Item.rare = ItemRarityID.White;
			Item.noMelee = true;
			//Item.useStyle = ItemUseStyleID.HoldingOut;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.knockBack = 4f;
			Item.damage = 9;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.RopeDartProjectile>();
			Item.shootSpeed = 15.1f;
			Item.UseSound = SoundID.Item1;
			//Item.melee = true;
			Item.crit = 9;
			Item.channel = true;
		}
		public override void AddRecipes()
		{
			//This Recipe Needs Work
			CreateRecipe()
				.AddIngredient(ItemID.Chain, 3)
				.Register();
		}
	}
}
