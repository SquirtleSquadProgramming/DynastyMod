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
			npc.spriteDirection = 1;
			npc.velocity.X = 0.7f;
			
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
		public override bool StrikeNPC(ref double damage,int defense,ref float knockback,int hitDirection,ref bool crit)
		{
			mod.Logger.Info("Direction is " + hitDirection);
			if (hitDirection == 1)
				npc.velocity.X = 2f;
			else if(hitDirection == -1)
				npc.velocity.X = -2f;
			npc.spriteDirection = hitDirection*1;
			return true;
		}
	}
}
