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
            Main.npcFrameCount[npc.type] = 15;
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

		// These const ints are for the benefit of the programmer. Organization is key to making an AI that behaves properly without driving you crazy.
		// Here I lay out what I will use each of the 4 npc.ai slots for.
		private const int AI_State_Slot = 0;
		private const int AI_Timer_Slot = 1;
		private const int AI_Flutter_Time_Slot = 2;
		private const int AI_Unused_Slot_3 = 3;

		// npc.localAI will also have 4 float variables available to use. With ModNPC, using just a local class member variable would have the same effect.
		private const int Local_AI_Unused_Slot_0 = 0;
		private const int Local_AI_Unused_Slot_1 = 1;
		private const int Local_AI_Unused_Slot_2 = 2;
		private const int Local_AI_Unused_Slot_3 = 3;

		// Here I define some values I will use with the State slot. Using an ai slot as a means to store "state" can simplify things greatly. Think flowchart.
		private const int State_Asleep = 0;
		private const int State_Notice = 1;
		private const int State_Jump = 2;
		private const int State_Hover = 3;
		private const int State_Fall = 4;

		// This is a property (https://msdn.microsoft.com/en-us/library/x9fsa0sw.aspx), it is very useful and helps keep out AI code clear of clutter.
		// Without it, every instance of "AI_State" in the AI code below would be "npc.ai[AI_State_Slot]". 
		// Also note that without the "AI_State_Slot" defined above, this would be "npc.ai[0]".
		// This is all to just make beautiful, manageable, and clean code.
		public float AI_State
		{
			get => npc.ai[AI_State_Slot];
			set => npc.ai[AI_State_Slot] = value;
		}

		public float AI_Timer
		{
			get => npc.ai[AI_Timer_Slot];
			set => npc.ai[AI_Timer_Slot] = value;
		}

		public float AI_FlutterTime
		{
			get => npc.ai[AI_Flutter_Time_Slot];
			set => npc.ai[AI_Flutter_Time_Slot] = value;
		}

		// AdvancedFlutterSlime will need: float in water, diminishing aggo, spawn projectiles.

		// Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
		public override void AI()
		{ 
			// In this state, our npc starts to flutter/fly a little to make it's movement a little bit interesting.
			if (AI_State == State_Hover)
			{
				AI_Timer += 1;
				// Here we make a decision on how long this flutter will last. We check netmode != 1 to prevent Multiplayer Clients from running this code. (similarly, spawning projectiles should also be wrapped like this)
				// netmode == 0 is SP, netmode == 1 is MP Client, netmode == 2 is MP Server. 
				// Typically in MP, Client and Server maintain the same state by running deterministic code individually. When we want to do something random, we must do that on the server and then inform MP Clients.
				// Informing MP Clients is done automatically by syncing the npc.ai array over the network whenever npc.netUpdate is set. Don't set netUpdate unless you do something non-deterministic ("random")
				if (AI_Timer == 1 && Main.netMode != NetmodeID.MultiplayerClient)
				{
					AI_FlutterTime = Main.rand.NextBool() ? 100 : 50;
					npc.netUpdate = true;
				}
				// Here we add a tiny bit of upward velocity to our npc.
				npc.velocity += new Vector2(0, -.35f);
				// ... and some additional X velocity when traveling slow.
				if (Math.Abs(npc.velocity.X) < 2)
				{
					npc.velocity += new Vector2(npc.direction * .05f, 0);
				}
				if (AI_Timer > AI_FlutterTime)
				{
					// after fluttering for 100 ticks (1.66 seconds), our Flutter Slime is tired, so he decides to go into the Fall state.
					AI_State = State_Fall;
					AI_Timer = 0;
				}
			}
			// In this state, we fall untill we hit the ground. Since npc.noTileCollide is false, our npc collides with ground it lands on and will have a zero y velocity once it has landed.
			else if (AI_State == State_Fall)
			{
				if (npc.velocity.Y == 0)
				{
					npc.velocity.X = 0;
					AI_State = State_Asleep;
					AI_Timer = 0;
				}
			}
		}
    }
}
