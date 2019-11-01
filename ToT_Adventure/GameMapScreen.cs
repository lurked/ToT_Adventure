﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class GameMapScreen : Screen 
    {
        Starfield starfield;
        Vector2 MousePos;
        GameMap GameMap;

        #region MenuUIs
        private void GenerateUI_GameMap()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "New Game", new UIAction(), Toolbox.Font.logo01, Toolbox.TextAlignment.TopLeft);
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUI.Position = new Vector2((ToT.Settings.Resolution.X - ToT.Fonts[Toolbox.Font.logo01.ToString()].MeasureString(mMenuUII.DisplayText).X) / 2, ToT.Settings.Resolution.Y / 20);
            mMenuUI.RefreshUISize();

            UIs.Add("newGameLogo", mMenuUI);
            UIs["newGameLogo"].ToDraw = true;
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

        public override void LoadAssets()                                                                                                                                                                                       
        {
            base.LoadAssets();
            GenerateUI_GameMap();
            GenerateUI_BackMenu();
            starfield = new Starfield((int)(ToT.Settings.Resolution.X * 1.5f), (int)(ToT.Settings.Resolution.Y * 1.5f), 200, new Vector2(10f, 10f), ToT.Textures["star03"], new Rectangle(0, 0, 7, 7));
            GameMap = new GameMap();
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
            Vector2 vCamOffset;
            switch(ToT.State)
            {
                case Toolbox.GameState.GameMap:
                    vCamOffset = ToT.PlayerCamera.Position;
                    break;
                case Toolbox.GameState.GameLevel:
                    vCamOffset = ToT.PlayerCamera.Position;
                    break;
                default:
                    vCamOffset = new Vector2();
                    break;
            }
            //Draw each tile to its corresponding position according to the tile size and border size.
            foreach (KeyValuePair<Vector2, Tile> tile in GameMap.Map)
            {
                spriteBatch.Draw(
                    ToT.Textures[tile.Value.ImageName], 
                    tile.Key * ToT.Settings.TileSize + (new Vector2(ToT.Settings.BorderSize, ToT.Settings.BorderSize) * tile.Key), 
                    null, 
                    Color.White
                );
            }
        }
    }
}
