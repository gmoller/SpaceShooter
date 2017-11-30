using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameExtensions;

namespace PenetratorGame
{
    public class StatusPane
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private readonly PixelFont _font;
        private readonly FramesPerSecondCounter _fps;

        public StatusPane(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _font = new PixelFont(_graphicsDevice);
            _fps = new FramesPerSecondCounter(_graphicsDevice);
        }

        public void LoadContent(ContentManager content)
        {
            _font.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            _fps.Update(gameTime);
        }

        public void Draw(Camera2D camera)
        {
            string fps = $"FPS: {_fps}";
            string viewport = $"Viewport: [X: {_graphicsDevice.Viewport.X}, Y: {_graphicsDevice.Viewport.Y}, Width: {_graphicsDevice.Viewport.Width}, Height: {_graphicsDevice.Viewport.Height}]";
            string screenMousePosition = $"Screen Mouse Position: [X: {Mouse.GetState().X}, Y: {Mouse.GetState().Y}]"; // viewspace
            Vector2 world = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(camera.GetTransformation()));
            string worldMousePosition = $"World Mouse Position: [X: {(int)world.X}, Y: {(int)world.Y}]"; // worldspace
            string zoom = $"Zoom: {camera.Zoom}";

            var position = new Vector2(10.0f, _graphicsDevice.Viewport.Height - _font.Measure(fps).Y - 60.0f);

            _spriteBatch.Begin();

            _spriteBatch.DrawRectangle(new Rectangle((int)position.X - 2, (int)position.Y - 2, 400, 69), Color.Gray);
            _spriteBatch.DrawString(_font, fps, position, Color.Gray);
            _spriteBatch.DrawString(_font, viewport, position + new Vector2(0.0f, 14.0f), Color.Gray);
            _spriteBatch.DrawString(_font, screenMousePosition, position + new Vector2(0.0f, 28.0f), Color.Gray);
            _spriteBatch.DrawString(_font, worldMousePosition, position + new Vector2(0.0f, 42.0f), Color.Gray);
            _spriteBatch.DrawString(_font, zoom, position + new Vector2(0.0f, 56.0f), Color.Gray);

            _spriteBatch.End();
        }
    }
}