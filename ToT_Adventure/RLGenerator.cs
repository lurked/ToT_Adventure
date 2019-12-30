using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public static class RLGenerator
    {
        public static Dictionary<Vector2, Room> GenerateMap(string lvlBase)
        {
            Dictionary<Vector2, Room> tMap = new Dictionary<Vector2, Room>();
            int iX;
            int iY;
            int iLastX = 0;
            int iLastY = 0;
            int nbRoom;
            int LastRoomOri = -1; //0 = North, 1 = East, 2 = South, 3 = West
            int roomOri;
            bool bOverlapses;
            int maxLoops;
            Vector2 currentPos = Vector2.Zero;
            Vector2 tempPos = currentPos;

            nbRoom = Toolbox.StaticRandom.Instance.Next(6, 25);

            for (int iR = 0; iR < nbRoom; iR++)
            {
                maxLoops = 20;
                int[,] tArr;
                do
                {
                    iX = Toolbox.StaticRandom.Instance.Next(6, 19);
                    iY = Toolbox.StaticRandom.Instance.Next(6, 19);

                    int[,] arr = new int[iX, iY];
                    for (int i = 0; i < iX; i++)
                    {
                        for (int j = 0; j < iY; j++)
                        {
                            arr[i, j] = 1;
                        }
                    }
                    tArr = arr;

                    do
                    {
                        roomOri = Toolbox.StaticRandom.Instance.Next(0, 4);
                    } while (roomOri == LastRoomOri);

                    if (LastRoomOri != -1)
                    {
                        switch (roomOri)
                        {
                            case 0:
                                tempPos = new Vector2(currentPos.X, currentPos.Y - iY - 1);
                                break;
                            case 1:
                                tempPos = new Vector2(currentPos.X + iLastX + 1, currentPos.Y);
                                break;
                            case 2:
                                tempPos = new Vector2(currentPos.X, currentPos.Y + iLastY + 1);
                                break;
                            case 3:
                                tempPos = new Vector2(currentPos.X - iX - 1, currentPos.Y);
                                break;
                        }
                    }
                    bOverlapses = false;
                    foreach (KeyValuePair<Vector2, Room> tRoom in tMap)
                    {
                        if (Intersects(tRoom.Key, new Vector2(tRoom.Value.Size.GetLength(0), tRoom.Value.Size.GetLength(1)), tempPos, new Vector2(iX, iY)))
                        {
                            bOverlapses = true;
                            break;
                        }
                    }
                    maxLoops--;
                    if (maxLoops <= 0)
                        return null;
                } while (bOverlapses);
                currentPos = tempPos;
                iLastX = iX;
                iLastY = iY;
                LastRoomOri = roomOri;
                Toolbox.Orientation gateOrientation = Toolbox.Orientation.North;
                //0 = North, 1 = East, 2 = South, 3 = West
                switch (roomOri)
                {
                    case 0:
                        gateOrientation = Toolbox.Orientation.South;
                        break;
                    case 1:
                        gateOrientation = Toolbox.Orientation.West;
                        break;
                    case 2:
                        gateOrientation = Toolbox.Orientation.North;
                        break;
                    case 3:
                        gateOrientation = Toolbox.Orientation.East;
                        break;
                }
                Room tNewRoom = new Room
                {
                    Size = tArr,
                    GateOrientation = gateOrientation
                };
                tMap.Add(currentPos, tNewRoom);
            }
            return tMap;
        }
        public static bool Intersects(Vector2 room1Pos, Vector2 room1Size, Vector2 room2Pos, Vector2 room2Size)
        {
            Rectangle tRect1;
            Rectangle tRect2;
            tRect1 = new Rectangle((int)room1Pos.X, (int)room1Pos.Y, (int)room1Size.X, (int)room1Size.Y);
            tRect2 = new Rectangle((int)room2Pos.X, (int)room2Pos.Y, (int)room2Size.X, (int)room2Size.Y);

            if (tRect1.Intersects(tRect2))
                return true;
            else
                return false;
        }
    }
}
