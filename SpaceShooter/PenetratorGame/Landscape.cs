using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameExtensions;

namespace PenetratorGame
{
    public class Landscape
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private Vector2[] _points;

        public Landscape(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
        }

        public void Initialize()
        {
            _points = new Vector2[100];
            float z = _graphicsDevice.Viewport.Height;
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = new Vector2(i * 50, z);
                if (IsEven(i))
                {
                    z -= 10.0f;
                }
            }
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public void Draw(Matrix transformation)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, transformation);

            Vector2 point1 = _points[0];
            for (int i = 1; i < _points.Length; i++)
            {
                Vector2 point2 = _points[i];
                _spriteBatch.DrawLine(point1, point2, Color.Blue, 3.0f);
                point1 = point2;
            }

            _spriteBatch.End();
        }
    }
}