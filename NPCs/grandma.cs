using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 60f;
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
			banner = Item.NPCtoBanner(NPCID.Zombie);
			bannerItem = Item.BannerToItem(banner);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
		}
		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), (short)ModContent.ItemType<Items.RedEnvelope>());
		}
	}
}
