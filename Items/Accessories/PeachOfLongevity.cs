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
            Item.width = 34;
            Item.height = 32;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
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
