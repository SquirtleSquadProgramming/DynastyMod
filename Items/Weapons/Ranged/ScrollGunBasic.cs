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
			Item.damage = 20;
			//Item.ranged = true;
			Item.width = 40;
			Item.height = 20;
            //Item.magic = true;
            Item.mana = 2;
            Item.useTime = 20;
			Item.useAnimation = 20;
			//Item.useStyle = ItemUseStyleID.HoldingOut;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shootSpeed = 16f;
			Item.useAmmo = 100;
			Item.shoot = ProjectileID.Bullet;
		}

        //Called just before the bullet is made - meant to be used for speacal bullet effects
		//This is broken Now - Nathan 2023
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            lastDam = damage - 20;
            return true;
        }*/

        // Called all the time - modify mana by the damage done by the gun before it shoot - will be changed later when sean tells me how he wants it changed
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            mult = lastDam / 2;
        }
    }
}
