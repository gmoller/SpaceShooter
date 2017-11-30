using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PenetratorGame;
using ScreenManagerLibrary;

namespace SpaceShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();

            InputManager.Initialize();
            _screenManager = new ScreenManager();
            _screenManager.AddScreen(new PenetratorScreen(GraphicsDevice));
            _screenManager.AddScreen(new TestScreen(GraphicsDevice));
            _screenManager.GotoScreen("Penetrator");
            _screenManager.Initialize();
            _screenManager.GotoScreen("TestScreen");
            _screenManager.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Screen penetrator = _screenManager.GetScreen(0);
            penetrator.LoadContent(Content);
            Screen testScreen = _screenManager.GetScreen(1);
            testScreen.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputManager.Update();
            if (InputManager.IsKeyTriggered(Keys.D1))
            {
                _screenManager.GotoScreen("Penetrator");
            }
            if (InputManager.IsKeyTriggered(Keys.D2))
            {
                _screenManager.GotoScreen("TestScreen");
            }

            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}