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
            Screens[Toolbox.ScreenType.MainMenu].LoadAssets();
        }

        public virtual void UnloadContent()
        {
            Screens[Toolbox.ScreenType.MainMenu].UnloadAssets();
        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {
            switch (ToT.State)
            {
                case Toolbox.GameState.MainMenu:
                    Screens[Toolbox.ScreenType.MainMenu].Update(gameTime, input);
                    break;
                case Toolbox.GameState.GameMap:
                    break;
                case Toolbox.GameState.GameLevel:
                    break;
                case Toolbox.GameState.GameOver:
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //foreach (KeyValuePair<Toolbox.ScreenType, Screen> screen in Screens)
            //{
            //    screen.Value.Draw(spriteBatch);
            //}
            switch (ToT.State)
            {
                case Toolbox.GameState.MainMenu:
                    Screens[Toolbox.ScreenType.MainMenu].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.GameMap:
                    break;
                case Toolbox.GameState.GameLevel:
                    break;
                case Toolbox.GameState.GameOver:
                    break;
            }
        }
    }
}
