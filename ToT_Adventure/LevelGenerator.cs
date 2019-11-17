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
        public static Level Generate(string lvlType, Vector2 size)
        {
            Level lvl = new Level();
            Tile tTile;
            string imgPrefix = "tile_plains_{spriteposY}{spriteposX}_{tilesize}";
            string imgName;
            string imgPrename;
            switch(lvlType)
            {
                case "basic":
                    imgPrefix = imgPrefix.Replace("{tilesize}", ((int)ToT.Settings.LevelTileSize.X).ToString());
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
                            else if(j == size.Y - 1)
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
                    break;
                default:

                    break;
            }
            return lvl;
        }
    }
}
