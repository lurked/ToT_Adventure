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
        public Rectangle Rectangle;
        public float BorderSize = 10f;
        public bool Active = false;

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

        public void RefreshUISize()
        {
            int fX = 0;
            int fY = 0;
            float currX = 0;
            float currY = 0;
            Vector2 vUIISize = new Vector2();

            switch (uIAlignment)
            {
                case Toolbox.UIAlignment.Vertical:
                    foreach (var UII in uiItems)
                    {
                        if (UII.ToDraw)
                        {
                            vUIISize = ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText);
                            UII.UpdateSpecs(Position, new Vector2(currX, currY), vUIISize);
                            if (fX < vUIISize.X)
                                fX = (int)vUIISize.X;
                            fY += (int)vUIISize.Y;
                            currY += vUIISize.Y;
                        }
                    }

                    break;
                case Toolbox.UIAlignment.Horizontal:
                    foreach (var UII in uiItems)
                    {
                        if (UII.ToDraw)
                        {
                            vUIISize = ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText);
                            UII.UpdateSpecs(Position, new Vector2(currX, currY), vUIISize);
                            fX += (int)vUIISize.X;
                            if (fY < (int)vUIISize.Y)
                                fY = (int)vUIISize.Y;
                            currX += vUIISize.Y;
                        }
                    }
                    break;
            }
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, fX, fY);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tStrSize = new Vector2();
            foreach (var UII in uiItems)
            {
                if (UII.ToDraw)
                {
                    UII.Draw(spriteBatch, Position + tStrSize);
                }
                switch (uIAlignment)
                {
                    case Toolbox.UIAlignment.Vertical:
                        tStrSize += new Vector2(0, ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).Y);
                        break;
                    case Toolbox.UIAlignment.Horizontal:
                        tStrSize += new Vector2(ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).X, 0);
                        break;
                }
            }
        }
    }
}
