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
            npc.value = 300f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => (Main.hardMode) ? SpawnCondition.OverworldNightMonster.Chance * 0.01f : 0.0f;

        private const int AI_State_Slot = 0;

        public float AI_State
        {
            get => npc.ai[AI_State_Slot];
            set => npc.ai[AI_State_Slot] = value;
        }

        public override void AI()
        {
            Lighting.AddLight(new Vector2(npc.position.X - 0.5f, npc.position.Y), Color.Orange.ToVector3() * 2.0f);
            if (AI_State == 0)
            {
                npc.TargetClosest(true);
                npc.velocity = new Vector2(0, 0); 

                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 1500f)
                    AI_State = 1;
            }
            else if (AI_State == 1)
            {
                npc.TargetClosest(true);
                npc.velocity = new Vector2(npc.direction * 0.5f, (Main.player[npc.target].position.Y - npc.position.Y) * 0.02f);
                if (npc.HasValidTarget && Main.player[npc.target].Distance(npc.Center) < 200f)
                    AI_State = 2;
                else if (!npc.HasValidTarget || Main.player[npc.target].Distance(npc.Center) > 1500f)
                    AI_State = 0;
            }
            else if (AI_State == 2)
            {
                npc.TargetClosest(true);
                npc.velocity = new Vector2(npc.direction * 0.3f + (-npc.direction * 0.1f), (Main.player[npc.target].position.Y - npc.position.Y) * 0.04f);
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
