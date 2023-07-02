using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DynastyMod.Items.Weapons;

namespace DynastyMod.Projectiles
{
	public class RopeDartProjectile : ModProjectile
	{
		private const string ChainTexturePath = "DynastyMod/Projectiles/RopeDartChain";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rope Flail");
		}
		
		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 44;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			//This uses Projectile Types now I think not these goofy ah bool
			//Projectile.meele = true;
			Projectile.aiStyle = 15;
		}
		public dynamic d = 0;
		public override void AI()
        {
			var player = Main.player[Projectile.owner];
			if (d == 0)
				d = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			Projectile.rotation = d;
			//Projectile.rotation = vectorToPlayer.ToRotation();
		}
		//Draw chain cus defult look as hoe bitch
		//This is now brokey with new tmod version so need look at wiki
		/*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var player = Main.player[Projectile.owner];

			Vector2 mountedCenter = player.MountedCenter;
			Texture2D chainTexture = ModContent.GetText(ChainTexturePath);

			var drawPosition = Projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

			if (Projectile.alpha == 0)
			{
				int direction = -1;

				if (Projectile.Center.X < mountedCenter.X)
					direction = 1;

				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}
			while (true)
			{
				float length = remainingVectorToPlayer.Length();

				// Once the remaining length is small enough, we terminate the loop
				if (length < 25f || float.IsNaN(length))
					break;

				// drawPosition is advanced along the vector back to the player by 12 pixels
				// 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
				drawPosition += remainingVectorToPlayer * 12 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				// Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.35f, 1f, SpriteEffects.None, 0f);
			}
			return true;
		}*/
	}

}
