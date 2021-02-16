using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DynastyMod.Items.Accessories
{
    public class PeachOfLongevity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+40 maximum mana. Has a rare chance to award gold coins on hitting a monster.");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            player.statManaMax2 += 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DynastyPlayer>().peachOfLongevity = true;
        }
    }
}
