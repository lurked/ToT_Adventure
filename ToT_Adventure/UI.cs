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
        public bool Visible;
        public Toolbox.UIType uiType;
        public Toolbox.UIAlignment uIAlignment;
        public Rectangle Rectangle;
        public Vector2 Size;
        public float BorderSize = 10f;
        public bool Active = false;

        public UI()
        {
            uiItems = new List<UIItem>();
            Visible = false;
            uiType = Toolbox.UIType.BasicInvis;
            uIAlignment = Toolbox.UIAlignment.Vertical;
        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {

        }

        public void RefreshRectangle()
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }

        public void RefreshUISize(bool refreshRectangle = true)
        {
            int fX = 0;
            int fY = 0;
            float currX = 0;
            float currY = 0;
            Vector2 vUIISize;

            switch (uIAlignment)
            {
                case Toolbox.UIAlignment.Vertical:
                    foreach (var UII in uiItems)
                    {
                        if (UII.Visible)
                        {
                            vUIISize = UII.GetSize();
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
                        if (UII.Visible)
                        {
                            vUIISize = UII.GetSize();
                            UII.UpdateSpecs(Position, new Vector2(currX, currY), vUIISize);
                            fX += (int)vUIISize.X;
                            if (fY < (int)vUIISize.Y)
                                fY = (int)vUIISize.Y;
                            currX += vUIISize.Y;
                        }
                    }
                    break;
            }
            Size = new Vector2(fX, fY);
            if (refreshRectangle)
                RefreshRectangle();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tStrSize = new Vector2();
            foreach (var UII in uiItems)
            {
                if (UII.Visible)
                {
                    UII.Draw(spriteBatch, Position + tStrSize);
                }
                switch (uIAlignment)
                {
                    case Toolbox.UIAlignment.Vertical:
                        switch (UII.UIIType)
                        {
                            case Toolbox.UIItemType.ImageText:
                                tStrSize += new Vector2(
                                    0, 
                                    ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).Y + ToT.Textures[UII.ImageName].Height
                                    );
                                break;
                            default:
                                tStrSize += new Vector2(
                                    0, 
                                    ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).Y
                                    );
                                break;
                        }
                        break;
                    case Toolbox.UIAlignment.Horizontal:
                        switch (UII.UIIType)
                        {
                            case Toolbox.UIItemType.ImageText:
                                tStrSize += new Vector2(
                                    ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).X + ToT.Textures[UII.ImageName].Width, 
                                    0
                                    );
                                break;
                            default:
                                tStrSize += new Vector2(
                                    ToT.Fonts[UII.Font.ToString()].MeasureString(UII.DisplayText).X, 
                                    0
                                    );
                                break;
                        }
                        break;
                }
            }
        }
    }
}
