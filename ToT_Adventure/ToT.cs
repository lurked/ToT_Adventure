using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ToT_Adventure
{
    public class ToT : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Settings settings;
        public static InputManager input;
        public static ScreenManager screenManager;
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Texture2D> Textures;
        public static ContentManager ContentMgr;


        Starfield starfield;

        

        public ToT()
        {
            graphics = new GraphicsDeviceManager(this);
            settings = new Settings();
            graphics.PreferredBackBufferWidth = (int)settings.Resolution.X;
            graphics.PreferredBackBufferHeight = (int)settings.Resolution.Y;
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            input = new InputManager();

            screenManager = new ScreenManager();
            ContentMgr = Content;
            Fonts = new Dictionary<string, SpriteFont>();
            Textures = new Dictionary<string, Texture2D>();

            InitializeFonts();
            InitializeTextures();

            starfield = new Starfield((int)(settings.Resolution.X * 1.5f), (int)(settings.Resolution.Y * 1.5f), 200, new Vector2(10f, 10f), Textures["star03"], new Rectangle(0, 0, 7, 7));
        }

        private void InitializeFonts()
        {
            SpriteFont logoFont = ContentMgr.Load<SpriteFont>("fonts/AlinCartoon1");
            Fonts.Add(Toolbox.Font.logo01.ToString(), logoFont);
            SpriteFont debug01Font = ContentMgr.Load<SpriteFont>("fonts/Earth2073");
            Fonts.Add(Toolbox.Font.debug01.ToString(), debug01Font);
            SpriteFont debug02Font = ContentMgr.Load<SpriteFont>("fonts/Earth2073");
            Fonts.Add(Toolbox.Font.debug02.ToString(), debug02Font);
            SpriteFont menuItem01 = ContentMgr.Load<SpriteFont>("fonts/TECHNOLIN");
            Fonts.Add(Toolbox.Font.menuItem01.ToString(), menuItem01);
            SpriteFont menuItem02 = ContentMgr.Load<SpriteFont>("fonts/nasalization-rg");
            Fonts.Add(Toolbox.Font.menuItem02.ToString(), menuItem02);
            SpriteFont menuItem03 = ContentMgr.Load<SpriteFont>("fonts/nasalization-rg-small");
            Fonts.Add(Toolbox.Font.menuItem03.ToString(), menuItem03);
        }

        private void InitializeTextures()
        {
            string[] files = Directory.GetFiles("Resources\\sprites", "*.png");
            foreach (string tS in files)
            {
                FileStream filestream = new FileStream(tS, FileMode.Open);
                Texture2D tTexture = Texture2D.FromStream(GraphicsDevice, filestream);

                Textures.Add(Path.GetFileName(tS).Replace(".png", ""), tTexture);
                filestream.Close();
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ContentMgr.Unload();
            settings = null;
            input = null;
            Textures = null;
            starfield = null;
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();

            if (input.ButtonPressed(Buttons.Back) || input.KeyPressed(Keys.Escape))
                Exit();

            base.Update(gameTime);
            starfield.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            base.Draw(gameTime);
            starfield.Draw(spriteBatch);
            screenManager.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
