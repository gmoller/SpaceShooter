using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; }
        public Point CellSize { get; }
        public Point CellOffset { get; }
        public Point CellOrigin { get; }
        public double FrameDefaultDuration { get; }
        public SpriteEffects FrameDefaultEffects { get; }

        public SpriteSheet(Texture2D texture) : this(texture, null, null, null) { }

        private SpriteSheet(Texture2D texture, Point? cellSize = null, Point? cellOffset = null, Point? cellOrigin = null, double frameDuration = 200.0f, SpriteEffects frameEffects = SpriteEffects.None)
        {
            Texture = texture;
            CellSize = cellSize ?? new Point(32, 32);
            CellOffset = cellOffset ?? new Point(0, 0);
            CellOrigin = cellOrigin ?? CellSize / new Point(2, 2);
            FrameDefaultDuration = frameDuration;
            FrameDefaultEffects = frameEffects;
        }

        #region Grid

        public SpriteSheet WithGrid(Cell cell, Offset offset, CellOrigin cellOrigin)
        {
            return new SpriteSheet(Texture, new Point(cell.W, cell.H), new Point(offset.X, offset.Y), new Point(cellOrigin.X, cellOrigin.Y));
        }

        public SpriteSheet WithGrid(Cell cell, Offset offset)
        {
            return WithGrid(cell, offset, new CellOrigin(cell.W / 2, cell.H / 2));
        }

        public SpriteSheet WithGrid(Cell cell)
        {
            return WithGrid(cell, new Offset(0, 0), new CellOrigin(cell.W / 2, cell.H / 2));
        }

        #endregion

        #region Frame default settings

        public SpriteSheet WithFrameDuration(double durationInMs)
        {
            return new SpriteSheet(Texture, CellSize, CellOffset, CellOrigin, durationInMs, FrameDefaultEffects);
        }

        public SpriteSheet WithFrameEffect(SpriteEffects effects)
        {
            return new SpriteSheet(Texture, CellSize, CellOffset, CellOrigin, FrameDefaultDuration, effects);
        }

        #endregion

        public Frame CreateFrame(int id, int x, int y, double duration = 0.0d, SpriteEffects effects = SpriteEffects.None)
        {
            x = x * CellSize.X + CellOffset.X;
            y = y * CellSize.Y + CellOffset.Y;
            var area = new Rectangle(x, y, CellSize.X, CellSize.Y);

            return new Frame(id, Texture, area, CellOrigin, duration, effects);
        }

        public Animation CreateAnimation(string name, params FrameStruct[] frames)
        {
            int i = 0;
            List<Frame> framesList = new List<Frame>();
            foreach (FrameStruct f in frames)
            {
                Frame frame = CreateFrame(i++, f.X, f.Y, f.Duration == null ? FrameDefaultDuration : f.Duration.GetValueOrDefault(), f.Effects == null ? FrameDefaultEffects : f.Effects.GetValueOrDefault());
                framesList.Add(frame);
            }

            Frame[] frames2 = framesList.ToArray();
            var animation = new Animation(name, Texture, frames2);

            return animation;
        }
    }

    public struct Cell
    {
        public int W { get; }
        public int H { get; }

        public Cell(int w, int h) : this()
        {
            W = w;
            H = h;
        }

        public override string ToString()
        {
            return $"W: {W} H: {H}";
        }
    }

    public struct Offset
    {
        public int X { get; }
        public int Y { get; }

        public Offset(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }
    }

    public struct CellOrigin
    {
        public int X { get; }
        public int Y { get; }

        public CellOrigin(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }
    }

    public struct FrameStruct
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double? Duration { get; set; }
        public SpriteEffects? Effects { get; set; }

        public FrameStruct(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public FrameStruct(int x, int y, double duration) : this()
        {
            X = x;
            Y = y;
            Duration = duration;
        }

        public FrameStruct(int x, int y, double duration, SpriteEffects effects) : this()
        {
            X = x;
            Y = y;
            Duration = duration;
            Effects = effects;
        }

        public FrameStruct(int x, int y, SpriteEffects effects) : this()
        {
            X = x;
            Y = y;
            Effects = effects;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Duration: {Duration} SpriteEffects: {Effects}";
        }
    }
}