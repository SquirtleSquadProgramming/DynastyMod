using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace DynastyMod.NPCs.Vermillion_Bird
{
	[AutoloadBossHead]
	public class VermillionBird : ModNPC
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vermillion Bird");
            Main.npcFrameCount[npc.type] = 3;
        }

		public override void SetDefaults()
		{
			npc.aiStyle = -1; // need to add ai
			npc.lifeMax = 2000;
			npc.damage = 100;
			npc.defense = 55;
			npc.knockBackResist = 0f;
			npc.width = 100;
			npc.height = 100;
			npc.scale = 1.5f;
			npc.value = Item.buyPrice(0, 20, 0, 0);
			npc.npcSlots = 15f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.buffImmune[24] = true;
			music = MusicID.Boss2;
		}

		private Vector2 DefaultAccelartion = new Vector2(0.12f, 0.7f);
		private Vector2 CM = new Vector2(16, 16); // Co-ordinates multiplication because 1 tile is 16 wide by 16 high
		private int timer;
		private int tickSpeed = 60;
		private int cooldown;
		private enum Attack
        {
			None,
			Star
        }
		private Attack currentAttack = Attack.None;

		// Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
		public override void AI()
		{
			npc.TargetClosest(true);
			Vector2 targetPosition = Main.player[npc.target].position;
			if (targetPosition.Y < npc.position.Y + (18*CM.Y) && npc.velocity.Y > -4)
				npc.velocity.Y -= DefaultAccelartion.Y; // accelerate up
			if (targetPosition.Y > npc.position.Y + (18*CM.Y) && npc.velocity.Y < 4)
				npc.velocity.Y += DefaultAccelartion.Y; // accelerate down

			npc.velocity.X = (cooldown > 0) ? 0 : (targetPosition.X - (3*CM.X) - npc.position.X) / (2*CM.X);

			timer--;
			cooldown--;
            if (timer < 0)
            {
                currentAttack = Attack.None;
                timer = 3 * tickSpeed;
                switch (WorldGen.genRand.Next(2))
                {
                    case 0:
						currentAttack = Attack.None;
						cooldown = 0;
						break;
                    case 1:
						cooldown = (int)(1.5f * tickSpeed);
						npc.velocity.X = 0;
						currentAttack = Attack.Star;
						break;
                }
            }
			if (currentAttack == Attack.Star && cooldown % (0.25f * tickSpeed) == 0) StarAttack();
		}

		private void StarAttack()
		{ 
			Vector2[] strDirections = new Vector2[] { new Vector2(0f, 10f), new Vector2(10f, 10f), new Vector2(10f, 0f), new Vector2(10f, -10f), new Vector2(0f, -10f), new Vector2(-10f, -10f), new Vector2(-10f, 0), new Vector2(-10f, 10f) };
			Vector2[] diaDirections = new Vector2[] { new Vector2(5f, 10f), new Vector2(10f, 5f), new Vector2(10f, -5f), new Vector2(5f, -10f), new Vector2(-5f, -10f), new Vector2(-10f, -5f), new Vector2(-10f, 5f), new Vector2(-5f, 10f) };
			Vector2 pos = new Vector2(npc.position.X + (5.25f * CM.X), npc.position.Y + (0 * CM.Y));
			if (cooldown == (int)(1.25f * tickSpeed))
				strDirections.ToList().ForEach(n => Projectile.NewProjectile(pos, n, ProjectileID.Fireball, 10, 0f));
			else if (cooldown == (int)(1f * tickSpeed))
				diaDirections.ToList().ForEach(n => Projectile.NewProjectile(pos, n, ProjectileID.Fireball, 10, 0f));
			else if (cooldown == (int)(0.75f * tickSpeed))
				strDirections.ToList().ForEach(n => Projectile.NewProjectile(pos, n, ProjectileID.Fireball, 10, 0f));
			else if (cooldown == (int)(0.5f * tickSpeed))
				diaDirections.ToList().ForEach(n => Projectile.NewProjectile(pos, n, ProjectileID.Fireball, 10, 0f));
		}

		int count = 0;
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
			switch((int)npc.frameCounter)
            {
				case 0:
					count = 0;
					break;
				case 3:
					count = 1;
					break;
				case 6:
					count = 2;
					break;
				case 9:
					count = 1;
					npc.frameCounter = 0;
					break;
            }
            npc.frame.Y = count * frameHeight;
			npc.frameCounter++;
		}
    }
}
