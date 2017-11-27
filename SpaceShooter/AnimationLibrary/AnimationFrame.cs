using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public struct AnimationFrame
    {
        public int SpriteSheetId { get; set; }
        public int SpriteSheetFrameId { get; set; }
        public double Duration { get; set; }
        public SpriteEffects Effects { get; set; }

        public AnimationFrame(int spriteSheetId, int frameId, double duration, SpriteEffects effects = SpriteEffects.None) : this()
        {
            SpriteSheetId = spriteSheetId;
            SpriteSheetFrameId = frameId;
            Duration = duration;
            Effects = effects;
        }

        public override string ToString()
        {
            return $"SpriteSheetId: {SpriteSheetId} SpriteSheetFrameId: {SpriteSheetFrameId} Duration: {Duration} SpriteEffects: {Effects}";
        }
    }
}