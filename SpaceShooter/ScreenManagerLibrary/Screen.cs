using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagerLibrary
{
    public abstract class Screen
    {
        protected GraphicsDevice GraphicsDevice;

        public string Name { get; }

        protected Screen(GraphicsDevice device, string name)
        {
            Name = name;
            GraphicsDevice = device;
        }

        public virtual void Initialize()
        {
        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Shutdown()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }
    }
}