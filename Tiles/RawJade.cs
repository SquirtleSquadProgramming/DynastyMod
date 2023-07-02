using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using On.Terraria.Audio;

namespace DynastyMod.Tiles
{
    public class RawJade : ModTile
    {
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; 
			//This isnt a thing anymore
			//Main.tileValue[Type] = 660; // between titanium and chlorophyte
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 700;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Raw Jade");
			AddMapEntry(new Color(72, 222, 42), name);

			DustType = 46; // greenish dust

			//Drop is now a function not variable 
			//drop = ModContent.ItemType<Items.Placeables.RawJade>();

			//Sound stuff is all messed up they changed everything 
			//SoundType = SoundID.Tink;
			//SoundStyle = 1;
			MinPick = 190; // can mine with titanium and above 
		}
	}
}
