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
                    CheckCollision(Toolbox.CollisionType.Mouse_Entity, Screens[Toolbox.ScreenType.GameMap]);
                    break;
                case Toolbox.GameState.GameLevel:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.GameLevel]);
                    CheckCollision(Toolbox.CollisionType.Mouse_Entity, Screens[Toolbox.ScreenType.GameLevel]);
                    break;
                case Toolbox.GameState.GameOver:
                    CheckCollision(Toolbox.CollisionType.Mouse_Menu, Screens[Toolbox.ScreenType.GameOver]);
                    break;
            }
        }

        private void CheckCollision(Toolbox.CollisionType checkType, Screen screen)
        {
            UIAction menuAction = new UIAction();
            switch (checkType)
            {
                case Toolbox.CollisionType.Mouse_Menu:
                    foreach (KeyValuePair<string, UI> ui in screen.UIs)
                    {
                        if (ui.Value.ToDraw)
                        {
                            if (ui.Value.Rectangle.Intersects(ToT.input.MouseRect()))
                            {
                                ui.Value.Active = true;

                                foreach(UIItem uii in ui.Value.uiItems)
                                {
                                    if (uii.ToDraw)
                                    {
                                        if (uii.GetRectangle().Intersects(ToT.input.MouseRect()))
                                        {
                                            uii.Active = true;
                                            if (ToT.input.MouseClick())
                                            {
                                                menuAction = uii.Action;
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
            if (menuAction.Action != Toolbox.UIAction.Nothing)
            {
                DoAction(menuAction);
            }
        }

        public void DoAction(UIAction menuAction)
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
                case Toolbox.UIAction.GameMap_SaveGame:
                    Screens[Toolbox.ScreenType.GameMap].Save();
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
                    Screens[Toolbox.ScreenType.MainMenu].Draw(spriteBatch);
                    break;
                case Toolbox.GameState.GameOver:
                    Screens[Toolbox.ScreenType.GameOver].Draw(spriteBatch);
                    break;
            }
        }
    }
}
