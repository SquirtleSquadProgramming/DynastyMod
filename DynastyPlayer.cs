using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;



namespace DynastyMod
{
    // ModPlayer classes provide a way to attach data to Players and act on that data.
    public class DynastyPlayer : ModPlayer
    {
        public bool paperCrane;

        private int[] flyingAIStyles = { 2, 14, 22, 17, 11, 4, 5 };

        public override void ResetEffects()
        {
            paperCrane = false;
        }


        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
  
            // If the enemy is a flying type enemy and the player has a paper crane equipped as an accessory, award copper coins

            if (flyingAIStyles.Contains(npc.aiStyle) && paperCrane)
            {
                player.QuickSpawnItem(ItemID.CopperCoin, Convert.ToInt32(damage * 2));
            }
        }
    }
}