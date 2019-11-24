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
            if (!Stats.ContainsKey(Toolbox.Stat.Speed))
                Stats.Add(Toolbox.Stat.Speed, 2f);
            else
                Stats[Toolbox.Stat.Speed] = 2f;

            Anime = new Animation()
            {
                ImageName = "character_32x48",
                Frames = new Vector2(3, 4),
                Type = Toolbox.AnimationType.Spritesheet,
                Active = true
            };

            Orientation = Toolbox.Orientation.South;

            Anime.Init();
        }

        internal void MoveTo(Toolbox.Orientation orientation, Vector2 vDestPos)
        {
            DestTileIndex = vDestPos;
            State = Toolbox.EntityState.Moving;
            Orientation = orientation;
        }

        internal void UpdateMove()
        {
            if (State == Toolbox.EntityState.Moving)
            {
                Anime.Active = true;
                if (DistanceTraveled < 1f)
                {
                    DistanceTraveled += (Stats[Toolbox.Stat.Speed] * 1.5f * (1/ToT.Settings.FPScap));
                }
                if (DistanceTraveled >= 1f)
                {
                    TileIndex = DestTileIndex;
                    DistanceTraveled = 0f;
                    State = Toolbox.EntityState.Idle;
                }
                ToT.PlayerCamera.SetFocalPoint(TileIndex * ToT.Settings.TileSize + ToT.Settings.TileSize / 2 + GetMoveVector());
            }
            else
            {
                Anime.Active = false;
            }
        }
        internal void UpdateLevelMove()
        {
            if (State == Toolbox.EntityState.Moving)
            {
                Anime.Active = true;
                ToT.PlayerCamera.SetFocalPoint(LevelPosition);
            }
            else
            {
                Anime.Active = false;
            }
        }

        internal Vector2 GetMoveVector()
        {
            switch(Orientation)
            {
                case Toolbox.Orientation.East:
                    return new Vector2(ToT.Settings.TileSize.X * DistanceTraveled, 0f);
                case Toolbox.Orientation.West:
                    return new Vector2(-(ToT.Settings.TileSize.X * DistanceTraveled), 0f);
                case Toolbox.Orientation.South:
                    return new Vector2(0f, ToT.Settings.TileSize.Y * DistanceTraveled);
                case Toolbox.Orientation.North:
                    return new Vector2(0f, -(ToT.Settings.TileSize.Y * DistanceTraveled));
                default:
                    return Vector2.Zero;
            }
        }

        internal void LevelMoveTo(Toolbox.Orientation orientation, Vector2 vDestPos)
        {
            State = Toolbox.EntityState.Moving;
            LevelPosition = vDestPos;
            Orientation = orientation;
        }
    }
}
