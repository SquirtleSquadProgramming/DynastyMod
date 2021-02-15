using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace DynastyMod.Items
{
    public class TempItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is a temporary item\nYou shouldn't have this!");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            (short[], int[], int)[] Recipes = new (short[], int[], int)[] {
                (new short[] { ItemID.DirtBlock }, new int[] { 1 }, 2),
                (new short[] { ItemID.SnowBlock }, new int[] { 2 }, 3),
                (new short[0], new int[0], 1)
            };

            foreach ((short[], int[], int) R in Recipes)
            {
                recipe = new ModRecipe(mod);
                if (R.Item1.Length != 0 && R.Item1.Length == R.Item2.Length)
                    for (int i = 0; i < R.Item1.Length; i++)
                        recipe.AddIngredient((int)R.Item1[i], R.Item2[i]);
                recipe.SetResult(this, R.Item3);
                recipe.AddRecipe();
            }
        }
    }
}