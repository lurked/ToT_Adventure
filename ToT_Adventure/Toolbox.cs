﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public static class Toolbox
    {
        #region constants
        public const string GAMEFILES_EXT_LEVEL = ".totl";
        public const string GAMEFILES_EXT_SETTINGS = ".totc";
        #endregion

        #region enums
        public enum GameState
        {
            MainMenu,
            GameMap,
            GameLevel,
            GameOver,
            Quitting,
            LoadMenu,
            Loading
        }
        
        public enum ScreenType
        {
            MainMenu,
            GameMap,
            GameLevel,
            GameOver,
            LoadGame
        }
        public enum TileSize
        {
            x72y72,
            x128y128
        }
        public enum Font
        {
            logo01,
            debug01,
            debug02,
            menuItem01,
            menuItem02,
            menuItem03
        }
        public enum TextAlignment
        {
            TopLeft,
            MiddleLeft,
            BottomLeft,
            TopCenter,
            MiddleCenter,
            BottomCenter,
            TopRight,
            MiddleRight,
            BottomRight
        }
        public enum UIType
        {
            Basic,
            BasicInvis,
            BasicOpaque
        }
        public enum UIItemType
        {
            TextFix,
            TextFloating,
            ImageFix,
            ImageFloating,
            TextImage,
            ImageText
        }

        public enum UIAlignment
        {
            Vertical,
            Horizontal,
            FreeRoam
        }
        public enum UIAction
        {
            Nothing,
            MainMenu_NewGame,
            MainMenu_LoadGame,
            MainMenu_Settings,
            MainMenu_Exit,
            MainMenu,
            GameMap_SaveGame,
            GameMap_LoadGame,
            GameMap_MainMenu,
            GameMap_Exit,
            Toggle_UII
        }

        public enum CollisionType
        {
            Mouse_Menu,
            Mouse_Entity
        }

        public enum TileType
        {
            Home,
            Dirt,
            Plains,
            Forest,
            Dungeon,
            Castle
        }

        public enum EntityType
        {
            Player,
            Enemy,
            NPC,
            Decor,
            Resource
        }
        public enum Stat
        {
            HP,
            Strength,
            Intel,
            Dexterity,
            Speed
        }
        #endregion
    }
}
