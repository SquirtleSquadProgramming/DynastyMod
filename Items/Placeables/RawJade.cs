using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Placeables
{
    public class RawJade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A precious stone prized for its durability and supposed magical qualities.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.LightRed;
            //Item.useStyle = ItemUseStyleID.SwingThrow;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.RawJade>();
        }

        
    }
}
