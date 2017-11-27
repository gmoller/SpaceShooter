using System;
using AnimationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameExtensions;

namespace PenetratorGame
{
    public class Penetrator
    {
        private SpriteBatch _spriteBatch;
        private PixelFont _font;
        private SpriteSheets _sheets;
        private Animations _animations;
        private int _animIndex = -1;

        private MouseState _previousMouseState;

        public void Initialize()
        {
            _font = new PixelFont();
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);

            _sheets = new SpriteSheets();
            SpriteSheet sheet = SpriteSheetLoader.LoadFromFile(@"Content\characters.spritesheet.json", content);
            _sheets.Add(sheet);
            _animations = AnimationsLoader.LoadFromFile(@"Content\animations1.json");
            NextAnimation();

            _font.LoadContent(graphicsDevice);
        }

        private void NextAnimation()
        {
            _animIndex = (_animIndex + 1) % _animations.Length;
            _animations.GetById(_animIndex).Start(Repeat.Mode.Loop);
        }

        public void Update(GameTime gameTime)
        {
            _animations.GetById(_animIndex).Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                NextAnimation();
            }
            _previousMouseState = mouseState;
        }

        public void Draw(GameTime gameTime)
        {
            var borders = new Color(1.0f, 1.0f, 1.0f, 0.2f);

            try
            {
                _spriteBatch.Begin();

                Animation animation = _animations.GetById(_animIndex);

                AnimationFrame animationFrame = animation.CurrentFrame;
                int key = animationFrame.SpriteSheetId;
                SpriteSheet sheet = _sheets[key];

                // Full texture
                _spriteBatch.Draw(sheet.Texture, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawRectangle(sheet.Texture.Bounds, borders);

                // Animation
                _spriteBatch.Draw(_sheets, animation, new Vector2(16, 16 + sheet.Texture.Height), new Vector2(2.0f, 2.0f));
                _spriteBatch.DrawCross(new Vector2(16, 16 + sheet.Texture.Height), Color.Aqua);
                _font.Draw(_spriteBatch, new Vector2(120, 12 + sheet.Texture.Height), $"{_animIndex}-{animation.CurrentFrame.SpriteSheetFrameId} : {animation.Name}", Color.White);

                // Frames in full texture
                for (int i = 0; i < animation.Frames.Length; i++)
                {
                    AnimationFrame frame = animation.Frames[i];
                    sheet = _sheets[frame.SpriteSheetId];
                    SpriteSheetFrame spriteSheetFrame = sheet.Frames[frame.SpriteSheetFrameId];
                    Rectangle area = new Rectangle(spriteSheetFrame.X, spriteSheetFrame.Y, spriteSheetFrame.Width, spriteSheetFrame.Height);

                    _spriteBatch.DrawRectangle(area, borders);
                    _spriteBatch.DrawCross(area.Location.ToVector2() + new Vector2(spriteSheetFrame.Width / 2.0f, spriteSheetFrame.Height / 2.0f), borders); // frame.Origin.ToVector2(),
                    _font.Draw(_spriteBatch, area.Location.ToVector2() + new Vector2(4, 4), $"{i}", borders);
                }

                //Mouse
                const int mouseGrid = 16;
                var mx = (Mouse.GetState().X / mouseGrid) * mouseGrid;
                var my = (Mouse.GetState().Y / mouseGrid) * mouseGrid;
                _spriteBatch.DrawLine(0, my, 1920, my, Color.Green); // GraphicsDevice.Viewport.Width
                _spriteBatch.DrawLine(mx, 0, mx, 1080, Color.Blue); // GraphicsDevice.Viewport.Height
                _font.Draw(_spriteBatch, new Vector2(mx, my) + new Vector2(32, 32), $"({mx},{my})", borders);

                _spriteBatch.End();
            }
            catch (Exception ex)
            {
                _font.Draw(_spriteBatch, Vector2.Zero, $"{ex.Message}", borders);
            }
        }
    }
}