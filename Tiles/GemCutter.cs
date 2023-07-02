using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DynastyMod.Tiles
{
    public class GemCutter : ModTile
    {
        //NGL I dont think that ModTile has a SetDefaults AnyMore prob has a different name
        /*public override void SetDefaults()
		{
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Gem Cutter");
			AddMapEntry(new Color(200, 200, 200), name);
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.WorkBenches };
		}*/

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			//Item.NewItem has changed now
			//Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<Items.Placeables.GemCutter>());
		}
	}
}
