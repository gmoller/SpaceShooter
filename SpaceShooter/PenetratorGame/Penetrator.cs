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
        private Animation[] _animations;
        private int _animIndex = -1;

        private MouseState _previousMouseState;

        public void Initialize()
        {
            _font = new PixelFont();
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);

            _animations = SpriteSheetLoader.LoadFromFile(@"Content\characters.json", content);
            NextAnimation();

            _font.LoadContent(graphicsDevice);
        }

        private void NextAnimation()
        {
            _animIndex = (_animIndex + 1) % _animations.Length;
            _animations[_animIndex].Start(Repeat.Mode.Loop);
        }

        public void Update(GameTime gameTime)
        {
            _animations[_animIndex].Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                NextAnimation();
            }
            _previousMouseState = mouseState;
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            var borders = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            Animation animation = _animations[_animIndex];

            // Full texture
            _spriteBatch.Draw(animation.SpriteSheet, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawRectangle(animation.SpriteSheet.Bounds, borders);

            // Animation
            _spriteBatch.Draw(animation, new Vector2(16, 16 + animation.SpriteSheet.Height), new Vector2(2.0f, 2.0f));
            _font.Draw(_spriteBatch, new Vector2(120, 12 + animation.SpriteSheet.Height), $"{_animIndex}-{animation.CurrentFrame.Id} : {animation.Name}", Color.White);

            // Frames in full texture
            for (int i = 0; i < animation.Frames.Length; i++)
            {
                Frame frame = animation.Frames[i];

                _spriteBatch.DrawRectangle(frame.Area, borders);
                _spriteBatch.DrawCross(frame.Area.Location.ToVector2() + frame.Origin.ToVector2(), borders);
                _font.Draw(_spriteBatch, frame.Area.Location.ToVector2() + new Vector2(4, 4), $"{i}", borders);
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
    }
}