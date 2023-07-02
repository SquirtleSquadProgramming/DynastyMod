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
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DynastyPlayer>().paperCrane = true; 
        }
        //They Changed this system Look online - Nathan 2023
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<RedEnvelope>()
                .AddIngredient(ItemID.Book)
                .Register();
        }
    }
}
