using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public override void ResetEffects()
        {
            paperCrane = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            // If the player is moving faster than a velocity of 3 and they have a paper crane equipped, award silver coins on damage
            if ((player.bodyVelocity.X > 3 || player.bodyVelocity.Y > 3 || player.bodyVelocity.X < 3 || player.bodyVelocity.Y < 3) && paperCrane)
            {
                player.QuickSpawnItem(ItemID.SilverCoin, Convert.ToInt32(damage / 12));
            }
        }
    }
}