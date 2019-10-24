using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Xna.Framework;


namespace ToT_Adventure
{
    public class Settings
    {
        public string ConfigName { get; set; }
        public Vector2 Resolution { get; set; }
        public Vector2 TileSize { get; set; }

        public Settings(string configName = "default")
        {
            ConfigName = configName;
            Resolution = new Vector2(1440, 900);
            TileSize = new Vector2(72, 72);
        }

        public void Save()
        {
            string settings;
            settings = JsonConvert.SerializeObject(this);

            FileManager.SaveToFile(settings, "data", ConfigName + Toolbox.GAMEFILES_EXT_SETTINGS);
        }
    }
}
