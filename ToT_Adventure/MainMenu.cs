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
        Starfield starfield;
        Vector2 MousePos;

        #region MenuUIs
        private void GenerateUI_MMenuLogo()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Tales of Tiles", new UIAction(), Toolbox.Font.logo01, Toolbox.TextAlignment.TopLeft);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUI.Position = new Vector2((ToT.Settings.Resolution.X - ToT.Fonts[Toolbox.Font.logo01.ToString()].MeasureString(mMenuUII.DisplayText).X) / 2, ToT.Settings.Resolution.Y / 20);
            mMenuUI.RefreshUISize();

            UIs.Add("mMenuLogo", mMenuUI);
            UIs["mMenuLogo"].ToDraw = true;
        }
        private void GenerateUI_MMenu()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "New Game", new UIAction(Toolbox.UIAction.MainMenu_NewGame),Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Load Game", new UIAction(Toolbox.UIAction.MainMenu_LoadGame), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Settings", new UIAction(Toolbox.UIAction.MainMenu_Settings), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Exit", new UIAction(Toolbox.UIAction.MainMenu_Exit), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);
            Vector2 uiiSize = ToT.Fonts[Toolbox.Font.menuItem02.ToString()].MeasureString(mMenuUII.DisplayText);
            mMenuUI.Position = new Vector2(10, ToT.Settings.Resolution.Y - (uiiSize.Y * mMenuUI.uiItems.Count()) - 5);
            mMenuUI.RefreshUISize();

            UIs.Add("mMenu", mMenuUI);
            UIs["mMenu"].ToDraw = true;
        }
        #endregion

        public override void LoadAssets()                                                                                                                                                                                       
        {
            base.LoadAssets();
            GenerateUI_MMenuLogo();
            GenerateUI_MMenu();
            starfield = new Starfield((int)(ToT.Settings.Resolution.X * 1.5f), (int)(ToT.Settings.Resolution.Y * 1.5f), 200, new Vector2(10f, 10f), ToT.Textures["star03"], new Rectangle(0, 0, 7, 7));
        }

        public override void UnloadAssets()
        {
            base.UnloadAssets();
            UIs = null;
            starfield = null;
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            base.Update(gameTime, input);
            starfield.Update(gameTime);
            MousePos = input.MousePosition();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            starfield.Draw(spriteBatch);
            base.Draw(spriteBatch);
            //spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug01.ToString()], MousePos.ToString(), MousePos, Color.CornflowerBlue);
        }
    }
}
