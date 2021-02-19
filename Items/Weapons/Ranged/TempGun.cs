using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace DynastyMod.Items.Weapons.Ranged
{
    public class TempGun : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots Arrows");
		}
        public int lastDam = 2;
        public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
            item.magic = false;
            item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.shootSpeed = 16f;
			item.useAmmo = ModContent.ItemType<BohiyaArrowExplosive>();
			item.shoot = ProjectileID.Bullet;
		}
    }
}
