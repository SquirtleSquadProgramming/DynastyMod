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
		public int dir;
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
			//	aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
			if (WorldGen.genRand.Next(2) == 0)
				npc.spriteDirection = -1;
			else
				npc.spriteDirection = 1;
		}
		//Grandma prob wont spawn random might be event npc
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.9f;
		}*/
		public Player lasthit;
		public override void ModifyHitByItem(Player player,Item item,ref int damage,ref float knockback,ref bool crit) => lasthit = player;

		public override void NPCLoot()
        {
			lasthit.AddBuff(BuffID.Cursed, 300, true);
			Item.NewItem(npc.getRect(), (short)ModContent.ItemType<Items.RedEnvelope>());
		}
		public dynamic lastX;
		public int timer = 0;
		public bool hit = false;
		//For a wait
		public int tickCounter = 0;
		public int lastTick;
		public int t = 0;
		public override void AI()
        {
			if (t > 9)
			{
				npc.spriteDirection = npc.spriteDirection * -1;
				t = 0;
			}
			//mod.Logger.Info("This is an informational log	" + t + "		" + npc.position.X/16);
			tickCounter++;
			if (!hit)
			{
				npc.velocity.X = 0.7f* npc.spriteDirection;
			}
			if (npc.position.X == lastX && timer != 3)
			{
				npc.velocity = new Vector2(npc.direction * 1, -4f);
				npc.velocity.X = 2f;
				timer++;
				lastTick = tickCounter;
				t++;
				mod.Logger.Info("Grandam Jump	" + t);
			}
            /*if (npc.position.X != lastX)
            {
				t = 0;
				mod.Logger.Info("Grandam Walk	" + npc.position.X + "	" + lastX);
			}*/


			if (timer == 3)
			{
				//lastX = 0;
				if (lastTick + 60 == tickCounter)
				{
					timer = lastTick = tickCounter = 0;
				}	
			}
			else
				lastX = npc.position.X;
			

		}
		public override bool StrikeNPC(ref double damage,int defense,ref float knockback,int hitDirection,ref bool crit)
		{
			npc.velocity.X = 2f*hitDirection;
			hit = true;
			npc.spriteDirection = hitDirection*1;
			return true;
		}
	}
}
