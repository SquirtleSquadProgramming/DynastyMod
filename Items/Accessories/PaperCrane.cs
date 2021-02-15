using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Accessories
{
    public class PaperCrane : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Awards money for damage dealt by flying enemies.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DynastyPlayer>().paperCrane = true; 
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RedEnvelope", 1);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
