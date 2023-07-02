using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace DynastyMod.Items
{
    public class BlankScroll : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A blank scroll used for crafting powerful scrolls...");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
