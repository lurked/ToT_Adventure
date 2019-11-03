using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class GameMap
    {
        public Dictionary<Vector2, Tile> Map;
        public Player player;

        public GameMap()
        {
            Map = GenerateMap("base");
            player = new Player();
        }

        private Dictionary<Vector2, Tile> GenerateMap(string mapType)
        {
            string imgName_Dirt = "dirt_" + ToT.Settings.TileSize.X.ToString();
            string imgName_Plains = "cotton_green_" + ToT.Settings.TileSize.X.ToString();
            Dictionary<Vector2, Tile> tMap = new Dictionary<Vector2, Tile>();
            Tile tTile = new Tile();
            switch(mapType)
            {
                case "base":
                    tMap.Add(new Vector2(), tTile);
                    tTile = new Tile(imgName_Dirt, Toolbox.TileType.Dirt);
                    tMap.Add(new Vector2(1, 0), tTile);
                    tTile = new Tile(imgName_Dirt, Toolbox.TileType.Dirt);
                    tMap.Add(new Vector2(0, 1), tTile);
                    tTile = new Tile(imgName_Dirt, Toolbox.TileType.Dirt);
                    tMap.Add(new Vector2(-1, 0), tTile);
                    tTile = new Tile(imgName_Dirt, Toolbox.TileType.Dirt);
                    tMap.Add(new Vector2(0, -1), tTile);
                    tTile = new Tile(imgName_Plains, Toolbox.TileType.Plains);
                    tMap.Add(new Vector2(1, 1), tTile);
                    tTile = new Tile(imgName_Plains, Toolbox.TileType.Plains);
                    tMap.Add(new Vector2(-1, -1), tTile);
                    tTile = new Tile(imgName_Plains, Toolbox.TileType.Plains);
                    tMap.Add(new Vector2(1, -1), tTile);
                    tTile = new Tile(imgName_Plains, Toolbox.TileType.Plains);
                    tMap.Add(new Vector2(-1, 1), tTile);
                    break;
            }
            return tMap;
        }
    }
}
