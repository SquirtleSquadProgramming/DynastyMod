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
			Item.damage = 20;
			//Item.ranged = true;
			Item.width = 40;
			Item.height = 20;
            //Item.magic = false;
            Item.useTime = 20;
			Item.useAnimation = 20;
			//Item.useStyle = ItemUseStyleID.HoldingOut;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = false;
			Item.shootSpeed = 16f;
			Item.useAmmo = ModContent.ItemType<BohiyaArrowExplosive>();
			Item.shoot = ProjectileID.Bullet;
		}
    }
}
