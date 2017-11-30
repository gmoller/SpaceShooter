using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PenetratorGame
{
    public class RingThing
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private Texture2D _flash;
        private Vector2 _position;
        private float _rotation;
        private Vector2 _scale;
        private Vector2 _origin;
        private Rectangle _sourceRectangle;
        private float _cnt;

        public RingThing(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public void LoadContent(ContentManager content)
        {
            _flash = content.Load<Texture2D>("flash");
            _position = new Vector2(_graphicsDevice.Viewport.Width / 2.0f, _graphicsDevice.Viewport.Height / 2.0f);
            _sourceRectangle = new Rectangle(0, 0, _flash.Width, _flash.Height);
            _rotation = 0.0f;
            _origin = new Vector2(256.0f, 256.0f);
            _scale = new Vector2(1.0f, 1.0f);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            _rotation += 1.0f * delta;

            _cnt += 2.0f * delta;
            float s = (float)Math.Sin(_cnt); // gets a number between -1 and 1
            //_scale = new Vector2(s + 3.5f, s + 1.5f) * 0.5f;
            _scale = new Vector2(s + 1.5f, s + 1.5f) * 0.5f;
        }

        public void Draw()
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_flash, _position, _sourceRectangle, Color.Red, _rotation, _origin, _scale, SpriteEffects.None, 0.0f);

            _spriteBatch.End();
        }
    }
}