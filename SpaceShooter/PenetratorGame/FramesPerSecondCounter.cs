using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenetratorGame
{
    public class FramesPerSecondCounter
    {
        private readonly GraphicsDevice _graphicsDevice;

        private int _totalFrames;
        private float _elapsedTime;
        private int _fps;

        public FramesPerSecondCounter(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsedTime >= 1000.0f) // 1 second
            {
                _fps = _totalFrames;
                _totalFrames = 0;
                _elapsedTime = 0.0f;
            }
        }

        public override string ToString()
        {
            _totalFrames++;
            return _fps.ToString();
        }
    }
}