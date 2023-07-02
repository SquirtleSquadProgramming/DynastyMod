using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Placeables
{
    public class CutJade : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 24);
            Item.rare = ItemRarityID.LightRed;
            //Item.useStyle = ItemUseStyleID.SwingThrow;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.consumable = true;
            // Item.createTile = ModContent.TileType<Tiles.CutJade>(); temporary because i cbf to sprite another fucking tilesheet for another block
        }
        //They Changed This 
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<RawJade>(1)
                .AddTile<Tiles.GemCutter>()
                .Register();
                   
        }
    }
}
