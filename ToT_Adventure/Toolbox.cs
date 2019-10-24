using System;
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
        public enum ScreenType
        {
            MainMenu,
            Game,
            Splash,
            GameOver
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

        #endregion
    }
}
