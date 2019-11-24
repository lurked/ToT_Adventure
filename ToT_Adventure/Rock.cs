using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class Rock : Thing
    {
        public Toolbox.ResourceType ResourceType { get; set; }
        public int ResourceQty { get; set; }
        public Rock()
        {
            ImageName = "terrain" + ToT.Settings.LevelTileSize.X.ToString() + "\\rock_01";
            Anime = new Animation(Toolbox.AnimationType.None, ImageName);
            Anime.Init();
            Kind = Toolbox.EntityType.Resource;
            ResourceType = Toolbox.ResourceType.Rock;
            ResourceQty = Toolbox.StaticRandom.Instance.Next(1, 5);
        }
    }
}
