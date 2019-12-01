using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class UIItem
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string DisplayText { get; set; }
        public Toolbox.TextAlignment TextAlign { get; set; } = Toolbox.TextAlignment.MiddleLeft;
        public Toolbox.UIItemType UIIType { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public Toolbox.Font Font { get; set; }
        public Color TextColor { get; set; }
        public UIAction Action { get; set; }
        public Vector2 PositionInUI { get; set; }
        public Vector2 UIPosition { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Rectangle { get; set; }
        public Color ActiveColor { get; set; }
        public Rectangle GetRectangle()
        {
            if (Rectangle != null)
                return Rectangle;
            else
                return SetRectangle(UIPosition, PositionInUI, Size);
        }

        public Vector2 GetSize()
        {
            Vector2 vUIISize;
            if (UIIType == Toolbox.UIItemType.ImageFix ||
                UIIType == Toolbox.UIItemType.ImageFloating)
            {
                vUIISize = new Vector2(ToT.Textures[ImageName].Width, ToT.Textures[ImageName].Height);
            }
            else if (UIIType == Toolbox.UIItemType.ImageText)
            {
                vUIISize = new Vector2(
                    ToT.Textures[ImageName].Width + ToT.Fonts[Font.ToString()].MeasureString(DisplayText).X, 
                    ToT.Textures[ImageName].Height > (int)ToT.Fonts[Font.ToString()].MeasureString(DisplayText).Y ? ToT.Textures[ImageName].Height : (int)ToT.Fonts[Font.ToString()].MeasureString(DisplayText).Y
                    );
            }
            else
            {
                vUIISize = ToT.Fonts[Font.ToString()].MeasureString(DisplayText);
            }

            return vUIISize;
        }

        public UIItem()
        {
            DisplayText = "Placeholder Text";
            TextAlign = Toolbox.TextAlignment.MiddleCenter;
            Font = Toolbox.Font.menuItem01;
            TextColor = Color.White;
            Visible = true;
            Action = new UIAction();
            ActiveColor = Color.Red;
            UIIType = Toolbox.UIItemType.TextFix;
            ImageName = "colorwheel32.png";
            Name = "Unnamed UIItem";
        }
        public UIItem(string name, Toolbox.UIItemType uiiType, string displayText, UIAction action, Toolbox.Font font = Toolbox.Font.menuItem01, Toolbox.TextAlignment textAlign = Toolbox.TextAlignment.MiddleCenter, string imageName = "")
        {
            Font = font;
            DisplayText = displayText;
            TextAlign = textAlign;
            TextColor = Color.White;
            Visible = true;
            Action = action;
            ActiveColor = Color.Red;
            UIIType = uiiType;
            ImageName = imageName;
            Name = name;
        }

        public void UpdateSpecs(Vector2 uiPosition, Vector2 position, Vector2 size)
        {
            PositionInUI = position;
            Size = size;
            UIPosition = uiPosition;
            SetRectangle(uiPosition, position, size);
        }

        private Rectangle SetRectangle(Vector2 uiPosition, Vector2 position, Vector2 size)
        {
            Rectangle = new Rectangle(new Point((int)uiPosition.X + (int)position.X, (int)uiPosition.Y + (int)position.Y), new Point((int)size.X, (int)size.Y));
            return Rectangle;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 uiiPosition)
        {
            switch(UIIType)
            {
                case Toolbox.UIItemType.ImageFix:
                    spriteBatch.Draw(
                        ToT.Textures[ImageName],
                        uiiPosition + ToT.PlayerCamera.Position,
                        null,
                        Color.White
                        );
                    break;
                case Toolbox.UIItemType.ImageText:
                    spriteBatch.Draw(
                        ToT.Textures[ImageName],
                        uiiPosition + ToT.PlayerCamera.Position,
                        null,
                        Color.White
                        );
                    spriteBatch.DrawString(
                        ToT.Fonts[Font.ToString()], 
                        DisplayText, 
                        uiiPosition + ToT.PlayerCamera.Position + new Vector2(ToT.Textures[ImageName].Width, 0), 
                        (Active && Action.Action != Toolbox.UIAction.Nothing) ? ActiveColor : TextColor
                        );
                    break;
                case Toolbox.UIItemType.TextFix:
                    spriteBatch.DrawString(ToT.Fonts[Font.ToString()], DisplayText, uiiPosition + ToT.PlayerCamera.Position, (Active && Action.Action != Toolbox.UIAction.Nothing) ? ActiveColor : TextColor);
                    break;
            }
            
            //spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug01.ToString()], uiiPosition.ToString(), uiiPosition, Active ? ActiveColor : TextColor);
        }
    }
}
