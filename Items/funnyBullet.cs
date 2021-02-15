using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
namespace DynastyMod.Items
{
	public class funnyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Funny Scroll");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			//Resize this depending on sprite size *Note bullet sprite is not shot only used as the inv item
			item.width = 8;
			item.height = 8;
			item.maxStack = 1;
			item.consumable = false;             
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.shoot = ModContent.ProjectileType<funnyProjectile>();   
			item.shootSpeed = 16f;                  
			item.ammo = item.type;              
		}

	}
}