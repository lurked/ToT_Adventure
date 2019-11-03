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
        public Dictionary<string, UI> UIs;

        public Screen()
        {

        }

        public virtual void LoadAssets()
        {
            UIs = new Dictionary<string, UI>();
        }
        public virtual void UnloadAssets()
        {
            UIs = null;
        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {
            foreach (KeyValuePair<string, UI> ui in UIs)
            {
                if (ui.Value.ToDraw)
                    ui.Value.Update(gameTime, input);
            }
        }

        public virtual void Save()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, UI> ui in UIs)
            {
                if (ui.Value.ToDraw)
                    ui.Value.Draw(spriteBatch);
            }
        }
    }
}
