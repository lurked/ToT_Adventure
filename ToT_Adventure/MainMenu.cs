using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class MainMenuScreen : Screen
    {
        
        public override void LoadAssets()
        {
            base.LoadAssets();
        }
        public override void UnloadAssets()
        {
            base.UnloadAssets();
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            base.Update(gameTime, input);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.logo01.ToString()], "Tales of Tiles", new Vector2(10,10), Color.CornflowerBlue);
        }
    }
}
