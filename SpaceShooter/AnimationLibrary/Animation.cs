using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public class Animation
    {
        public string Name { get; }
        private Repeat.Mode _repeatMode;
        public Frame[] Frames { get; }
        public bool IsStarted { get; private set; }
        public Frame CurrentFrame { get; private set; }
        public double Time { get; private set; }
        public double TotalDuration { get; }
        public Texture2D SpriteSheet { get; }

        public Animation(string name, Texture2D spriteSheet, Frame[] frames)
        {
            Name = name;
            SpriteSheet = spriteSheet;
            Frames = frames;
            TotalDuration = frames.Sum(x => x.Duration);
            CurrentFrame = Frames[0];
        }

        #region Cloning

        public Animation Clone() => new Animation(Name, SpriteSheet, Frames);

        public Animation FlipX() => new Animation(Name, SpriteSheet, Frames.Select(x => x.FlipX()).ToArray());

        public Animation FlipY() => new Animation(Name, SpriteSheet, Frames.Select(x => x.FlipY()).ToArray());

        #endregion

        public void Start(Repeat.Mode repeatMode)
        {
            Time = 0;
            _repeatMode = repeatMode;
            IsStarted = true;
        }

        public void Stop() => Pause();

        public void Pause() => IsStarted = false;

        public void Resume() => IsStarted = true;

        public void Reset()
        {
            Time = 0;
            _repeatMode = Repeat.Mode.Once;
            UpdateCurrentFrame();
        }

        public bool Update(GameTime time)
        {
            if (IsStarted)
            {
                Time += time.ElapsedGameTime.TotalMilliseconds;

                double amount = UpdateCurrentFrame();

                if (Repeat.IsFinished(_repeatMode, amount))
                {
                    Stop();
                    return true;
                }
            }

            return false;
        }

        private double UpdateCurrentFrame()
        {
            // Updating current frame
            float amount = (float)(Time / TotalDuration);
            double value = Repeat.Calculate(_repeatMode, amount);
            int i = Math.Max(0, Math.Min(Frames.Length - 1, (int)(value * Frames.Length)));
            CurrentFrame = Frames[i];

            return amount;
        }

        private Frame GetFrame(double amount)
        {
            double time = amount * TotalDuration;
            double current = 0.0d;

            foreach (Frame frame in Frames)
            {
                current += frame.Duration;
                if (time <= current)
                {
                    return frame;
                }
            }

            return null;
        }
    }
}