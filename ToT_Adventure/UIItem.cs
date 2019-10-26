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

        public UIItem()
        {
            DisplayText = "Placeholder Text";
            TextAlign = Toolbox.TextAlignment.MiddleCenter;
            Font = Toolbox.Font.menuItem01;
            TextColor = Color.White;
            ToDraw = true;
        }
        public UIItem(Toolbox.UIItemType uiiType, string displayText, Toolbox.Font font = Toolbox.Font.menuItem01, Toolbox.TextAlignment textAlign = Toolbox.TextAlignment.MiddleCenter)
        {
            Font = font;
            DisplayText = displayText;
            TextAlign = Toolbox.TextAlignment.MiddleCenter;
            TextColor = Color.White;
            ToDraw = true;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 uiiPosition)
        {
            spriteBatch.DrawString(ToT.Fonts[Font.ToString()], DisplayText, uiiPosition, TextColor);
        }
    }
}
