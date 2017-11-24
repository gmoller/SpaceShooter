using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace AnimationLibrary
{
    public static class SpriteSheetLoader
    {
        public static Animation[] LoadFromFile(string fileName, ContentManager content)
        {
            string json = File.ReadAllText(fileName);
            Animation[] animations = LoadFromJson(json, content);

            return animations;
        }

        public static Animation[] LoadFromJson(string json, ContentManager content)
        {
            SpriteSheetLoaderParameters parameters = JsonConvert.DeserializeObject<SpriteSheetLoaderParameters>(json);
            Animation[] animations = Load(parameters, content);

            return animations;
        }

        public static Animation[] Load(SpriteSheetLoaderParameters parameters, ContentManager content)
        {
            var texture2D = content.Load<Texture2D>(parameters.SpriteSheetTextureName);
            var sheet = new SpriteSheet(texture2D).WithGrid(new Cell(parameters.CellWidth, parameters.CellHeight));

            List<Animation> animations = new List<Animation>();
            foreach (AnimationDefinition animation in parameters.Animations)
            {
                var frames = new FrameStruct[animation.Frames.Length];
                for (int i = 0; i < animation.Frames.Length; i++)
                {
                    frames[i] = animation.Frames[i];
                }

                Animation anim = sheet.CreateAnimation(animation.Name, frames);
                animations.Add(anim);
            }

            return animations.ToArray();
        }
    }

    public struct SpriteSheetLoaderParameters
    {
        public string SpriteSheetTextureName { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public AnimationDefinition[] Animations { get; set; }
    }

    public struct AnimationDefinition
    {
        public AnimationDefinition(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public FrameStruct[] Frames { get; set; }
    }
}