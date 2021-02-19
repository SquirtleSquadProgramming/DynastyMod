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
			item.damage = 9;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 1;
			item.consumable = false;
			item.knockBack = 1.5f;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.shootSpeed = 16f;
			item.shoot = ProjectileID.IceBolt;
			item.ammo = 100;
		}
	}
}
