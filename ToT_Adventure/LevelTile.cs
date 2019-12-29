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
            if (ImageName == "")
                ImageName = "tile_home_middlemiddle";
        }
    }
}
 