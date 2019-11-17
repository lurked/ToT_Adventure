using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class GameLevelScreen : Screen
    {
        

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
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Vector2, Tile> tile in ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].Level.Tileset)
            {
                spriteBatch.Draw(
                    ToT.Textures[tile.Value.ImageName],
                    tile.Key * ToT.Settings.LevelTileSize,
                    null,
                    Color.White
                );
            }

            foreach (KeyValuePair<int, Dictionary<Vector2, Thing>> things in ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.Map
                [
                    ((GameMapScreen)ToT.screenManager.Screens[Toolbox.ScreenType.GameMap]).GameMap.player.TileIndex
                ].Level.Things)
            {
                foreach(KeyValuePair<Vector2, Thing> thing in things.Value)
                {
                    spriteBatch.Draw(
                        ToT.Textures[thing.Value.ImageName],
                        thing.Key * ToT.Settings.LevelTileSize,
                        null,
                        Color.White
                    );
                }
            }

            base.Draw(spriteBatch);
        }
    }
}
