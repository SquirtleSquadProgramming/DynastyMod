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
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(silver: 24);
            item.rare = ItemRarityID.LightRed;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            // item.createTile = ModContent.TileType<Tiles.CutJade>(); temporary because i cbf to sprite another fucking tilesheet for another block
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<RawJade>(), 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
