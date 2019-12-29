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
        public Dictionary<Vector2, LevelTile> Tileset;
        public Dictionary<Vector2, int[,]> Map;
        public Dictionary<int, Dictionary<Vector2, Thing>> Things;
        //Layers :
        //0 = Resources
        //1 = Buildings
        public Vector2 StartPosition { get; set; }
        public Vector2 Size { get; set; }
        
        public Level(string lvlBase = "")
        {
            if (Things == null)
                Things = new Dictionary<int, Dictionary<Vector2, Thing>>();
            //if (StartPosition == null)
            StartPosition = new Vector2(1, 1);

            if (Tileset == null)
            {
                do
                {
                    Map = RLGenerator.GenerateMap(lvlBase);
                } while (Map == null);

                Tileset = GenerateTilesetFromMap(Map);
            }
        }

        private Dictionary<Vector2, LevelTile> GenerateTilesetFromMap(Dictionary<Vector2, int[,]> map)
        {
            Dictionary<Vector2, LevelTile> tileset = new Dictionary<Vector2, LevelTile>();
            Vector2 tPos = Vector2.Zero;
            LevelTile tTile;
            foreach (KeyValuePair<Vector2, int[,]> tRoom in map)
            {
                for (int i = 0; i <= tRoom.Value.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= tRoom.Value.GetUpperBound(1); j++)
                    {
                        tPos = new Vector2(tRoom.Key.X + i, tRoom.Key.Y + j);
                        if (i == 0
                            || j == 0
                            || i == tRoom.Value.GetUpperBound(0) 
                            || j == tRoom.Value.GetUpperBound(1))
                            tTile = new LevelTile
                            {
                                Walkable = false
                            };
                        else
                            tTile = new LevelTile();
                        tTile.ImageName = Toolbox.GetImgName(Toolbox.TileType.Plains, tRoom.Value.GetUpperBound(0), tRoom.Value.GetUpperBound(1), i, j);
                        tileset.Add(tPos, tTile);
                    }
                }
            }

            return tileset;
        }
    }
}
