using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace DynastyMod.Items
{
	public class IceScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			//Not How things work anymore
			//Item.ranged = true;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 1.5f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.shootSpeed = 16f;
			Item.shoot = ProjectileID.IceBolt;
			Item.ammo = 100;
		}
	}
}
