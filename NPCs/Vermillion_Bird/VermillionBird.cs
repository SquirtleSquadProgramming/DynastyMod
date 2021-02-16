using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;

namespace DynastyMod.NPCs.Vermillion_Bird
{
	[AutoloadBossHead]
	public class VermillionBird : ModNPC
    {
		private const int sphereRadius = 300;

		private float attackCool
		{
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}

		private float moveCool
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}

		private float rotationSpeed
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}

		private float captiveRotation
		{
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}

		private int moveTime = 300;
		private int moveTimer = 60;
		internal int laserTimer;
		internal int laser1 = -1;
		internal int laser2 = -1;
		private bool dontDamage;

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vermillion Bird");
            Main.npcFrameCount[npc.type] = 1;
        }

		public override void SetDefaults()
		{
			npc.aiStyle = -1; // need to add ai
			npc.lifeMax = 2000;
			npc.damage = 100;
			npc.defense = 55;
			npc.knockBackResist = 0f;
			npc.width = 400;
			npc.height = 400;
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

		public override void AI()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == 0f)
			{

			}

			Player player = Main.player[npc.target];
			if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				if (!player.active || player.dead)
				{
					npc.velocity = new Vector2(0f, 10f);
					if (npc.timeLeft > 10)
					{
						npc.timeLeft = 10;
					}
					return;
				}
			}
			moveCool -= 1f;
			if (Main.netMode != NetmodeID.MultiplayerClient && moveCool <= 0f)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				double angle = Main.rand.NextDouble() * 2.0 * Math.PI;
				int distance = sphereRadius + Main.rand.Next(200);
				Vector2 moveTo = player.Center + (float)distance * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
				moveCool = (float)moveTime + (float)Main.rand.Next(100);
				npc.velocity = (moveTo - npc.Center) / moveCool;
				rotationSpeed = (float)(Main.rand.NextDouble() + Main.rand.NextDouble());
				if (rotationSpeed > 1f)
				{
					rotationSpeed = 1f + (rotationSpeed - 1f) / 2f;
				}
				if (Main.rand.NextBool())
				{
					rotationSpeed *= -1;
				}
				rotationSpeed *= 0.01f;
				npc.netUpdate = true;
			}
		}
    }
}
