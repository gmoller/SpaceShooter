using System;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameExtensions;
using ScreenManagerLibrary;

namespace PenetratorGame
{
    public class PenetratorScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private PixelFont _font;
        private SpriteSheets _sheets;
        private Animations _animations;
        private int _animIndex = -1;
        private Camera2D _camera;
        private StatusPane _statusPane;
        private Landscape _landscape;

        private MouseState _previousMouseState;

        public PenetratorScreen(GraphicsDevice device) : base(device, "Penetrator")
        {
        }
        
        public override void Initialize()
        {
            _font = new PixelFont(GraphicsDevice);
            _camera = new Camera2D(GraphicsDevice);
            _statusPane = new StatusPane(GraphicsDevice);
            _landscape = new Landscape(GraphicsDevice);
            _landscape.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _sheets = new SpriteSheets();
            SpriteSheet sheet1 = SpriteSheetLoader.LoadFromFile(@"Content\characters.spritesheet.json", content);
            _sheets.Add(sheet1);
            SpriteSheet sheet2 = SpriteSheetLoader.LoadFromFile(@"Content\playership.spritesheet.json", content);
            _sheets.Add(sheet2);
            SpriteSheet sheet3 = SpriteSheetLoader.LoadFromFile(@"Content\explosion1.spritesheet.json", content);
            _sheets.Add(sheet3);
            _animations = AnimationsLoader.LoadFromFile(@"Content\animations1.json");
            NextAnimation();

            _font.LoadContent();
            _statusPane.LoadContent(content);
        }

        private void NextAnimation()
        {
            _animIndex = (_animIndex + 1) % _animations.Length;
            _animations.GetById(_animIndex).Start(Repeat.Mode.Loop);
        }

        public override void Update(GameTime gameTime)
        {
            _animations.GetById(_animIndex).Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                NextAnimation();
            }
            _previousMouseState = mouseState;

            _camera.Update(gameTime);
            _statusPane.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var borders = new Color(1.0f, 1.0f, 1.0f, 0.2f);

            try
            {
                Matrix transformation = _camera.GetTransformation();
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, transformation);

                _landscape.Draw(transformation);

                Animation animation = _animations.GetById(_animIndex);

                AnimationFrame animationFrame = animation.CurrentFrame;
                int key = animationFrame.SpriteSheetId;
                SpriteSheet sheet = _sheets[key];

                // Draw full texture
                _spriteBatch.Draw(sheet.Texture, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawRectangle(sheet.Texture.Bounds, borders);

                // Animation
                SpriteSheetFrame ssFrame = sheet.Frames[animationFrame.SpriteSheetFrameId];
                _spriteBatch.Draw(_sheets, animation, new Vector2(ssFrame.Width/2, ssFrame.Height/2 + sheet.Texture.Height));
                _spriteBatch.DrawCross(new Vector2(ssFrame.Width/2, ssFrame.Height/2 + sheet.Texture.Height), Color.Red); // 16,16
                _spriteBatch.DrawString(_font, $"{_animIndex}-{animation.CurrentFrame.SpriteSheetFrameId} : {animation.Name}", new Vector2(120, 12 + sheet.Texture.Height), Color.White);

                // Frames in full texture
                for (int i = 0; i < animation.Frames.Length; i++)
                {
                    AnimationFrame frame = animation.Frames[i];
                    sheet = _sheets[frame.SpriteSheetId];
                    SpriteSheetFrame spriteSheetFrame = sheet.Frames[frame.SpriteSheetFrameId];
                    Rectangle area = new Rectangle(spriteSheetFrame.X, spriteSheetFrame.Y, spriteSheetFrame.Width, spriteSheetFrame.Height);

                    _spriteBatch.DrawRectangle(area, borders);
                    _spriteBatch.DrawCross(area.Location.ToVector2() + new Vector2(spriteSheetFrame.Width / 2.0f, spriteSheetFrame.Height / 2.0f), borders); // frame.Origin.ToVector2(),
                    _spriteBatch.DrawString(_font, $"{i}", area.Location.ToVector2() + new Vector2(4, 4), borders);
                }

                _spriteBatch.End();

                _statusPane.Draw(_camera);
            }
            catch (Exception ex)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_font, $"{ex.Message}", Vector2.Zero, Color.White);
                _spriteBatch.End();
            }
        }
    }
}