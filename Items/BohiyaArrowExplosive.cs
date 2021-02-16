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
			ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			//Resize this depending on sprite size 
			item.width = 8;
			item.height = 8;
			item.maxStack = 100;
			item.consumable = true;             
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
			item.shoot = ModContent.ProjectileType<Projectiles.BohiyaArrowExplosive>();   
			item.shootSpeed = 1f;                  
			item.ammo = item.type;              
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
