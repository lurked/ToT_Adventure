using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ToT_Adventure
{
    public class Entity
    {
        public Animation Anime { get; set; }
        public Toolbox.EntityType Kind { get; set; }
        public Toolbox.EntityState State { get; set; }
        public Dictionary<Toolbox.ResourceType, int> Resources { get; set; }
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public string ImageName { get; set; }
        public Vector2 TileIndex { get; set; }
        public Vector2 DestTileIndex { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LevelPosition { get; set; }
        public float DistanceTraveled { get; set; }
        public Toolbox.Orientation Orientation { get; set; }
        public bool Visible { get; set; } = true;
        public bool IsHover { get; set; }

        protected Dictionary<Toolbox.Stat, float> Stats;
        protected Dictionary<Toolbox.Stat, float> Bonus;

        public Entity()
        {
            State = Toolbox.EntityState.Idle;
            if (Stats == null)
                Stats = new Dictionary<Toolbox.Stat, float>
                {
                    { Toolbox.Stat.HP, 1f },
                    { Toolbox.Stat.Speed, 1f }
                };
            if (Resources == null)
                Resources = new Dictionary<Toolbox.ResourceType, int>
                {
                    { Toolbox.ResourceType.Wood, 0 },
                    { Toolbox.ResourceType.Rock, 0 },
                    { Toolbox.ResourceType.Crystal, 0 },
                    { Toolbox.ResourceType.Gold, 0 }
                };
            Tooltip = "Basic entity, so basic all it wants to do is drink pumpkin spice lattes and play candy crush.";
            ImageName = "colorwheel32";
            DestTileIndex = Vector2.Zero;
            LevelPosition = Vector2.Zero;
            if (Bonus == null)
                Bonus = new Dictionary<Toolbox.Stat, float>();
        }

        public float GetStat(Toolbox.Stat stat, bool withBonuses = true)
        {
            float tStat = 0;

            if (Stats.ContainsKey(stat))
                tStat = Stats[stat];

            if (withBonuses)
                if (Bonus.ContainsKey(stat))
                    tStat += Bonus[stat];

            return tStat;
        }

        public int GetResource(Toolbox.ResourceType res)
        {
            int tRes = 0;

            if (Resources.ContainsKey(res))
                tRes = Resources[res];

            return tRes;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Anime.Update();
        }
    }
}
