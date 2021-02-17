using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DynastyMod.Projectiles
{
	public class InvisableDebuffProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("yuriJoe");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        
		}

		public override void SetDefaults()
		{
			projectile.width = 30;               
			projectile.height = 30;              
			projectile.aiStyle = 1; //The ai style of the projectile, please reference the source code of Terraria
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;           
			projectile.penetrate = 5;           
			projectile.timeLeft = 600;          
			projectile.alpha = 255;             
			projectile.light = 0f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;          
			projectile.extraUpdates = 1;
			//aiType = ProjectileID.Arrow;          
		}
		public override void ModifyHitNPC(NPC target,ref int damage,ref float knockback,ref bool crit,ref int hitDirection)
        {
			target.AddBuff(24, 300, true);
		}
	}
}
