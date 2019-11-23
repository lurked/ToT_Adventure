using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class Tree : Thing
    {
        public Toolbox.ResourceType ResourceType { get; set; }
        public int ResourceQty { get; set; }
        public Tree()
        {
            int iTree = Toolbox.StaticRandom.Instance.Next(1, 4);
            ImageName = "terrain" + ToT.Settings.LevelTileSize.X.ToString() + "\\tree_" + iTree.ToString("00");
            Anime = new Animation(Toolbox.AnimationType.None, ImageName);
            Anime.Init();
            Kind = Toolbox.EntityType.Resource;
            ResourceType = Toolbox.ResourceType.Wood;
            ResourceQty = Toolbox.StaticRandom.Instance.Next(1, 5);
        }
    }
}
