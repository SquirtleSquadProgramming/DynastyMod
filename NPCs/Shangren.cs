using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace XiJinpingVirusProject.NPCs
{
    [AutoloadHead]
    public class Shangren : ModNPC
    {
        private string[] PotenialNPCNames = new string[] { "Fang Li", "Zhang Wei", "Wang Wei", "Li Wei", "Li Hua" };

        public override string Texture
        {
            get { return "XiJinpingVirusProject/NPCs/Shangren"; }
        }

        public override bool Autoload(ref string name)
        {
            name = "Shangren";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0; // TBE
            NPCID.Sets.AttackTime[npc.type] = 90; // TBE
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 12;
            npc.defense = 17;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        //public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        //{
        //    for(int k = 0; k < 255; k++)
        //    {
        //        Player player = Main.player[k];
        //        if(!player.active)
        //            continue;

        //        foreach(Item item in player.inventory)
        //            if (item.type == mod.ItemType("sex"))
        //                return true;
        //    }
        //    return false;
        //}

        public override string TownNPCName()
        {
            return PotenialNPCNames[WorldGen.genRand.Next(PotenialNPCNames.Length)];
        }

        // TBE
        public override string GetChat()
        {
            return base.GetChat();
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Custom";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if(firstButton)
            {
                shop = true;
            }
            else
            {
                Main.npcChatText = "Tset test";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(2);
            shop.item[nextSlot].value = 10000;
            nextSlot++;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), 2);
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 25;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.DemonScythe;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return base.CheckConditions(left, right, top, bottom);
        }
    }
}
