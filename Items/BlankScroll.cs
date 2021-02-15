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
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
        }
    }
}
