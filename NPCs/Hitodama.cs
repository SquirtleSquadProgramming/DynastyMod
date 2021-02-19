using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DynastyMod.NPCs
{
    public class Hitodama : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hitodama");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = -1;
            npc.damage = 43;
            npc.defense = 30;
            npc.lifeMax = 200;
            npc.alpha = 20;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit44;
            npc.DeathSound = SoundID.Item110;
            aiType = -1;
            npc.value = 650f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => (Main.hardMode) ? SpawnCondition.OverworldNightMonster.Chance * 0.01f : 0.0f;

        private int AI_State_Slot = 0;

        public float AI_State
        {
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
        }

        public override void AI()
        {
            Vector2 velocityBias = new Vector2(WorldGen.genRand.NextFloat(1.0f) - 0.5f, WorldGen.genRand.NextFloat(1.0f) - 0.5f);
            Lighting.AddLight(new Vector2(npc.position.X - 0.5f, npc.position.Y), Color.Orange.ToVector3() * 2.0f);
            if (AI_State == 0)
            {
                npc.alpha = 100;
                npc.TargetClosest(true);
                npc.velocity = velocityBias;
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 1500f)
                    AI_State = 1;
            }
            else if (AI_State == 1)
            {
                npc.alpha = 0;
                npc.TargetClosest(true);
                npc.velocity = new Vector2(npc.direction * 1f + velocityBias.X, (Main.player[npc.target].position.Y - npc.position.Y) * 0.02f + velocityBias.Y);
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 300f)
                {
                    AI_State = 2;
                    Main.PlaySound(SoundID.NPCDeath40, npc.position);
                }
                else if (!npc.HasValidTarget || Main.player[npc.target].Distance(npc.Center) > 1500f)
                    AI_State = 0;
            }
            else if (AI_State == 2)
            {
                npc.TargetClosest(true);
                Vector2 targetDifference = npc.position - Main.player[npc.target].position;

                if (Math.Sqrt(Math.Pow(targetDifference.X, 2.0f) + Math.Pow(targetDifference.Y, 2.0f)) > 100f)
                    npc.velocity = new Vector2(npc.direction * 3f + velocityBias.X, (Main.player[npc.target].position.Y - npc.position.Y + velocityBias.Y) * 0.05f);
                else
                    npc.velocity = new Vector2(npc.velocity.X * 1.05f + velocityBias.X, (Main.player[npc.target].position.Y - npc.position.Y + velocityBias.Y) * 0.05f);
                
                if (!npc.HasValidTarget || Main.player[npc.target].Distance(npc.Center) > 1500f)
                    AI_State = 0;
            }
        }

        private int count = 0;
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            count = (int)(npc.frameCounter / 5);
            if (count == 5) npc.frameCounter = 0;
            npc.frame.Y = count * frameHeight;
            npc.frameCounter++;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), (short)ModContent.ItemType<Items.SoulofSpite>());
        }
    }  
}
