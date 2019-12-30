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
        public Dictionary<Vector2, Room> Map;
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

        private Dictionary<Vector2, LevelTile> GenerateTilesetFromMap(Dictionary<Vector2, Room> map)
        {
            Dictionary<Vector2, LevelTile> tileset = new Dictionary<Vector2, LevelTile>();
            Vector2 tPos = Vector2.Zero;
            LevelTile tTile;
            int iRoom = 0;
            int iRoomLength = 0;
            int iRoomHeight = 0;
            int iLastRoomLength = 0;
            int iLastRoomHeight = 0;
            int iLastRoomX = 0;
            
            foreach (KeyValuePair<Vector2, Room> tRoom in map)
            {
                for (int i = 0; i <= tRoom.Value.Size.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= tRoom.Value.Size.GetUpperBound(1); j++)
                    {
                        tPos = new Vector2(tRoom.Key.X + i, tRoom.Key.Y + j);
                        if (i == 0
                            || j == 0
                            || i == tRoom.Value.Size.GetUpperBound(0)
                            || j == tRoom.Value.Size.GetUpperBound(1))
                        {
                            tTile = new LevelTile
                            {
                                Walkable = false
                            };
                        }
                        else
                            tTile = new LevelTile();
                        tTile.ImageName = Toolbox.GetImgName(Toolbox.TileType.Plains, tRoom.Value.Size.GetUpperBound(0), tRoom.Value.Size.GetUpperBound(1), i, j);
                        tileset.Add(tPos, tTile);
                    }
                }
                iRoomLength = tRoom.Value.Size.GetUpperBound(0);
                iRoomHeight = tRoom.Value.Size.GetUpperBound(1);
                if (iRoom > 0)
                {
                    int xGate = 0, yGate = 0;
                    int xGate2 = 0, yGate2 = 0;
                    int xToOpen = 0, xToOpen2 = 0;
                    int yToOpen = 1, yToOpen2 = 0;

                    switch (tRoom.Value.GateOrientation)
                    {
                        case Toolbox.Orientation.North:
                            yGate = -1;
                            yGate2 = -1;
                            yToOpen = -2;
                            yToOpen2 = 0;
                            xGate = Toolbox.StaticRandom.Instance.Next(1, (iRoomLength < iLastRoomLength ? iRoomLength : iLastRoomLength) - 2);
                            xGate2 = xGate + 1;
                            xToOpen = xGate;
                            xToOpen2 = xGate2;
                            break;
                        case Toolbox.Orientation.East:
                            xGate = iRoomLength + 1;
                            xGate2 = iRoomLength + 1;
                            xToOpen = iRoomLength;
                            xToOpen2 = iRoomLength + 2;
                            yGate = Toolbox.StaticRandom.Instance.Next(1, (iRoomHeight < iLastRoomHeight ? iRoomHeight : iLastRoomHeight) - 2);
                            yGate2 = yGate + 1;
                            yToOpen = yGate;
                            yToOpen2 = yGate2;
                            break;
                        case Toolbox.Orientation.South:
                            yGate = iRoomHeight + 1;
                            yGate2 = iRoomHeight + 1;
                            yToOpen = iRoomHeight;
                            yToOpen2 = iRoomHeight + 2;
                            xGate = Toolbox.StaticRandom.Instance.Next(1, (iRoomLength < iLastRoomLength ? iRoomLength : iLastRoomLength) - 2);
                            xGate2 = xGate + 1;
                            xToOpen = xGate;
                            xToOpen2 = xGate2;
                            break;
                        case Toolbox.Orientation.West:
                            xGate = -1;
                            xGate2 = -1;
                            xToOpen = 0;
                            xToOpen2 = -2;
                            yGate = Toolbox.StaticRandom.Instance.Next(1, (iRoomHeight < iLastRoomHeight ? iRoomHeight : iLastRoomHeight) - 2);
                            yGate2 = yGate + 1;
                            yToOpen = yGate;
                            yToOpen2 = yGate2;
                            break;
                    }
                    tTile = new LevelTile();
                    tTile.SetImgRoad();
                    tPos = new Vector2(xGate, yGate) + tRoom.Key;
                    if (tileset.ContainsKey(tPos))
                        tileset[tPos] = tTile;
                    else
                        tileset.Add(tPos, tTile);

                    tTile = new LevelTile();
                    tTile.SetImgRoad();
                    tPos = new Vector2(xGate2, yGate2) + tRoom.Key;
                    if (tileset.ContainsKey(tPos))
                        tileset[tPos] = tTile;
                    else
                        tileset.Add(tPos, tTile);

                    tPos = new Vector2(xToOpen, yToOpen) + tRoom.Key;
                    tileset[tPos].Walkable = true;
                    tileset[tPos].SetImgRoad();
                    tPos = new Vector2(xToOpen2, yToOpen2) + tRoom.Key;
                    tileset[tPos].Walkable = true;
                    tileset[tPos].SetImgRoad();
                    tPos = new Vector2(xToOpen, yToOpen2) + tRoom.Key;
                    tileset[tPos].Walkable = true;
                    tileset[tPos].SetImgRoad();
                    tPos = new Vector2(xToOpen2, yToOpen) + tRoom.Key;
                    tileset[tPos].Walkable = true;
                    tileset[tPos].SetImgRoad();
                }
                iLastRoomLength = iRoomLength;
                iLastRoomHeight = iRoomHeight;
                iRoom++;
            }

            return tileset;
        }
    }
}
