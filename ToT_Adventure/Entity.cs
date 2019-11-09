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
        public Toolbox.EntityType Kind { get; set; }
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public string ImageName { get; set; }
        public Vector2 TileIndex { get; set; }
        public Vector2 Position { get; set; }
        public bool Visible { get; set; } = true;
        protected Dictionary<Toolbox.Stat, float> Stats;
        protected Dictionary<Toolbox.Stat, float> Bonus;
        public Animation Anime { get; set; }

        public Entity()
        {
            Kind = Toolbox.EntityType.Decor;
            Position = Vector2.Zero;
            TileIndex = Vector2.Zero;
            Stats = new Dictionary<Toolbox.Stat, float>
            {
                { Toolbox.Stat.HP, 1f }
            };
            Tooltip = "Basic entity, so basic all it wants to do is drink pumpkin spice lattes and play candy crush.";
            ImageName = "colorwheel32";
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

        public virtual void Update(GameTime gameTime)
        {
            //Anime.Update();
        }
    }
}
