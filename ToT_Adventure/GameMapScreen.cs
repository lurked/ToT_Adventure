using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ToT_Adventure
{
    public class GameMapScreen : Screen 
    {
        public GameMap GameMap { get; set; }

        readonly string SaveFile;


        public GameMapScreen(string saveFile = "")
        {
            SaveFile = saveFile;
        }
        #region MenuUIs
        //private void GenerateUI_GameMap()
        //{
        //    UI mMenuUI = new UI();
        //    UIItem mMenuUII = new UIItem("newgame", Toolbox.UIItemType.TextFix, "New Game", new UIAction(), Toolbox.Font.logo01, Toolbox.TextAlignment.TopLeft);
        //    mMenuUI.uiItems.Add(mMenuUII);
        //    mMenuUI.Position = new Vector2((ToT.Settings.Resolution.X - ToT.Fonts[Toolbox.Font.logo01.ToString()].MeasureString(mMenuUII.DisplayText).X) / 2, ToT.Settings.Resolution.Y / 20);
        //    mMenuUI.RefreshUISize();

        //    UIs.Add("newGameLogo", mMenuUI);
        //    UIs["newGameLogo"].Visible = true;
        //}
        private void GenerateUI_SaveMenu()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem("game", Toolbox.UIItemType.ImageFix, "Game", new UIAction(Toolbox.UIAction.Toggle_UII, "savegame,leavegame,exit"), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter, "gear_24");
            mMenuUI.uiItems.Add(mMenuUII);

            mMenuUII = new UIItem("savegame", Toolbox.UIItemType.TextFix, "Save Game", new UIAction(Toolbox.UIAction.GameMap_SaveGame), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter)
            {
                Visible = false
            };
            mMenuUI.uiItems.Add(mMenuUII); 

            mMenuUII = new UIItem("leavegame", Toolbox.UIItemType.TextFix, "Leave Game", new UIAction(Toolbox.UIAction.GameMap_MainMenu), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter)
            {
                Visible = false
            };
            mMenuUI.uiItems.Add(mMenuUII); 

            mMenuUII = new UIItem("exit", Toolbox.UIItemType.TextFix, "Exit to Desktop", new UIAction(Toolbox.UIAction.GameMap_Exit), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter)
            {
                Visible = false
            };
            mMenuUI.uiItems.Add(mMenuUII);

            //Vector2 uiiSize = ToT.Fonts[Toolbox.Font.menuItem02.ToString()].MeasureString(mMenuUII.DisplayText);
            mMenuUI.RefreshUISize(false);
            mMenuUI.Position = new Vector2(ToT.Settings.Resolution.X - mMenuUI.Size.X, 0);
            mMenuUI.RefreshUISize();

            UIs.Add("BackMenu", mMenuUI);
            UIs["BackMenu"].Visible = true;
        }
        #endregion

        public override void LoadAssets()                                                                                                                                                                                       
        {
            base.LoadAssets();
            //GenerateUI_GameMap();
            GenerateUI_SaveMenu();
            if (SaveFile != "")
            {
                GameMap = LoadGame(SaveFile);
            }
            else
            {
                GameMap = new GameMap();
            }
        }

        public override void UnloadAssets()
        {
            base.UnloadAssets();
            UIs = null;
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            base.Update(gameTime, input);
            UpdatePlayer(input);
        }

        private void UpdatePlayer(InputManager input)
        {
            Vector2 vCurrentPos = GameMap.player.TileIndex;
            if (input.KeyPressed(Keys.Left) || input.KeyPressed(Keys.A))
            {
                MovePlayer(new Vector2(vCurrentPos.X - 1, vCurrentPos.Y));
            }
            else if (input.KeyPressed(Keys.Right) || input.KeyPressed(Keys.D))
            {
                MovePlayer(new Vector2(vCurrentPos.X + 1, vCurrentPos.Y));
            }
            else if (input.KeyPressed(Keys.Up) || input.KeyPressed(Keys.W))
            {
                MovePlayer(new Vector2(vCurrentPos.X, vCurrentPos.Y - 1));
            }
            else if (input.KeyPressed(Keys.Down) || input.KeyPressed(Keys.S))
            {
                MovePlayer(new Vector2(vCurrentPos.X, vCurrentPos.Y + 1));
            }
            GameMap.player.Anime.Update();
        }

        private void MovePlayer(Vector2 vCurrentPos)
        {
            if (GameMap.Map.ContainsKey(vCurrentPos))
            {
                GameMap.player.TileIndex = vCurrentPos;
                ToT.PlayerCamera.SetFocalPoint(vCurrentPos * ToT.Settings.TileSize + ToT.Settings.TileSize / 2);
            }
        }

        public override void Save()
        {
            string gameSave = JsonConvert.SerializeObject(GameMap);
            FileManager.SaveToFile(
                gameSave, 
                "saves", 
                "save" + DateTime.Now.Year.ToString() + 
                         DateTime.Now.Month.ToString() + 
                         DateTime.Now.Day.ToString() + 
                         DateTime.Now.Hour.ToString() + 
                         DateTime.Now.Minute.ToString() + 
                         DateTime.Now.Second.ToString() + ".tots");
        }

        public GameMap LoadGame(string saveName)
        {
            GameMap gmLoad;

            gmLoad = JsonConvert.DeserializeObject<GameMap>(FileManager.ReadFile(saveName));

            return gmLoad;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Vector2 vCamOffset;
            //switch(ToT.State)
            //{
            //    case Toolbox.GameState.GameMap:
            //        vCamOffset = ToT.PlayerCamera.Position;
            //        break;
            //    case Toolbox.GameState.GameLevel:
            //        vCamOffset = ToT.PlayerCamera.Position;
            //        break;
            //    default:
            //        vCamOffset = new Vector2();
            //        break;
            //}
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

            //Draw the player(s)
            Rectangle pSourceRect = GameMap.player.Anime.SourceRect;
            Vector2 origin = new Vector2(pSourceRect.Width / 2, pSourceRect.Height / 2);
            spriteBatch.Draw(
                ToT.Textures[GameMap.player.Anime.ImageName],
                (
                    GameMap.player.TileIndex * ToT.Settings.TileSize + 
                    (new Vector2(ToT.Settings.BorderSize, ToT.Settings.BorderSize) * GameMap.player.TileIndex) + 
                    (ToT.Settings.TileSize / 2)
                ),
                pSourceRect,
                Color.White,
                0f,
                origin,
                1f,
                SpriteEffects.None,
                0.0f
            );
        }
    }
}
