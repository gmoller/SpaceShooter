using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace AnimationLibrary
{
    public class Animations
    {
        private readonly Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
       private readonly Dictionary<int, Animation> _animations2 = new Dictionary<int, Animation>();

        //public Animation this[string key] => _animations[key];

        //public Animation this[int index] => _animations2[index];
        //{
        //    get
        //    {
        //        Dictionary<string, Animation>.ValueCollection values = _animations.Values;

        //        return values[0];
        //    }
        //}

        public Animation GetByName(string name)
        {
            Animation animation = _animations[name];

            return animation;
        }

        public Animation GetById(int id)
        {
            Animation animation = _animations2[id];

            return animation;
        }

        public int Length => _animations.Count;

        public void Add(Animation animation)
        {
            _animations.Add(animation.Name, animation);
            _animations2.Add(_animations2.Count, animation);
        }
    }

    public class Animation
    {
        public string Name { get; }
        private Repeat.Mode _repeatMode;
        public AnimationFrame[] Frames { get; }
        public bool IsStarted { get; private set; }
        public AnimationFrame CurrentFrame { get; private set; }
        public double Time { get; private set; }
        public double TotalDuration { get; }

        public Animation(string name, AnimationFrame[] frames)
        {
            Name = name;
            Frames = frames;
            TotalDuration = frames.Sum(x => x.Duration);
            CurrentFrame = Frames[0];
        }

        #region Cloning

        public Animation Clone() => new Animation(Name, Frames);

        //public Animation FlipX() => new Animation(Name, Frames.Select(x => x.FlipX()).ToArray());

        //public Animation FlipY() => new Animation(Name, Frames.Select(x => x.FlipY()).ToArray());

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

        private AnimationFrame GetFrame(double amount)
        {
            double time = amount * TotalDuration;
            double current = 0.0d;

            foreach (AnimationFrame frame in Frames)
            {
                current += frame.Duration;
                if (time <= current)
                {
                    return frame;
                }
            }

            return default(AnimationFrame);
        }
    }
}