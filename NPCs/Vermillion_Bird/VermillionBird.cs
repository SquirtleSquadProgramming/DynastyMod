using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.IO;

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
			npc.height = 110;
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
			Star,
			Tracking,
			SweepRight,
			SweepLeft
        }
		private short AttackToShort(Attack a) => (short)((a == Attack.None) ? 0 : (a == Attack.Star) ? 1 : (a == Attack.Tracking) ? 2 : (a == Attack.SweepRight) ? 3 : (a == Attack.SweepLeft) ? 4 : -1);
		private Attack ShortToAttack(short a) =>   ((a == 0) ? Attack.None : (a == 1) ? Attack.Star : (a == 2) ? Attack.Tracking : (a == 3) ? Attack.SweepRight : (a == 4) ? Attack.SweepLeft : Attack.None);
		private Attack currentAttack = Attack.None;
		private short Phase = 1;

		// Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
		public override void AI()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Lighting.AddLight(npc.Center, Color.Orange.ToVector3() * 1f);
				Lighting.AddLight(npc.Center, Color.Green.ToVector3() * 1f);
				npc.TargetClosest(true);
				Vector2 targetPosition = Main.player[npc.target].position;
				if (targetPosition.Y < npc.Center.Y + (10 * CM.Y) && npc.velocity.Y > -4)
					npc.velocity.Y -= DefaultAccelartion.Y; // accelerate up
				if (targetPosition.Y > npc.Center.Y + (10 * CM.Y) && npc.velocity.Y < 4)
					npc.velocity.Y += DefaultAccelartion.Y; // accelerate down

				Action[] Phases = new Action[] { Phase1, Phase2 };
				Phases[Phase - 1]();
			}
		}

        public override void SendExtraAI(BinaryWriter writer)
        {
			writer.Write(timer);
			writer.Write(cooldown);
			writer.Write(AttackToShort(currentAttack));
			writer.Write(Phase);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
        {
			timer = reader.ReadInt32();
			cooldown = reader.ReadInt32();
			currentAttack = ShortToAttack(reader.ReadInt16());
			Phase = reader.ReadInt16();
        }

        private void Phase1()
		{
			Vector2 targetPosition = Main.player[npc.target].position;
			npc.velocity.X = (cooldown > 0) ? 0 : (targetPosition.X - npc.Center.X) / (2 * CM.X);
			timer--;
			cooldown--;
			if (timer < 0)
			{
				currentAttack = Attack.None;
				int RandomTime = WorldGen.genRand.Next(5);
				switch (WorldGen.genRand.Next(6) % 3)
				{
					case 0: // Tracking Attack
						cooldown = (int)(1.5f * tickSpeed);
						timer = RandomTime + (1 * tickSpeed) + cooldown;
						npc.velocity.X = 0;
						currentAttack = Attack.Tracking;
						break;
					case 1: // Star Attack
						cooldown = (int)(1.5f * tickSpeed);
						timer = RandomTime + (4 * tickSpeed) + cooldown;
						npc.velocity.X = 0;
						currentAttack = Attack.Star;
						break;
					case 2: // Sweep/hellfire Attack
						cooldown = (int)(1.5f * tickSpeed);
						timer = RandomTime + (5 * tickSpeed) + cooldown;
						npc.velocity.X = 0;
						currentAttack = (WorldGen.genRand.Next(2) == 0) ? Attack.SweepRight : Attack.SweepLeft;
						break;
				}
			}
			if (currentAttack == Attack.Star && cooldown % (0.25f * tickSpeed) == 0) StarAttack();
			else if (currentAttack == Attack.Tracking && cooldown % (0.25f * tickSpeed) == 0) TrackingAttack();
			else if ((currentAttack == Attack.SweepRight || currentAttack == Attack.SweepLeft) && cooldown % 2 == 0) SweepAttack();
		}

		private void Phase2()
        {

        }

		private void SweepAttack()
		{
			float SweepDirection = (currentAttack == Attack.SweepLeft) ? 1 : -1;
			int r = WorldGen.genRand.Next(30);
			if (r == 0) return;
			Vector2 direction = new Vector2(0f, 1f);
			float speed = 4.5f;
			Vector2 SpawnPosition = new Vector2((npc.Center.X - (SweepDirection * cooldown * CM.X) - (45 * CM.X)), npc.Center.Y - (-r + 20 * CM.Y));
			Projectile.NewProjectile(SpawnPosition, direction * speed, ProjectileID.Fireball, 10, 0f, Main.myPlayer);
        }

		private void TrackingAttack()
		{
			if (cooldown == (int)(0.5f * tickSpeed) || cooldown == (int)(1f * tickSpeed))
            {
				TrackingProjectile(npc.Center);
				TrackingProjectile(new Vector2(npc.Center.X + 10*CM.X, npc.Center.Y));
				TrackingProjectile(new Vector2(npc.Center.X + 30 * CM.X, npc.Center.Y));
				TrackingProjectile(new Vector2(npc.Center.X - 10*CM.X, npc.Center.Y));
				TrackingProjectile(new Vector2(npc.Center.X - 30 * CM.X, npc.Center.Y));
			}
		}
		private void TrackingProjectile(Vector2 SpawnPosition)
        {
			Vector2 targetPosition = Main.player[npc.target].Center;
			Vector2 direction = targetPosition - SpawnPosition;
			direction.Normalize();
			float speed = 10f;
			Projectile.NewProjectile(SpawnPosition, direction * speed, ProjectileID.Fireball, 10, 0f, Main.myPlayer);
        }

		private void StarAttack()
		{ 
			Vector2[] strDirections = new Vector2[] { new Vector2(0f, 10f), new Vector2(10f, 10f), new Vector2(10f, 0f), new Vector2(10f, -10f), new Vector2(0f, -10f), new Vector2(-10f, -10f), new Vector2(-10f, 0), new Vector2(-10f, 10f) };
			Vector2[] diaDirections = new Vector2[] { new Vector2(5f, 10f), new Vector2(10f, 5f), new Vector2(10f, -5f), new Vector2(5f, -10f), new Vector2(-5f, -10f), new Vector2(-10f, -5f), new Vector2(-10f, 5f), new Vector2(-5f, 10f) };
			if (cooldown == (int)(1.25f * tickSpeed) || cooldown == (int)(0.75f * tickSpeed))
				strDirections.ToList().ForEach(n => Projectile.NewProjectile(npc.Center, n, ProjectileID.Fireball, 10, 0f));
			else if (cooldown == (int)(1f * tickSpeed) || cooldown == (int)(0.5f * tickSpeed))
				diaDirections.ToList().ForEach(n => Projectile.NewProjectile(npc.Center, n, ProjectileID.Fireball, 10, 0f));
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
