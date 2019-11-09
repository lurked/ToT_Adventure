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
            Anime = new Animation()
            {
                ImageName = "character_32x48",
                Frames = new Vector2(3, 4),
                Type = Toolbox.AnimationType.Spritesheet,
                Active = true
            };
            Anime.Init();
        }
    }
}
