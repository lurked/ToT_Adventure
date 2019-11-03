using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ToT_Adventure
{
    public class LoadGameScreen : Screen
    {
        public override void LoadAssets()
        {
            base.LoadAssets();
            GenerateUI_GameMap();
            GenerateUI_LoadGames();
            GenerateUI_BackMenu();
        }

        #region MenuUIs
        private void GenerateUI_GameMap()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Load Game", new UIAction(), Toolbox.Font.logo01, Toolbox.TextAlignment.TopLeft);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUI.Position = new Vector2((ToT.Settings.Resolution.X - ToT.Fonts[Toolbox.Font.logo01.ToString()].MeasureString(mMenuUII.DisplayText).X) / 2, ToT.Settings.Resolution.Y / 20);
            mMenuUI.RefreshUISize();

            UIs.Add("loadGameLogo", mMenuUI);
            UIs["loadGameLogo"].ToDraw = true;
        }
        private void GenerateUI_LoadGames()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII = new UIItem();
            string[] saves = FileManager.GetSaves();
            float fLongest = 0f;
            foreach (string sFile in saves)
            {
                mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, sFile, new UIAction(Toolbox.UIAction.GameMap_LoadGame, sFile), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
                mMenuUI.uiItems.Add(mMenuUII);
                fLongest = ToT.Fonts[Toolbox.Font.menuItem02.ToString()].MeasureString(mMenuUII.DisplayText).X > fLongest ? ToT.Fonts[Toolbox.Font.menuItem02.ToString()].MeasureString(mMenuUII.DisplayText).X : fLongest;
            }
            mMenuUI.Position = new Vector2((ToT.Settings.Resolution.X - fLongest) / 2, ToT.Settings.Resolution.Y / 4);
            mMenuUI.RefreshUISize();

            UIs.Add("loadGames", mMenuUI);
            UIs["loadGames"].ToDraw = true;
        }
        private void GenerateUI_BackMenu()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "< Back", new UIAction(Toolbox.UIAction.MainMenu), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);
            Vector2 uiiSize = ToT.Fonts[Toolbox.Font.menuItem02.ToString()].MeasureString(mMenuUII.DisplayText);
            mMenuUI.Position = new Vector2(10, ToT.Settings.Resolution.Y - (uiiSize.Y * mMenuUI.uiItems.Count()) - 5);
            mMenuUI.RefreshUISize();

            UIs.Add("BackMenu", mMenuUI);
            UIs["BackMenu"].ToDraw = true;
        }
        #endregion
    }
}
