using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PenetratorGame
{
    public class Camera2D
    {
        private readonly GraphicsDevice _graphicsDevice;

        private float _zoom;
        public float Zoom
        {
            get { return _zoom; }
            private set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        private float Rotation { get; set; }

        public Vector2 Position { get; set; }

        public Camera2D(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _zoom = 1.0f;
            Rotation = 0.0f;
            Position = new Vector2(graphicsDevice.Viewport.Width / 2.0f, graphicsDevice.Viewport.Height / 2.0f);
        }

        public void Update(GameTime gameTime)
        {
            const float speed = 100.0f; // 100 pixels per second (viewspace/screen)
            const float zoomSpeed = 2.0f;
            const float roationSpeed = 5.0f;

            if (InputManager.IsActionPressed(InputManager.Action.ResetCamera))
            {
                Position = new Vector2(_graphicsDevice.Viewport.Width * 0.5f, _graphicsDevice.Viewport.Height * 0.5f);
                Zoom = 1.0f;
                Rotation = 0.0f;
            }
            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterUp))
            {
                Move(new Vector2(0.0f, -speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterDown))
            {
                Move(new Vector2(0.0f, speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterLeft))
            {
                Move(new Vector2(-speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0.0f));
            }
            if (InputManager.IsActionPressed(InputManager.Action.MoveCharacterRight))
            {
                Move(new Vector2(speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0.0f));
            }
            if (InputManager.IsActionPressed(InputManager.Action.ZoomIn))
            {
                Zoom += zoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.IsActionPressed(InputManager.Action.ZoomOut))
            {
                Zoom -= zoomSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.IsActionPressed(InputManager.Action.RotateLeft))
            {
                Rotation += roationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.IsActionPressed(InputManager.Action.RotateRight))
            {
                Rotation -= roationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void Move(Vector2 amount)
        {
            Position += amount;
        }

        private void LookAt(Vector2 position)
        {
            Position = position;
        }

        public Matrix GetTransformation()
        {
            // position * rotation * zoom * center of screen
            Matrix transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0.0f)) *
                                                        Matrix.CreateRotationZ(Rotation) *
                                                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1.0f)) *
                                                        Matrix.CreateTranslation(new Vector3(_graphicsDevice.Viewport.Width * 0.5f, _graphicsDevice.Viewport.Height * 0.5f, 0.0f));

            return transform;
        }
    }
}