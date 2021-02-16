using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;

namespace DynastyMod.NPCs
{

    public class grandma : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grandma");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
		}
		
		public override void SetDefaults()
		{
			
			npc.width = 18;
			npc.height = 40;
			npc.damage = 0;
			npc.defense = 6;
			npc.lifeMax = 15;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/grandma_death"); ;
			npc.value = 60f;
			npc.knockBackResist = 0.5f;
			npc.aiStyle = -1;
			//aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
		}
		//Grandma prob wont spawn random might be event npc
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.9f;
		}*/
		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), (short)ModContent.ItemType<Items.RedEnvelope>());
		}
		public override void OnHitByProjectile(Projectile projectile,int damage,float knockback,bool crit)
		{
			/*if (targetPosition.X < npc.position.X && npc.velocity.X > -8) 
			{ 
				npc.velocity.X += 0.22f; // accelerate to the left
			}

			if (targetPosition.X > npc.position.X && npc.velocity.X < 8)
			{
				//npc.spriteDirection = 1;
				npc.velocity.X -= 0.22f; // accelerate to the right
			}*/
		}

		public override void AI()
		{
			npc.TargetClosest(false);		
			npc.spriteDirection = 1;
			npc.velocity.X = 0.7f;		
		}
	}
}
