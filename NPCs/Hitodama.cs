using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace DynastyMod.NPCs
{
    public class Hitodama : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hitodama");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.aiStyle = -1;
            NPC.damage = 43;
            NPC.defense = 30;
            NPC.lifeMax = 200;
            NPC.alpha = 20;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit44;
            NPC.DeathSound = SoundID.Item110;
            AIType = -1;
            NPC.value = 650f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => (Main.hardMode) ? SpawnCondition.OverworldNightMonster.Chance * 0.01f : 0.0f;

        private int AI_State_Slot = 0;

        public float AI_State
        {
            get => NPC.ai[AI_State_Slot];
            set => NPC.ai[AI_State_Slot] = value;
        }

        public override void AI()
        {
            Vector2 velocityBias = new Vector2(WorldGen.genRand.NextFloat(1.0f) - 0.5f, WorldGen.genRand.NextFloat(1.0f) - 0.5f);
            Lighting.AddLight(new Vector2(NPC.position.X - 0.5f, NPC.position.Y), Color.Orange.ToVector3() * 2.0f);
            if (AI_State == 0)
            {
                NPC.alpha = 100;
                NPC.TargetClosest(true);
                NPC.velocity = velocityBias;
                if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 1500f)
                    AI_State = 1;
            }
            else if (AI_State == 1)
            {
                NPC.alpha = 0;
                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 1f + velocityBias.X, (Main.player[NPC.target].position.Y - NPC.position.Y) * 0.02f + velocityBias.Y);
                if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 300f)
                {
                    AI_State = 2;
                    SoundEngine.PlaySound(SoundID.NPCDeath40, NPC.position);
                }
                else if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 1500f)
                    AI_State = 0;
            }
            else if (AI_State == 2)
            {
                NPC.TargetClosest(true);
                Vector2 targetDifference = NPC.position - Main.player[NPC.target].position;

                if (Math.Sqrt(Math.Pow(targetDifference.X, 2.0f) + Math.Pow(targetDifference.Y, 2.0f)) > 100f)
                    NPC.velocity = new Vector2(NPC.direction * 3f + velocityBias.X, (Main.player[NPC.target].position.Y - NPC.position.Y + velocityBias.Y) * 0.05f);
                else
                    NPC.velocity = new Vector2(NPC.velocity.X * 1.05f + velocityBias.X, (Main.player[NPC.target].position.Y - NPC.position.Y + velocityBias.Y) * 0.05f);
                
                if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 1500f)
                    AI_State = 0;
            }
        }

        private int count = 0;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            count = (int)(NPC.frameCounter / 5);
            if (count == 5) NPC.frameCounter = 0;
            NPC.frame.Y = count * frameHeight;
            NPC.frameCounter++;
        }

        /*public override void NPCLoot()
        {
            Item.NewItem(NPC.getRect(), (short)ModContent.ItemType<Items.SoulofSpite>());
        }*/


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Blackout, 1000);
        }
    }  
}
