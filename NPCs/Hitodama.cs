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

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => (Main.hardMode) ? SpawnCondition.OverworldNightMonster.Chance * 0.01f : 0.0f;

        public override void AI()
        {
            Lighting.AddLight(new Vector2(npc.position.X - 0.5f, npc.position.Y), Color.Orange.ToVector3() * 0.5f); // Lighting (thought you might like this)
            npc.TargetClosest(true);
        }

        private int count = 0;
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            count = (int)(npc.frameCounter / 10);
            if (count == 5) npc.frameCounter = 0;
            npc.frame.Y = count * frameHeight;
            npc.frameCounter++;
        }
    }  
}
