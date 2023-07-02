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
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            (short[], int[], int)[] Recipes = new (short[], int[], int)[] {
                (new short[] { ItemID.DirtBlock }, new int[] { 1 }, 2),
                (new short[] { ItemID.SnowBlock }, new int[] { 2 }, 3),
                (new short[0], new int[0], 1)
            };
            //Recipe class has changed and this is broken now
            /*foreach ((short[], int[], int) R in Recipes)
            {
                recipe = new Recipe(Mod);
                if (R.Item1.Length != 0 && R.Item1.Length == R.Item2.Length)
                    for (int i = 0; i < R.Item1.Length; i++)
                        recipe.AddIngredient((int)R.Item1[i], R.Item2[i]);
                recipe.SetResult(this, R.Item3);
                recipe.AddRecipe();
            }*/
        }
    }
}