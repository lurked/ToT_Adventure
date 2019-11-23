using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class ScreenManager
    {
        public Dictionary<Toolbox.ScreenType, Screen> Screens;

        public ScreenManager()
        {
            Screens = new Dictionary<Toolbox.ScreenType, Screen>
            {
                { Toolbox.ScreenType.MainMenu, new MainMenuScreen() }
            };
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadContent()
        {
            Screens[Toolbox.ScreenType.MainMenu].LoadAssets();
        }

        public virtual void UnloadContent()
        {
            foreach (KeyValuePair<Toolbox.ScreenType, Screen> screen in Screens)
            {
                screen.Value.UnloadAssets();
            }
        }

        public virtual void Update(GameTime gameTime, InputManager input)
        {
            ToT.PlayerCamera.Update();
            switch (ToT.State)
            {
                case Toolbox.GameState.MainMenu:
                    Screens[Toolbox.ScreenType.MainMenu].Update(gameTime, input);
                    break;
                case Toolbox.GameState.LoadMenu:
                    Screens[Toolbox.ScreenType.LoadGame].Update(gameTime, input);
                    break;
                case Toolbox.GameState.GameMap:
                    Screens[Toolbox.ScreenType.GameMap].Update(gameTime, input);
                    break;
                case Toolbox.GameState.GameLevel:
                    Screens[Toolbox.ScreenType.GameLevel].Update(gameTime, input);
                    ToT.PlayerCamera.Update();
                    break;
                case Toolbox.GameState.GameOver:
                    Screens[Toolbox.ScreenType.GameOver].Update(gameTime, input);
                    break;
            }
        }

        public void CheckMouseCollision()
        {
            switch (ToT.State)
            {
                case Toolbox.GameState.MainMenu:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.MainMenu]);
                    break;
                case Toolbox.GameState.LoadMenu:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.LoadGame]);
                    break;
                case Toolbox.GameState.GameMap:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.GameMap]);
                    if (Screens.ContainsKey(Toolbox.ScreenType.GameMap))
                    {
                        CheckCollision(Toolbox.CollisionType.Mouse_Entity, Screens[Toolbox.ScreenType.GameMap]);
                    }
                    break;
                case Toolbox.GameState.GameLevel:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.GameLevel]);
                    if (Screens.ContainsKey(Toolbox.ScreenType.GameLevel))
                    {
                        CheckCollision(Toolbox.CollisionType.Mouse_Entity, Screens[Toolbox.ScreenType.GameLevel]);
                    }
                    break;
                case Toolbox.GameState.GameOver:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.GameOver]);
                    break;
            }
        }

        private void CheckCollision(Toolbox.CollisionType checkType, Screen screen)
        {
            switch (checkType)
            {
                case Toolbox.CollisionType.Mouse_Menu:
                    foreach (KeyValuePair<string, UI> ui in screen.UIs)
                    {
                        if (ui.Value.Visible)
                        {
                            if (ui.Value.Rectangle.Intersects(ToT.input.MouseRect()))
                            {
                                ui.Value.Active = true;

                                foreach(UIItem uii in ui.Value.uiItems)
                                {
                                    if (uii.Visible)
                                    {
                                        if (uii.GetRectangle().Intersects(ToT.input.MouseRect()))
                                        {
                                            uii.Active = true;
                                            if (ToT.input.MouseClick())
                                            {
                                                DoAction(screen, uii.Action, ui.Value);
                                            }
                                        }
                                        else
                                        {
                                            uii.Active = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ui.Value.Active = false;
                                foreach (UIItem uii in ui.Value.uiItems)
                                {
                                    if (uii.Active)
                                        uii.Active = false;
                                }
                            }
                        }
                    }
                    break;
                case Toolbox.CollisionType.Mouse_Entity:

                    break;
            }
        }

        public void DoAction(Screen screen, UIAction menuAction, UI ui)
        {
            switch (menuAction.Action)
            {
                case Toolbox.UIAction.MainMenu_NewGame:
                    if (!Screens.ContainsKey(Toolbox.ScreenType.GameMap))
                        Screens.Add(Toolbox.ScreenType.GameMap, new GameMapScreen());
                    ToT.State = Toolbox.GameState.GameMap;
                    Screens[Toolbox.ScreenType.GameMap].LoadAssets();
                    ToT.PlayerCamera.SetFocalPoint(Vector2.Zero + ToT.Settings.TileSize / 2);
                    break;
                case Toolbox.UIAction.MainMenu:
                    ToT.State = Toolbox.GameState.MainMenu;
                    ToT.PlayerCamera.SetFocalPoint(Vector2.Zero + ToT.Settings.Resolution / 2);
                    break;
                case Toolbox.UIAction.MainMenu_LoadGame:
                    if (!Screens.ContainsKey(Toolbox.ScreenType.LoadGame))
                        Screens.Add(Toolbox.ScreenType.LoadGame, new LoadGameScreen());
                    Screens[Toolbox.ScreenType.LoadGame].LoadAssets();
                    ToT.State = Toolbox.GameState.LoadMenu;
                    ToT.PlayerCamera.SetFocalPoint(Vector2.Zero + ToT.Settings.Resolution / 2);
                    break;
                case Toolbox.UIAction.GameMap_LoadGame:
                    if (!Screens.ContainsKey(Toolbox.ScreenType.GameMap))
                        Screens.Add(Toolbox.ScreenType.GameMap, new GameMapScreen(menuAction.ActionParam));
                    ToT.State = Toolbox.GameState.GameMap;
                    Screens[Toolbox.ScreenType.GameMap].LoadAssets();
                    ToT.PlayerCamera.SetFocalPoint((((GameMapScreen)Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex * ToT.Settings.TileSize) + ToT.Settings.TileSize / 2);
                    break;
                case Toolbox.UIAction.MainMenu_Settings:

                    break;
                case Toolbox.UIAction.MainMenu_Exit:
                    ToT.UIAction = menuAction;
                    break;
                case Toolbox.UIAction.GameMap_Exit:
                    ((GameMapScreen)screen).Save();
                    ToT.UIAction = menuAction;
                    break;
                case Toolbox.UIAction.GameMap_MainMenu:
                    ((GameMapScreen)screen).Save();
                    Screens.Remove(Toolbox.ScreenType.GameMap);
                    ToT.State = Toolbox.GameState.MainMenu;
                    ToT.PlayerCamera.SetFocalPoint(Vector2.Zero + ToT.Settings.Resolution / 2);
                    break;
                case Toolbox.UIAction.GameMap_SaveGame:
                    Screens[Toolbox.ScreenType.GameMap].Save();
                    break;
                case Toolbox.UIAction.Toggle_UII:
                    string[] split = menuAction.ActionParam.Split(',');
                    foreach(UIItem uii in ui.uiItems)
                        if (split.Contains(uii.Name))
                            uii.Visible = !uii.Visible;
                    ui.RefreshUISize();

                    if (ui.Position.X + ui.Rectangle.Width > ToT.Settings.Resolution.X)
                        ui.Position = new Vector2(ToT.Settings.Resolution.X - (float)ui.Rectangle.Width, ui.Position.Y);
                    if (ui.Position.Y + ui.Rectangle.Height > ToT.Settings.Resolution.Y)
                        ui.Position = new Vector2(ui.Position.X, ToT.Settings.Resolution.Y - (float)ui.Rectangle.Height);

                    if (ui.Position.X < 0f)
                        ui.Position = new Vector2(0f, ui.Position.Y);
                    if (ui.Position.Y < 0f)
                        ui.Position = new Vector2(ui.Position.X, 0f);
                    ui.RefreshUISize();
                    break;
                case Toolbox.UIAction.GameMap_Adventure:
                    if (!Screens.ContainsKey(Toolbox.ScreenType.GameLevel))
                        Screens.Add(Toolbox.ScreenType.GameLevel, new GameLevelScreen());
                    ToT.State = Toolbox.GameState.GameLevel;
                    Screens[Toolbox.ScreenType.GameLevel].LoadAssets();
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelMoveTo(Toolbox.Orientation.South, ToT.Settings.LevelTileSize / 2);
                    ToT.PlayerCamera.SetFocalPoint(
                        ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.LevelPosition
                    );
                    break;
                case Toolbox.UIAction.GameLevel_GameMap:
                    Screens.Remove(Toolbox.ScreenType.GameLevel);
                    ToT.State = Toolbox.GameState.GameMap;
                    ToT.PlayerCamera.SetFocalPoint
                        (
                            (((GameMapScreen)Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex * ToT.Settings.TileSize) + 
                            ToT.Settings.TileSize / 2
                        );
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (ToT.State)
            {
                case Toolbox.GameState.MainMenu:
                    Screens[Toolbox.ScreenType.MainMenu].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.LoadMenu:
                    Screens[Toolbox.ScreenType.LoadGame].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.GameMap:
                    Screens[Toolbox.ScreenType.GameMap].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.GameLevel:
                    Screens[Toolbox.ScreenType.GameLevel].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.GameOver:
                    Screens[Toolbox.ScreenType.GameOver].Draw(spriteBatch);
                    break;
            }
            if (ToT.DebugMode)
                spriteBatch.DrawString(ToT.Fonts[Toolbox.Font.debug01.ToString()], ToT.input.MouseRectReal(ToT.PlayerCamera.Position).ToString(), ToT.input.MousePosition() + ToT.PlayerCamera.Position, Color.White);
        }
    }
}
