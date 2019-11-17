﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{

    public class Tile
    {
        public string ImageName { get; set; }
        public Toolbox.TileType TileType { get; set; }

        public Tile()
        {
            ImageName = "main_01_" + ToT.Settings.TileSize.X.ToString();
            TileType = Toolbox.TileType.Home;
        }
        public Tile(string imageName, Toolbox.TileType tileType = Toolbox.TileType.Plains)
        {
            TileType = tileType;

            if (TileType == Toolbox.TileType.Castle ||
                TileType == Toolbox.TileType.Dirt ||
                TileType == Toolbox.TileType.Dungeon ||
                TileType == Toolbox.TileType.Forest ||
                TileType == Toolbox.TileType.Home ||
                TileType == Toolbox.TileType.Plains
                )
            {
                if (imageName == "")
                    ImageName = "cotton_green_" + ToT.Settings.TileSize.X.ToString();
                else
                    ImageName = imageName;
            }
            else 
            {
                ImageName = imageName;
            }
        }
    }
}
