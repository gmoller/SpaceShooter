using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public class Frame
    {
        public int Id { get; }
        public Texture2D Texture { get; }
        public Rectangle Area { get; }
        public Point Origin { get; }
        public double Duration { get; }
        public SpriteEffects Effects { get; }

        public Frame(int id, Texture2D texture, Rectangle area, Point origin, double duration, SpriteEffects effects)
        {
            Id = id;
            Texture = texture;
            Area = area;
            Origin = origin;
            Duration = duration;
            Effects = effects;
        }

        #region Cloning

        public Frame FlipX()
        {
            return new Frame(Id, Texture, Area, Origin, Duration, SpriteEffects.FlipHorizontally);
        }

        public Frame FlipY()
        {
            return new Frame(Id, Texture, Area, Origin, Duration, SpriteEffects.FlipVertically);
        }

        public Frame WithDuration(double duration)
        {
            return new Frame(Id, Texture, Area, Origin, duration, Effects);
        }

        public Frame WithOrigin(int x, int y)
        {
            return new Frame(Id, Texture, Area, new Point(x, y), Duration, Effects);
        }

        public Frame WithArea(Rectangle area)
        {
            return new Frame(Id, Texture, area, Origin, Duration, Effects);
        }

        #endregion
    }
}