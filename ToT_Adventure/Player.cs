using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class Player : Entity
    {
        public Player()
        {
            Kind = Toolbox.EntityType.Player;
            if (TileIndex == null)
                TileIndex = Vector2.Zero;
        }
    }
}
