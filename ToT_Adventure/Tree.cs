using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class Tree : Thing
    {
        public Tree()
        {
            int iTree = Toolbox.StaticRandom.Instance.Next(1, 4);
            ImageName = "terrain" + ToT.Settings.LevelTileSize.X.ToString() + "\\tree_" + iTree.ToString("00");
        }
    }
}
