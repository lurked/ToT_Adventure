using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class LevelGenerator
    {
        public static Level Generate(string lvlType, Vector2 size, Toolbox.TileType tileType)
        {
            Level lvl;

            switch(lvlType)
            {
                case "basic":
                    lvl = GenerateBasicLevel(size, tileType);
                    break;
                case "home":
                    lvl = GenerateHomeLevel(size, tileType);
                    break;
                default:
                    lvl = GenerateBasicLevel(size, tileType);
                    break;
            }
            return lvl;
        }

        private static Level GenerateHomeLevel(Vector2 size, Toolbox.TileType tileType)
        {
            Level lvl = new Level();
            Tile tTile;
            string imgPrefix = "terrain{tilesize}\\tile_{terrain}_{spriteposY}{spriteposX}";
            string imgName;
            string imgPrename;
            imgPrefix = imgPrefix.Replace("{tilesize}", ToT.Settings.LevelTileSize.X.ToString());
            imgPrefix = imgPrefix.Replace("{terrain}", tileType.ToString().ToLower().Replace("level_", ""));
            lvl.Size = size;
            for (int i = 0; i < size.X; i++)
            {
                if (i == 0)
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "left");
                }
                else if (i == size.X - 1)
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "right");
                }
                else
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "middle");
                }

                for (int j = 0; j < size.Y; j++)
                {
                    imgName = imgPrename;
                    if (j == 0)
                    {
                        imgName = imgName.Replace("{spriteposY}", "top");
                    }
                    else if (j == size.Y - 1)
                    {
                        imgName = imgName.Replace("{spriteposY}", "bottom");
                    }
                    else
                    {
                        imgName = imgName.Replace("{spriteposY}", "middle");
                    }
                    tTile = new Tile(imgName, Toolbox.TileType.Level_Plains);
                    lvl.Tileset.Add(new Vector2(i, j), tTile);
                }
            }

            switch (tileType)
            {
                case Toolbox.TileType.Plains:
                    lvl.Things.Add(0, new Dictionary<Vector2, Thing>());
                    int nbTrees = Toolbox.StaticRandom.Instance.Next(1, 21);
                    Vector2 vTreePos;
                    for (int i = 0; i < nbTrees; i++)
                    {
                        do
                        {
                            vTreePos = new Vector2(Toolbox.StaticRandom.Instance.Next(1, (int)size.X - 1), Toolbox.StaticRandom.Instance.Next(1, (int)size.Y - 1));
                        } while (lvl.Things[0].ContainsKey(vTreePos));
                        int resKind = Toolbox.StaticRandom.Instance.Next(1, 4);
                        switch (resKind)
                        {
                            case 1:
                                lvl.Things[0].Add(vTreePos, new Tree());
                                break;
                            case 2:
                                lvl.Things[0].Add(vTreePos, new Rock());
                                break;
                            case 3:
                                lvl.Things[0].Add(vTreePos, new Crystal());
                                break;
                        }
                    }
                    break;
            }
            lvl.StartPosition = size / 2;
            return lvl;

        }

        private static Level GenerateBasicLevel(Vector2 size, Toolbox.TileType tileType)
        {
            Level lvl = new Level();
            Tile tTile;
            string imgPrefix = "terrain{tilesize}\\tile_{terrain}_{spriteposY}{spriteposX}";
            string imgName;
            string imgPrename;
            imgPrefix = imgPrefix.Replace("{tilesize}", ToT.Settings.LevelTileSize.X.ToString());
            imgPrefix = imgPrefix.Replace("{terrain}", tileType.ToString().ToLower().Replace("level_", ""));
            lvl.Size = size;
            for (int i = 0; i < size.X; i++)
            {
                if (i == 0)
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "left");
                }
                else if (i == size.X - 1)
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "right");
                }
                else
                {
                    imgPrename = imgPrefix.Replace("{spriteposX}", "middle");
                }

                for (int j = 0; j < size.Y; j++)
                {
                    imgName = imgPrename;
                    if (j == 0)
                    {
                        imgName = imgName.Replace("{spriteposY}", "top");
                    }
                    else if (j == size.Y - 1)
                    {
                        imgName = imgName.Replace("{spriteposY}", "bottom");
                    }
                    else
                    {
                        imgName = imgName.Replace("{spriteposY}", "middle");
                    }
                    tTile = new Tile(imgName, Toolbox.TileType.Level_Plains);
                    lvl.Tileset.Add(new Vector2(i, j), tTile);
                }
            }

            switch(tileType)
            {
                case Toolbox.TileType.Plains:
                    lvl.Things.Add(0, new Dictionary<Vector2, Thing>());
                    int nbTrees = Toolbox.StaticRandom.Instance.Next(1, 21);
                    Vector2 vTreePos;
                    for (int i = 0; i < nbTrees; i++)
                    {
                        do
                        {
                            vTreePos = new Vector2(Toolbox.StaticRandom.Instance.Next(1, (int)size.X - 1), Toolbox.StaticRandom.Instance.Next(1, (int)size.Y - 1));
                        } while (lvl.Things[0].ContainsKey(vTreePos));
                        int resKind = Toolbox.StaticRandom.Instance.Next(1, 4);
                        switch(resKind)
                        {
                            case 1:
                                lvl.Things[0].Add(vTreePos, new Tree());
                                break;
                            case 2:
                                lvl.Things[0].Add(vTreePos, new Rock());
                                break;
                            case 3:
                                lvl.Things[0].Add(vTreePos, new Crystal());
                                break;
                        }
                    }
                    break;
            }
            return lvl;
        }
    }
}
