using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class LevelTile
    {
        public string ImageName { get; set; }
        public bool Visible { get; set; } = true;
        public bool Walkable { get; set; } = true;

        public LevelTile()
        {
            if (ImageName == "" || ImageName == null)
            {
                string imgPrefix = "terrain{tilesize}\\tile_home_middlemiddle";
                imgPrefix = imgPrefix.Replace("{tilesize}", ToT.Settings.LevelTileSize.X.ToString());
                ImageName = imgPrefix;
            }
        }
        public void SetImgRoad()
        {
            string imgPrefix = "terrain{tilesize}\\tile_montain_middlemiddle";
            imgPrefix = imgPrefix.Replace("{tilesize}", ToT.Settings.LevelTileSize.X.ToString());
            ImageName = imgPrefix;
        }
    }
}
 