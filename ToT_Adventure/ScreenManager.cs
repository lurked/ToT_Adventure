using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class ScreenManager
    {
        public Dictionary<Toolbox.ScreenType, Screen> Screens;

        public ScreenManager()
        {
            Screens = new Dictionary<Toolbox.ScreenType, Screen>();
            Screens.Add(Toolbox.ScreenType.MainMenu, new MainMenuScreen());
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Toolbox.ScreenType, Screen> screen in Screens)
            {
                screen.Value.Draw(spriteBatch);
            }
        }
    }
}
