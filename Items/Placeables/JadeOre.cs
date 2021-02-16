using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Accessories
{
    public class JadeOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A precious stone praised for its durability and supposed magical qualities.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Green; // temp
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.JadeOre>();
        }

        
    }
}
