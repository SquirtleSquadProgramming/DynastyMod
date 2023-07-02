using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;

namespace DynastyMod.NPCs
{

    public class grandma : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("Grandma");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
		}
		
		public override void SetDefaults()
		{
			
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 0;
			NPC.defense = 6;
			NPC.lifeMax = 15;
			NPC.HitSound = SoundID.NPCHit2;
			//look up how to use custom sounds 
			//NPC.DeathSound = SoundEngine.PlaySound(SoundType.Sound, "Sounds/grandma_death"); ;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = -1;
			//aiType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			//banner =
			//NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
			NPC.spriteDirection = 1;
			NPC.velocity.X = 0.7f;
			
		}
		//Grandma prob wont spawn random might be event npc
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.9f;
		}*/
		public override void ModifyNPCLoot(NPCLoot npcloot)
		{
			//Check Docs and rewrite
			//Item.NewItem(NPC.getRect(), (short)ModContent.ItemType<Items.RedEnvelope>());
		}

		public override bool StrikeNPC(ref double damage,int defense,ref float knockback,int hitDirection,ref bool crit)
		{
			//ModNPC.Logger.Info("Direction is " + hitDirection);
			if (hitDirection == 1)
				NPC.velocity.X = 2f;
			else if(hitDirection == -1)
				NPC.velocity.X = -2f;
			NPC.spriteDirection = hitDirection*1;
			return true;
		}
	}
}
