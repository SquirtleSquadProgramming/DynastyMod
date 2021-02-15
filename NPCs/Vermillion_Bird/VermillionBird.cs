using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace DynastyMod.NPCs.Vermillion_Bird
{
    public class VermillionBird : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vermillion Bird");
            Main.npcFrameCount[npc.type] = 1;
        }

		public override void SetDefaults()
		{
			npc.aiStyle = -1; // need to add ai
			npc.lifeMax = 2000;
			npc.damage = 100;
			npc.defense = 55;
			npc.knockBackResist = 0f;
			npc.width = 400;
			npc.height = 400;
			npc.value = Item.buyPrice(0, 20, 0, 0);
			npc.npcSlots = 15f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.buffImmune[24] = true;
			music = MusicID.Boss2;
		}


	}
}
