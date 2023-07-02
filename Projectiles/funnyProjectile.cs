using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DynastyMod.Projectiles
{
	public class funnyProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("yuri");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;               
			Projectile.height = 30;              
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         
			Projectile.hostile = false;    
			// This Not a thing anymore - Nathan
			//Projectile.ranged = true;           
			Projectile.penetrate = 5;           
			Projectile.timeLeft = 600;          
			Projectile.alpha = 255;             
			Projectile.light = 0.5f;            
			Projectile.ignoreWater = true;          
			Projectile.tileCollide = true;          
			Projectile.extraUpdates = 1;            
			AIType = ProjectileID.Bullet;          
		}

	}
}
