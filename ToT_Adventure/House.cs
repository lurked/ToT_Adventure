using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class House : Building
    {
        public House()
        {
            ImageName = "home_01";
            Anime = new Animation(Toolbox.AnimationType.None, ImageName);
            Anime.Init();
            Kind = Toolbox.EntityType.Building;
        }
    }
}
