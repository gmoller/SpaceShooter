using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch batch, SpriteSheets spriteSheets, Animation animation, Vector2 position, Vector2 scale, Color? color = null, float rotation = 0, float layerDepth = 0)
        {
            batch.Draw(spriteSheets, animation.CurrentFrame, position, color, rotation, scale, layerDepth);
        }

        public static void Draw(this SpriteBatch batch, SpriteSheets spriteSheets, Animation animation, Vector2 position, Color? color = null, float rotation = 0, Vector2? scale = null, float layerDepth = 0)
        {
            batch.Draw(spriteSheets, animation.CurrentFrame, position, color, rotation, scale, layerDepth);
        }

        public static void Draw(this SpriteBatch batch, SpriteSheets spriteSheets, AnimationFrame frame, Vector2 position, Color? color = null, float rotation = 0, Vector2? scale = null, float layerDepth = 0)
        {
            SpriteSheet sheet = spriteSheets[frame.SpriteSheetId];
            Texture2D texture = sheet.Texture;
            SpriteSheetFrame spriteSheetFrame = sheet.Frames[frame.SpriteSheetFrameId];
            Rectangle area = new Rectangle(spriteSheetFrame.X, spriteSheetFrame.Y, spriteSheetFrame.Width, spriteSheetFrame.Height);

            batch.Draw(texture: texture,
                       position: position,
                       sourceRectangle: area,
                       color: color ?? Color.White,
                       rotation: rotation,
                       origin: new Vector2(spriteSheetFrame.Width / 2.0f, spriteSheetFrame.Height / 2.0f), // frame.Origin.ToVector2(),
                       scale: scale ?? Vector2.One,
                       effects: frame.Effects,
                       layerDepth: layerDepth);
        }
    }
}