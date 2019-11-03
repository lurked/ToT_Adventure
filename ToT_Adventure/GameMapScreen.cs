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
        Vector2 MousePos;
        public GameMap GameMap { get; set; }
        string SaveFile;


        public GameMapScreen(string saveFile = "")
        {
            SaveFile = saveFile;
        }
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
        private void GenerateUI_SaveMenu()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem(Toolbox.UIItemType.TextFix, "Save Game", new UIAction(Toolbox.UIAction.GameMap_SaveGame), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
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
            MousePos = input.MousePosition();
            UpdatePlayer(gameTime, input);
        }

        private void UpdatePlayer(object gameTime, InputManager input)
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
            FileManager.SaveToFile(gameSave, "saves", "save" + DateTime.Now.ToShortDateString().Replace("/", "") + DateTime.Now.ToLongTimeString().Replace(":", "").Replace(" AM", "").Replace(" PM", "") + ".tots");
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

            //Draw the player(s)
            spriteBatch.Draw(
                ToT.Textures[GameMap.player.ImageName],
                    (
                        GameMap.player.TileIndex * ToT.Settings.TileSize + 
                        (new Vector2(ToT.Settings.BorderSize, ToT.Settings.BorderSize) * GameMap.player.TileIndex) + 
                        new Vector2((ToT.Settings.TileSize.X - ToT.Textures[GameMap.player.ImageName].Width) / 2, (ToT.Settings.TileSize.Y - ToT.Textures[GameMap.player.ImageName].Height) / 2)
                    ),
                null,
                Color.White
            );
        }
    }
}
