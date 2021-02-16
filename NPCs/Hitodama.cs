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
            Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs.
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.aiStyle = 22;
            npc.damage = 68;
            npc.defense = 30;
            npc.lifeMax = 200;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit44;
            npc.DeathSound = SoundID.Item110;
            aiType = -1;
            npc.value = 25f;
            
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.01f;
            }
            else
            {
                return 0.0f;
            }
        }

        public override void AI()
        {
            npc.TargetClosest(true);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            int count;
            if (npc.frameCounter > Main.npcFrameCount[npc.type] * 10)
            {
                count = 0;
                npc.frameCounter = 0;
            }
            else
            {
                
                switch((int)npc.frameCounter)
                {
                    case 0:
                        count = 0;
                        break;
                    case 10:
                        count = 1;
                        break;
                    case 20:
                        count = 2;
                        break;
                    case 30:
                        count = 3;
                        break;
                    case 40:
                        count = 4;
                        break;
                    case 50:
                        count = 5;
                        break;
                    default:
                        count = 0;
                        break;
                }
            }
            npc.frame.Y = count * 32;
            npc.frameCounter++;
            mod.Logger.Info(npc.frameCounter);
        }
    }
    
}
