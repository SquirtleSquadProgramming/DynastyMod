using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace DynastyMod.Tiles
{
    public class RawJade : ModTile
    {
		public override void SetDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; 
			Main.tileValue[Type] = 660; // between titanium and chlorophyte
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 700;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Raw Jade");
			AddMapEntry(new Color(72, 222, 42), name);

			dustType = 46; // greenish dust

			drop = ModContent.ItemType<Items.Placeables.RawJade>();
			soundType = SoundID.Tink;
			soundStyle = 1;
			minPick = 190; // can mine with titanium and above 
		}
	}
}
