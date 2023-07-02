using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using IL.Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.ItemDropRules;
//Why tf are there three differnt ones
using ItemDropRule = Terraria.GameContent.ItemDropRules.ItemDropRule;
namespace DynastyMod.NPCs
{

    public class grandma : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grandma");
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
			//NPC.DeathSound = new SoundStyle("Sounds/grandma_death",SoundType.Sound);
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = -1;
			AIType = NPCID.Zombie;
			AnimationType = NPCID.Zombie;
			Banner = NPCID.Zombie;
			BannerItem = Item.BannerToItem(Banner);
			NPC.spriteDirection = 1;
			NPC.velocity.X = 0.7f;
			
		}
		//Grandma prob wont spawn random might be event npc
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.9f;
		}
		public override void ModifyNPCLoot(NPCLoot npcloot)
		{
			npcloot.Add(ItemDropRule.Common(ModContent.ItemType<Items.RedEnvelope>(), 2)) ;
			//This was old code that dont work
            //Item.NewItem(NPC.GetSource_FromThis(),NPC.getRect(), (short)ModContent.ItemType<Items.RedEnvelope>());
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
