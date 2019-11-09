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
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Settings Settings;
        public static InputManager input;
        public static ScreenManager screenManager;
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Texture2D> Textures;
        public static ContentManager ContentMgr;
        public static Toolbox.GameState State = Toolbox.GameState.MainMenu;
        public static UIAction UIAction;
        public static bool DebugMode = false;
        public static Camera PlayerCamera;

        private readonly FrameCounter FrameCounter = new FrameCounter();

        public ToT()
        {
            graphics = new GraphicsDeviceManager(this);
            Settings = new Settings();
            graphics.PreferredBackBufferWidth = (int)Settings.Resolution.X;
            graphics.PreferredBackBufferHeight = (int)Settings.Resolution.Y;
            graphics.IsFullScreen = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 144.0f);
            //TargetElapsedTime = new TimeSpan(1 / 144);
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
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

            Fonts = new Dictionary<string, SpriteFont>();
            Textures = new Dictionary<string, Texture2D>();
            ContentMgr = Content;

            InitializeFonts();
            InitializeTextures();

            input = new InputManager();

            Camera tCam = new Camera(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            PlayerCamera = tCam;
            PlayerCamera.SetFocalPoint(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2));

            screenManager = new ScreenManager();
            screenManager.LoadContent();

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
            Settings = null;
            input = null;
            Textures = null;
            screenManager.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();
            screenManager.CheckMouseCollision();
            screenManager.Update(gameTime, input);

            if (input.ButtonPressed(Buttons.Back) || input.KeyPressed(Keys.Escape))
                Exit();

            if (input.KeyPressed(Keys.F12))
                if (DebugMode)
                    DebugMode = false;
                else
                    DebugMode = true;

            if (UIAction != null)
            {
                switch (UIAction.Action)
                {
                    case Toolbox.UIAction.MainMenu_Exit:
                        Exit();
                        break;
                    case Toolbox.UIAction.GameMap_Exit:
                        Exit();
                        break;
                }
            }

            base.Update(gameTime);
        }

        private void ShowFPS(GameTime gameTime)
        {
            if (DebugMode)
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", Math.Round(FrameCounter.AverageFramesPerSecond));
                spriteBatch.DrawString(Fonts[Toolbox.Font.debug01.ToString()], fps, ToT.PlayerCamera.Position + new Vector2(Settings.Resolution.X - 80, 0), Color.CornflowerBlue, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null, PlayerCamera.ViewMatrix);

            base.Draw(gameTime);
            screenManager.Draw(spriteBatch);

            ShowFPS(gameTime);

            spriteBatch.End();
        }
    }
}
