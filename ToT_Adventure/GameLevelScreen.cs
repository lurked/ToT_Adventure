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
    public class GameLevelScreen : Screen
    {
        public class CurrentDecor
        {
            public int LayerIndex;
            public Vector2 TileIndex;
        }
        private CurrentDecor CurrentThing { get; set; }

        public GameLevelScreen()
        {
            ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].InitLevel();
        }
        public override void LoadAssets()
        {
            base.LoadAssets();
            GenerateUI_Adventure();
            GenerateUI_Resources();
        }
        #region GenerateUI
        private void GenerateUI_Adventure()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem("gamemap", Toolbox.UIItemType.TextFix, "Return to Game Map", new UIAction(Toolbox.UIAction.GameLevel_GameMap, ""), Toolbox.Font.menuItem02, Toolbox.TextAlignment.MiddleCenter);
            mMenuUI.uiItems.Add(mMenuUII);

            mMenuUI.RefreshUISize(false);
            mMenuUI.Position = new Vector2(5, ToT.Settings.Resolution.Y - mMenuUI.Size.Y);
            mMenuUI.RefreshUISize();

            UIs.Add("GameMapMenu", mMenuUI);
            UIs["GameMapMenu"].Visible = true;
        }

        private void GenerateUI_Resources()
        {
            UI mMenuUI = new UI();
            UIItem mMenuUII;
            mMenuUII = new UIItem(
                "imgGold", 
                Toolbox.UIItemType.ImageText, 
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Gold].ToString(), 
                new UIAction(), 
                Toolbox.Font.menuItem02, 
                Toolbox.TextAlignment.TopLeft, 
                "Resource_Gold_01"
                );
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(
                "imgWood", 
                Toolbox.UIItemType.ImageText, 
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Wood].ToString(), 
                new UIAction(), 
                Toolbox.Font.menuItem02, 
                Toolbox.TextAlignment.TopLeft, 
                "Resource_Wood_01"
                );
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(
                "imgRock", 
                Toolbox.UIItemType.ImageText, 
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Rock].ToString(), 
                new UIAction(), 
                Toolbox.Font.menuItem02, 
                Toolbox.TextAlignment.TopLeft, 
                "Resource_Rock_01"
                );
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUII = new UIItem(
                "imgCrystal", 
                Toolbox.UIItemType.ImageText, 
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Crystal].ToString(), 
                new UIAction(), 
                Toolbox.Font.menuItem02, 
                Toolbox.TextAlignment.TopLeft, 
                "Resource_Crystal_01"
                );
            mMenuUI.uiItems.Add(mMenuUII);
            mMenuUI.uIAlignment = Toolbox.UIAlignment.Horizontal;

            mMenuUI.RefreshUISize(false);
            mMenuUI.Position = new Vector2(5, 5);
            mMenuUI.RefreshUISize();

            UIs.Add("ResourcesMenu", mMenuUI);
            UIs["ResourcesMenu"].Visible = true;
        }
        #endregion
        public void UpdateResourcesUI()
        {
            UIs["ResourcesMenu"].uiItems[0].DisplayText = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Gold].ToString();
            UIs["ResourcesMenu"].uiItems[1].DisplayText = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Wood].ToString();
            UIs["ResourcesMenu"].uiItems[2].DisplayText = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Rock].ToString();
            UIs["ResourcesMenu"].uiItems[3].DisplayText = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Crystal].ToString();
        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            //Update player state and properties
            UpdatePlayer(input);

            //Update what's currently under the mouse cursor
            UpdateHover(input.MouseRectReal(ToT.PlayerCamera.Position),
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].Level);

            //Do stuff if we click on the current thing
            ClickOnCurrentThing(input);

            UpdateResourcesUI();

            //Update the game state
            base.Update(gameTime, input);

        }

        private void ClickOnCurrentThing(InputManager input)
        {
            if (CurrentThing != null && input.MouseClick())
            {
                DoCurrentThing(CurrentThing,
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                    [((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex].Level
                );
            }
        }

        private void DoCurrentThing(CurrentDecor currentThing, Level lvl)
        {
            switch(lvl.Things[currentThing.LayerIndex][currentThing.TileIndex].Kind)
            {
                case Toolbox.EntityType.Resource:
                    if (lvl.Things[currentThing.LayerIndex][currentThing.TileIndex].GetType() == typeof(Tree))
                    {
                        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Wood]++;
                        ((Tree)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty--;
                        if (((Tree)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty <= 0)
                            lvl.Things[currentThing.LayerIndex].Remove(currentThing.TileIndex);
                    }
                    else if (lvl.Things[currentThing.LayerIndex][currentThing.TileIndex].GetType() == typeof(Rock))
                    {
                        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Rock]++;
                        ((Rock)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty--;
                        if (((Rock)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty <= 0)
                            lvl.Things[currentThing.LayerIndex].Remove(currentThing.TileIndex);
                    }
                    else if (lvl.Things[currentThing.LayerIndex][currentThing.TileIndex].GetType() == typeof(Crystal))
                    {
                        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Resources[Toolbox.ResourceType.Crystal]++;
                        ((Crystal)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty--;
                        if (((Crystal)lvl.Things[currentThing.LayerIndex][currentThing.TileIndex]).ResourceQty <= 0)
                            lvl.Things[currentThing.LayerIndex].Remove(currentThing.TileIndex);
                    }
                    break;
            }
        }

        private void UpdateHover(Rectangle mouseRect, Level level)
        {
            CurrentThing = null;
            foreach (KeyValuePair<int, Dictionary<Vector2, Thing>> tTs in level.Things)
            {
                foreach (KeyValuePair<Vector2, Thing> tT in tTs.Value)
                {
                    if (mouseRect.Intersects(tT.Value.Anime.LevelRectangle(tT.Key, ToT.Settings.LevelTileSize)))
                    {
                        tT.Value.IsHover = true;
                        CurrentThing = new CurrentDecor() { LayerIndex = tTs.Key, TileIndex = tT.Key };
                    }
                    else
                    {
                        tT.Value.IsHover = false;
                    }
                }
            }
        }

        private void UpdatePlayer(InputManager input)
        {
            UpdatePlayerMovement(input);
            ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.UpdateLevelMove();

            ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.Update();
        }

        private void UpdatePlayerMovement(InputManager input)
        {
            Vector2 vCurrentPos = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelPosition;
            Toolbox.Orientation ori = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Orientation;
            float pSpeed = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.GetStat(Toolbox.Stat.Speed);
            ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.State = Toolbox.EntityState.Idle;
            if (input.KeyDown(Keys.Left) || input.KeyDown(ToT.Settings.Controls[Toolbox.Controls.MoveLeft]))
            {
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.FY = 1;
                vCurrentPos = new Vector2(vCurrentPos.X - pSpeed, vCurrentPos.Y);
                ori = Toolbox.Orientation.West;
            }
            if (input.KeyDown(Keys.Right) || input.KeyDown(ToT.Settings.Controls[Toolbox.Controls.MoveRight]))
            {
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.FY = 2;
                vCurrentPos = new Vector2(vCurrentPos.X + pSpeed, vCurrentPos.Y);
                ori = Toolbox.Orientation.East;
            }
            if (input.KeyDown(Keys.Up) || input.KeyDown(ToT.Settings.Controls[Toolbox.Controls.MoveUp]))
            {
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.FY = 3;
                vCurrentPos = new Vector2(vCurrentPos.X, vCurrentPos.Y - pSpeed);
                ori = Toolbox.Orientation.North;
            }
            if (input.KeyDown(Keys.Down) || input.KeyDown(ToT.Settings.Controls[Toolbox.Controls.MoveDown]))
            {
                ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.FY = 0;
                vCurrentPos = new Vector2(vCurrentPos.X, vCurrentPos.Y + pSpeed);
                ori = Toolbox.Orientation.South;
            }
            if (vCurrentPos != ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelPosition)
                MovePlayer(ori, vCurrentPos);
        }
        private void MovePlayer(Toolbox.Orientation orientation, Vector2 vDestPos)
        {
            Rectangle pSourceRect = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.SourceRect;
            //Vector2 origin = new Vector2(pSourceRect.Width / 2, pSourceRect.Height / 2);
            Vector2 vLvlArea = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].Level.Size * ToT.Settings.LevelTileSize;
            //vDestPos = vDestPos - origin;
            //if (vDestPos.X >= ToT.Settings.LevelTileSize.X / 2
            //    && vDestPos.X <= vLvlArea.X - ToT.Settings.LevelTileSize.X / 2
            //    && vDestPos.Y >= ToT.Settings.LevelTileSize.Y / 2
            //    && vDestPos.Y <= vLvlArea.Y - ToT.Settings.LevelTileSize.Y / 2)
            //{
                //if (
                //    !CheckIfThingBlocking(
                //        vDestPos, 
                //        pSourceRect, 
                //        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                //        [
                //            ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                //        ].Level.Things
                //        )
                //    )
                //{
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.State = Toolbox.EntityState.Moving;
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelMoveTo(orientation, vDestPos);
                //}
            //}
        }

        private bool CheckIfThingBlocking(Vector2 vDestPos, Rectangle pSourceRect, Dictionary<int, Dictionary<Vector2, Thing>> things)
        {
            Rectangle rectThing;
            bool hasThing = false;
            Rectangle rectPlayer;
            rectPlayer = new Rectangle(
                            (int)(vDestPos.X),
                            (int)(vDestPos.Y),
                            pSourceRect.Width,
                            pSourceRect.Height
                            );
            foreach (KeyValuePair<int, Dictionary<Vector2, Thing>> tTs in things)
            {
                foreach(KeyValuePair<Vector2, Thing> tT in tTs.Value)
                {
                    rectThing = new Rectangle(
                        (int)(tT.Key.X * ToT.Settings.LevelTileSize.X), 
                        (int)(tT.Key.Y * ToT.Settings.LevelTileSize.Y), 
                        tT.Value.Anime.SourceRect.Width, 
                        tT.Value.Anime.SourceRect.Height
                        );
                    if (rectPlayer.Intersects(rectThing))
                    {
                        hasThing = true;
                        return hasThing;
                    }
                }
            }
            return hasThing;
        }

        private static string GetImgName(Toolbox.TileType tileType, int iLen, int jLen, int iInd, int jInd)
        {
            string imgPrefix = "terrain{tilesize}\\tile_{terrain}_{spriteposY}{spriteposX}";
            string imgName = "";
            string imgPrename;
            imgPrefix = imgPrefix.Replace("{tilesize}", ToT.Settings.LevelTileSize.X.ToString());
            imgPrefix = imgPrefix.Replace("{terrain}", tileType.ToString().ToLower().Replace("level_", ""));
            if (iInd == 0)
            {
                imgPrename = imgPrefix.Replace("{spriteposX}", "left");
            }
            else if (iInd == iLen - 1)
            {
                imgPrename = imgPrefix.Replace("{spriteposX}", "right");
            }
            else
            {
                imgPrename = imgPrefix.Replace("{spriteposX}", "middle");
            }

            imgName = imgPrename;
            if (jInd == 0)
            {
                imgName = imgName.Replace("{spriteposY}", "top");
            }
            else if (jInd == jLen - 1)
            {
                imgName = imgName.Replace("{spriteposY}", "bottom");
            }
            else
            {
                imgName = imgName.Replace("{spriteposY}", "middle");
            }

            return imgName;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            string imgName;
            int iRoom = 0;

            foreach (KeyValuePair<Vector2, int[,]> arrRoom in ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].Level.Map)
            {
                for (int i = 0; i < arrRoom.Value.GetLength(0); i++)
                {
                    for (int j = 0; j < arrRoom.Value.GetLength(1); j++)
                    {
                        if (arrRoom.Value[i, j] == 1)
                        {
                            int iLen = arrRoom.Value.GetLength(0);
                            int jLen = arrRoom.Value.GetLength(1);
                            imgName = GetImgName(Toolbox.TileType.Level_Plains, iLen, jLen, i, j);
                            spriteBatch.Draw(ToT.Textures[imgName], arrRoom.Key * ToT.Settings.LevelTileSize + new Vector2(i, j) * ToT.Settings.LevelTileSize, null, Color.White);
                        }
                    }
                }
                spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug02.ToString()], iRoom.ToString(), arrRoom.Key * ToT.Settings.LevelTileSize, Color.White);
                iRoom++;
            }

            //foreach (KeyValuePair<Vector2, Tile> tile in ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
            //    [
            //        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
            //    ].Level.Tileset)
            //{
            //    spriteBatch.Draw(
            //        ToT.Textures[tile.Value.ImageName],
            //        tile.Key * ToT.Settings.LevelTileSize,
            //        null,
            //        Color.White
            //    );
            //}

            //foreach (KeyValuePair<int, Dictionary<Vector2, Thing>> things in ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
            //    [
            //        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
            //    ].Level.Things)
            //{
            //    foreach(KeyValuePair<Vector2, Thing> thing in things.Value)
            //    {
            //        spriteBatch.Draw(
            //            ToT.Textures[thing.Value.ImageName],
            //            thing.Key * ToT.Settings.LevelTileSize,
            //            null,
            //            thing.Value.IsHover ? Color.Red : Color.White
            //        );
            //        if (ToT.DebugMode)
            //            spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug02.ToString()], (thing.Value.Anime.LevelRectangle(thing.Key, ToT.Settings.LevelTileSize)).ToString(), thing.Key * ToT.Settings.LevelTileSize, Color.White);
            //    }
            //}

            //Draw the player(s)
            Rectangle pSourceRect = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.SourceRect;
            Vector2 origin = new Vector2(pSourceRect.Width / 2, pSourceRect.Height / 2);
            spriteBatch.Draw(
                ToT.Textures[((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.Anime.ImageName],
                (
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelPosition
                ),
                pSourceRect,
                Color.White,
                0f,
                origin,
                1f,
                SpriteEffects.None,
                0.0f
            );
            Player tPlayer = ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player;
            Rectangle rectPlayer;
            rectPlayer = new Rectangle(
                            pSourceRect.X + (int)(tPlayer.LevelPosition.X),
                            pSourceRect.Y + (int)(tPlayer.LevelPosition.Y),
                            pSourceRect.Width,
                            pSourceRect.Height
                            );
            if (ToT.DebugMode)
                spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug02.ToString()], rectPlayer.ToString(), tPlayer.LevelPosition, Color.White);

            base.Draw(spriteBatch);
        }
    }
}
