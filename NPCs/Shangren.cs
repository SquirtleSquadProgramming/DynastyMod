using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace DynastyMod.NPCs
{
    [AutoloadHead]
    public class Shangren : ModNPC
    {
        public override string Texture
        {
            get { return "DynastyMod/NPCs/Shangren"; }
        }
        //Not Used anymore
        /*public override bool Autoload(ref string name)
        {
            name = "Shangren";
            return mod.Properties.Autoload;
        }*/

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Fang Li",
                "Zhang Wei",
                "Wang Wei",
                "Li Wei",
                "Li Hua"
            };
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0; // TBE
            NPCID.Sets.AttackTime[NPC.type] = 90; // TBE
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 12;
            NPC.defense = 17;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
            TownNPCStayingHomeless = true;

            //Create Shop here
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
        public override void AI()
        {
            NPC.homeless = true;
        }
        // TBE
        public override string GetChat()
        {
            string[] Lines = new string[] { "Never do to others what you would not like them to do to you.", "Wheresoever you go, go with all your heart.", "Our greatest glory is not in never falling, but in rising every time we fall.", "I hear and I forget. I see and I remember. I do and I understand.", "It does not matter how slowly you go so long as you do not stop." };
            return Lines[WorldGen.genRand.Next(Lines.Length)];
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
                shop = true;
            
        }

        public override void SetupShop(Chest shop, ref int slot)
        {
            //(ID,  vanilla), price
            (short, int)[] ShopItems = new (short, int)[] { (ItemID.DynastyWood, 50), (ItemID.RedDynastyShingles, 50), (ItemID.BlueDynastyShingles, 50), ((short)ModContent.ItemType<Items.BlankScroll>(), 10000) };

            foreach ((short, int) item in ShopItems)
            {
                shop.item[slot].SetDefaults(item.Item1);
                shop.item[slot].value = item.Item2;
                slot++;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcloot) => npcloot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1));

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
            projType = ProjectileID.DemonScythe; // Could be some magic type of weapon, like a scroll or a star based weapon
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom) => base.CheckConditions(left, right, top, bottom);
        public override ITownNPCProfile TownNPCProfile()
        {
            return new ShangrenProfile();
        }
    }
    public class ShangrenProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("DynastyMod/NPCs/Shangren");
            //From example mod used to put party hat on
           // if (npc.altTexture == 1)
               // return ModContent.Request<Texture2D>("ExampleMod/Content/NPCs/ExamplePerson_Party");

            return ModContent.Request<Texture2D>("DynastyMod/NPCs/Shangren");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("DynastyMod/NPCs/Shangren_Head");
    }
}
