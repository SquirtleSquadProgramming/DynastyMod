using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DynastyMod.Items
{
	public class RedEnvelope : ModItem
	{
		/*The sprite is just a place holder do not use its shitty and I made it in 30 secs
		 * 
		 * 
		 */
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("From Grandma");
		}

		public override void SetDefaults()
		{
			//Resize this depending on sprite size *Note bullet sprite is not shot only used as the inv item
			item.width = 10;
			item.height = 10;
			item.maxStack = 100;
			item.consumable = true;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
		}
		public override bool CanRightClick() => true;
		public override void RightClick(Player player) => player.QuickSpawnItem(ItemID.GoldCoin,  WorldGen.genRand.Next(5)+5);
		
	}
}
