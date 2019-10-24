using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class Screen
    {
        public bool IsActive = true;
        public bool IsPopup = false;
        public Color BackgroundColor = Color.Black;

        public Screen()
        {

        }

        public virtual void LoadAssets()
        {

        }
        public virtual void UnloadAssets()
        {

        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
