using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class Thing : Entity
    {
        public Vector2 Size { get; set; }

        public Thing()
        {
            if (Size == null)
                Size = Vector2.One;
        }
    }
}
