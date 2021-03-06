﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ToT_Adventure
{
    public class Settings
    {
        public string ConfigName { get; set; }
        public Vector2 Resolution { get; set; }
        public Vector2 TileSize { get; set; }
        public Vector2 LevelTileSize { get; set; }
        public float BorderSize { get; set; }
        public float FPScap { get; set; }
        public string TexturePath { get; set; }
        public Dictionary<Toolbox.Controls, Keys> Controls { get; set; }
        

        public Settings(string configName = "default")
        {
            ConfigName = configName;
            Resolution = new Vector2(1600, 900);
            TileSize = new Vector2(128, 128);
            LevelTileSize = new Vector2(64, 64);
            BorderSize = 1f;
            FPScap = 144f;
            TexturePath = "Resources\\sprites";
            Controls = new Dictionary<Toolbox.Controls, Keys>
            {
                { Toolbox.Controls.MoveUp, Keys.W },
                { Toolbox.Controls.MoveDown, Keys.S },
                { Toolbox.Controls.MoveLeft, Keys.A },
                { Toolbox.Controls.MoveRight, Keys.D },
                { Toolbox.Controls.Adventure, Keys.LeftControl },
                { Toolbox.Controls.Exit, Keys.Escape }
            };
        }

        public void Save()
        {
            string settings;
            settings = JsonConvert.SerializeObject(this);

            FileManager.SaveToFile(settings, "data", ConfigName + Toolbox.GAMEFILES_EXT_SETTINGS);
        }
    }
}
