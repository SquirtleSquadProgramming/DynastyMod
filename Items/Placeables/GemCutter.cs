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
            Tooltip.SetDefault("A precious stone prized for its durability and supposed magical qualities.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(silver: 20);
            item.rare = ItemRarityID.LightRed;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.GemCutter>();
        }

        
    }
}
