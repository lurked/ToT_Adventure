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
        public string ImageName { get; set; }
        public string DisplayText { get; set; }
        public Toolbox.TextAlignment TextAlign { get; set; } = Toolbox.TextAlignment.MiddleLeft;
        public Toolbox.UIItemType UIIType { get; set; }
        public bool Active { get; set; }
        public bool ToDraw { get; set; }
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

        public UIItem()
        {
            DisplayText = "Placeholder Text";
            TextAlign = Toolbox.TextAlignment.MiddleCenter;
            Font = Toolbox.Font.menuItem01;
            TextColor = Color.White;
            ToDraw = true;
            Action = new UIAction();
            ActiveColor = Color.Red;
        }
        public UIItem(Toolbox.UIItemType uiiType, string displayText, UIAction action, Toolbox.Font font = Toolbox.Font.menuItem01, Toolbox.TextAlignment textAlign = Toolbox.TextAlignment.MiddleCenter)
        {
            Font = font;
            DisplayText = displayText;
            TextAlign = Toolbox.TextAlignment.MiddleCenter;
            TextColor = Color.White;
            ToDraw = true;
            Action = action;
            ActiveColor = Color.Red;
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
            spriteBatch.DrawString(ToT.Fonts[Font.ToString()], DisplayText, uiiPosition + ToT.PlayerCamera.Position, (Active && Action.Action != Toolbox.UIAction.Nothing) ? ActiveColor : TextColor);
            //spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug01.ToString()], uiiPosition.ToString(), uiiPosition, Active ? ActiveColor : TextColor);
        }
    }
}
