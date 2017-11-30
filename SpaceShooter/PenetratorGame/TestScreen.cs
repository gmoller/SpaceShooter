using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ScreenManagerLibrary;

namespace PenetratorGame
{
    public class TestScreen : Screen
    {
        private RingThing _ringThing;

        public TestScreen(GraphicsDevice device) : base(device, "TestScreen")
        {
        }

        public override void Initialize()
        {
            _ringThing = new RingThing(GraphicsDevice);
        }

        public override void LoadContent(ContentManager content)
        {
            _ringThing.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _ringThing.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _ringThing.Draw();
        }
    }
}