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
			npc.aiStyle = -1;
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

		private Vector2 DefaultAcceleration = new Vector2(0.12f, 0.7f); // Default acceleration max
		private Vector2 CM = new Vector2(16, 16); // Co-ordinates multiplication because 1 tile is 16 wide by 16 high
		private int timer; // Used for counting until the boss can attack again
		private int tickSpeed = 60; // Used for converting ticks into seconds (it is 60 ticks per second)
		private int cooldown; // Used for the attacks and when to do them
		private enum Attack // Attack enumerator used for readable values
        {
			None,       // No attack happening (idling)
			Star,       // Star attack that summons projectiles in an 8-pointed star
			Tracking,   // Tracking attack that summons projectiles at the player
			SweepRight, // Sweep left attack that blankets the area with fireballs from right to left
			SweepLeft   //   "   right  "     "     "      "      "     "     "      "  left to right
        }
		private short AttackToShort(Attack a) => (short)((a == Attack.None) ? 0 : (a == Attack.Star) ? 1 : (a == Attack.Tracking) ? 2 : (a == Attack.SweepRight) ? 3 : (a == Attack.SweepLeft) ? 4 : -1); // Converts values from the Attack enum to a short for data transfer
		private Attack ShortToAttack(short a) =>   ((a == 0) ? Attack.None : (a == 1) ? Attack.Star : (a == 2) ? Attack.Tracking : (a == 3) ? Attack.SweepRight : (a == 4) ? Attack.SweepLeft : Attack.None); // Converts values from a short to the Attack enum for usage
		private Attack currentAttack = Attack.None; // The current attack
		private short Phase = 1; // What phase the boss is in (currently unused)
		private short Damage = 10; // Global fireball damage value

		/// <summary>
		/// AI Function
		/// </summary>
		public override void AI()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) // If the code is not being run on multiplayer clients (and is on the server or singleplayer clients)
			{
				Lighting.AddLight(npc.Center, Color.Orange.ToVector3() * 1f); // Add Orange Lighting
				Lighting.AddLight(npc.Center, Color.Green.ToVector3() * 1f);  // Add Green  Lighting
				npc.TargetClosest(true); // Set the npc to target the closest
				Vector2 targetPosition = Main.player[npc.target].position; // Get the player co-ordinates
				if (targetPosition.Y < npc.Center.Y + (10 * CM.Y) && npc.velocity.Y > -4) // If the boss centre is less than 10 tiles above the player and the acceleration is less than -4
					npc.velocity.Y -= DefaultAcceleration.Y; // Increase the cumulative upwards acceleration
				if (targetPosition.Y > npc.Center.Y + (10 * CM.Y) && npc.velocity.Y < 4) // If the boss centre is greater than 10 tiles above the player and the acceleration is less than 4
					npc.velocity.Y += DefaultAcceleration.Y; // Increase the cumulative downwards acceleration

				Action[] Phases = new Action[] { Phase1, Phase2 }; // Array for the different phase functions
				Phases[Phase - 1](); // Run the function
			}
		}

        #region Phase 1
		/// <summary>
		/// Function for running all things Phase 1
		/// </summary>
        private void Phase1()
		{
			Vector2 targetPosition = Main.player[npc.target].position; // Get the position of the closest target/player
			npc.velocity.X = (cooldown > 0) ? 0 : (targetPosition.X - npc.Center.X) / (2 * CM.X); // if the cool down is not complete make the boss have no velocity, else determine it from player position
			timer--; // reduce timer by 1
			cooldown--; // reduce cooldown by 1
			if (timer < 0) // if the timer has finished
			{
				currentAttack = Attack.None; // set the attack to none
				int RandomTime = WorldGen.genRand.Next(5); // generate a random time
				switch (WorldGen.genRand.Next(6) % 3) // randomize attack
				{
					case 0: // Tracking Attack
						cooldown = (int)(1.5f * tickSpeed); // set the cooldown to 1.5 seconds
						timer = RandomTime + (1 * tickSpeed) + cooldown; // set the time to a random time plus the cooldown
						npc.velocity.X = 0; // no velocity
						currentAttack = Attack.Tracking; // set the attack to tracking
						break;
					case 1: // Star Attack
						cooldown = (int)(1.5f * tickSpeed); // *
						timer = RandomTime + (4 * tickSpeed) + cooldown; // *
						npc.velocity.X = 0; // *
						currentAttack = Attack.Star; // set the attack to star
						break;
					case 2: // Sweep/hellfire Attack
						cooldown = (int)(1.5f * tickSpeed); // *
						timer = RandomTime + (5 * tickSpeed) + cooldown; // *
						npc.velocity.X = 0; // *
						currentAttack = (WorldGen.genRand.Next(2) == 0) ? Attack.SweepRight : Attack.SweepLeft; // if the random number is 0 set the attack to sweep right else set it to sweep left
						break;
				}
			}
			if (currentAttack == Attack.Star && cooldown % (0.25f * tickSpeed) == 0) StarAttack(); // run the star attack every ¼ second
			else if (currentAttack == Attack.Tracking && cooldown % (0.25f * tickSpeed) == 0) TrackingAttack(); // run the tracking attack every ¼ second
			else if ((currentAttack == Attack.SweepRight || currentAttack == Attack.SweepLeft) && cooldown % 2 == 0) SweepAttack(); // run the sweep attack every ⅟₃₀ of a second
		}

		/// <summary>
		/// Phase 1 attack that summons many projectiles sweep from either left to right or right to left
		/// </summary>
		private void SweepAttack()
		{
			float SweepDirection = (currentAttack == Attack.SweepLeft) ? 1 : -1; // if the attack is sweep left set the direction (multiplier) to 1 else set it to -1
			int r = WorldGen.genRand.Next(30); // skip projectiles random number (also used for projectile randomisation
			if (r == 0) return; // if r is 0, skip the following code
			Vector2 direction = new Vector2(0f, 1f); // set the direction to down
			float speed = 4.5f; // set the speed to 4.5 (for readability)
			// TODO: FIX SPAWNPOSITION
			Vector2 SpawnPosition = new Vector2((npc.Center.X - (SweepDirection * cooldown * CM.X) - (45 * CM.X)), npc.Center.Y - (-r + 20 * CM.Y)); // calculate the spawn position WIP
			Projectile.NewProjectile(SpawnPosition, direction * speed, ProjectileID.Fireball, 10, 0f, Main.myPlayer); // Summon a projectile
        }

		/// <summary>
		/// Phase 1 attack that summons 5 projectiles that are shot at the tracked player
		/// </summary>
		private void TrackingAttack()
		{
			// if the cooldown is at ~0.5 seconds or 1 second exactly run the attacks (this causes it to attack once per time)
			if (cooldown == (int)(0.5f * tickSpeed) || cooldown == (int)(1f * tickSpeed))
            {
				TrackingProjectile(npc.Center); // Summon the trackin projectile at the npc center
				TrackingProjectile(new Vector2(npc.Center.X + 10*CM.X, npc.Center.Y)); // Summon 10 tiles to the left
				TrackingProjectile(new Vector2(npc.Center.X + 30 * CM.X, npc.Center.Y)); //  "   30   "     "     "
				TrackingProjectile(new Vector2(npc.Center.X - 10*CM.X, npc.Center.Y)); // Summon 10 tiles to the right
				TrackingProjectile(new Vector2(npc.Center.X - 30 * CM.X, npc.Center.Y)); //  "   30   "     "     "
			}
		}

		/// <summary>
		/// Summons at projectile targetting the closest player
		/// </summary>
		/// <param name="SpawnPosition">Where to shoot the projective from</param>
		private void TrackingProjectile(Vector2 SpawnPosition)
        {
			Vector2 targetPosition = Main.player[npc.target].Center; // Target the centre of the player
			Vector2 direction = targetPosition - SpawnPosition; // Calculate the direction of the projectile
			direction.Normalize(); // Changes direction to be within ¯1 < x < 1 (same for y)
			float speed = 10f; // Variable speed for readability
			Projectile.NewProjectile( // Summon the projectile
				SpawnPosition, // At the spawn position
				direction * speed, // In the direction of the target at the specified speed
				ProjectileID.Fireball, // Summons the fireball projectile
				Damage, // With a damage of 10
				0f, // With 0 knockback
				Main.myPlayer // With owner of the player so all projectiles will deal damage
			);
        }

		/// <summary>
		/// Phase 1 attack that summons 8 projectiles per turn which are in an 8-pointed star shape
		/// </summary>
		private void StarAttack()
		{
			// -=-=- Vector arrays of the directions -=-=-
			// Straight directions
			Vector2[] strDirections = new Vector2[] { new Vector2(0f, 10f), new Vector2(10f, 10f), new Vector2(10f, 0f), new Vector2(10f, -10f), new Vector2(0f, -10f), new Vector2(-10f, -10f), new Vector2(-10f, 0), new Vector2(-10f, 10f) };
			// Diagonal directions
			Vector2[] diaDirections = new Vector2[] { new Vector2(5f, 10f), new Vector2(10f, 5f), new Vector2(10f, -5f), new Vector2(5f, -10f), new Vector2(-5f, -10f), new Vector2(-10f, -5f), new Vector2(-10f, 5f), new Vector2(-5f, 10f) };
			if (cooldown == (int)(1.25f * tickSpeed) || cooldown == (int)(0.75f * tickSpeed)) // if the cooldown is either ~1.25 seconds or ~0.75 seconds...
				strDirections.ToList().ForEach(n => Projectile.NewProjectile(npc.Center, n, ProjectileID.Fireball, Damage, 0f)); // ... iterate through the directions and summon the projectiles that way
			else if (cooldown == (int)(1f * tickSpeed) || cooldown == (int)(0.5f * tickSpeed)) // if the cooldown is either ~1 second or ~0.5 seconds...
				diaDirections.ToList().ForEach(n => Projectile.NewProjectile(npc.Center, n, ProjectileID.Fireball, Damage, 0f)); // ... iterate through the directions and summon the projectiles that way
		}
        #endregion

        #region Phase 2
        private void Phase2()
		{

		}
        #endregion

        int count = 0; // Counter for frames
		/// <summary>
		/// Finds what frame of animation is right now
		/// </summary>
		/// <param name="frameHeight">How tall the frames are</param>
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction; // Set the sprite direction to the reverse of the direction of the npc
            count = ((int)npc.frameCounter == 8) ? 1 : (int)npc.frameCounter / 4; // if the framecounter is 8 return 1 else return the quotient of framecounter and 4
            npc.frame.Y = count * frameHeight; // set the frame y to count × height
			npc.frameCounter = (npc.frameCounter == 12) ? 0 : npc.frameCounter + 1; // if the frame counter is 12 else increase it by 1
		}
    }
}
