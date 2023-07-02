using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DynastyMod.Items
{
    public class SoulofSpite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Spite");
            Tooltip.SetDefault("'The essence of betrayed ancestors.'");

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4)); // ticksperframe, frameCount
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Orange;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }
    }
}
