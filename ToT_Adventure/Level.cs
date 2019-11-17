using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class Level
    {
        public Dictionary<Vector2, Tile> Tileset;
        public Dictionary<int, Dictionary<Vector2, Thing>> Things;
        
        public Level()
        {
            if (Tileset == null)
                Tileset = new Dictionary<Vector2, Tile>();
            if (Things == null)
                Things = new Dictionary<int, Dictionary<Vector2, Thing>>();
        }
    }
}
