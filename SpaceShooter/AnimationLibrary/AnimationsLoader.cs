using System.IO;
using Newtonsoft.Json;

namespace AnimationLibrary
{
    public class AnimationsLoader
    {
        public static Animations LoadFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            Animations animations = LoadFromJson(json);

            return animations;
        }

        public static Animations LoadFromJson(string json)
        {
            AnimationDefinitionData[] animationDefinitions = JsonConvert.DeserializeObject<AnimationDefinitionData[]>(json);
            Animations animations = Load(animationDefinitions);

            return animations;
        }

        public static Animations Load(AnimationDefinitionData[] animationDefinitions)
        {
            var animations = new Animations();
            foreach (var animationDefinition in animationDefinitions)
            {
                var animation = new Animation(animationDefinition.Name, animationDefinition.Frames);
                animations.Add(animation);
            }

            return animations;
        }
    }

    public struct AnimationDefinitionData
    {
        public AnimationDefinitionData(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public AnimationFrame[] Frames { get; set; }
    }
}