using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class UI
    { 
        public Vector2 Position { get; set; }
        public List<UIItem> uiItems;
        public bool ToDraw;
        public Toolbox.UIType uiType;
        public Toolbox.UIAlignment uIAlignment;

        public UI()
        {
            uiItems = new List<UIItem>();
            ToDraw = false;
            uiType = Toolbox.UIType.BasicInvis;
            uIAlignment = Toolbox.UIAlignment.Vertical;
        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tStrSize = new Vector2();
            foreach (var UII in uiItems)
            {
                switch(uIAlignment)
                {
                    case Toolbox.UIAlignment.Vertical:
                        tStrSize += new Vector2(0, ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).Y);
                        break;
                    case Toolbox.UIAlignment.Horizontal:
                        tStrSize += new Vector2(ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).X, 0);
                        break;
                }
                if (UII.ToDraw)
                {
                    UII.Draw(spriteBatch, Position + tStrSize);
                }
            }
        }
    }
}
