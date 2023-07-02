using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
namespace DynastyMod.Items
{
	public class BohiyaArrowExplosive : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Explosive Arrow");
			ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			//This is not how it works anymore
			//Item.ranged = true;
			//Resize this depending on sprite size 
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 100;
			Item.consumable = true;             
			Item.knockBack = 1.5f;
			Item.value = 10;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = ModContent.ProjectileType<Projectiles.BohiyaArrowExplosive>();   
			Item.shootSpeed = 1f;                  
			Item.ammo = 40;              
		}
		//Change This later
		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ExampleItem>());
			recipe.AddIngredient(ItemID.Dynamite);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
