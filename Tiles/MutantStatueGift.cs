using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace FargowiltasSouls.Tiles
{
    public class MutantStatueGift : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.LavaDeath = true;
            //TileObjectData.newTile.Origin = new Point16(0, 1);
            //TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Mutant Statue");
            AddMapEntry(new Color(255, 215, 0), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("MutantStatue"));
            Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType("Masochist"));
        }
    }
}