using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Placeables
{
    public class GemCutter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A device designed to cut, shape, and polish jade.");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.LightRed;
            //Item.useStyle = ItemUseStyleID.SwingThrow;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.GemCutter>();
        }

        
    }
}
