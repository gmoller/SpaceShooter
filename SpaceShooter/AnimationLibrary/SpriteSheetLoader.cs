using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace AnimationLibrary
{
    public static class SpriteSheetLoader
    {
        public static SpriteSheet LoadFromFile(string fileName, ContentManager content)
        {
            string json = File.ReadAllText(fileName);
            SpriteSheet sheet = LoadFromJson(json, content);

            return sheet;
        }

        public static SpriteSheet LoadFromJson(string json, ContentManager content)
        {
            SpriteSheetData parameters = JsonConvert.DeserializeObject<SpriteSheetData>(json);
            SpriteSheet sheet = Load(parameters, content);

            return sheet;
        }

        public static SpriteSheet Load(SpriteSheetData parameters, ContentManager content)
        {
            var texture2D = content.Load<Texture2D>(parameters.SpriteSheetTextureName);
            var sheet = new SpriteSheet(parameters.Id, texture2D, parameters.Frames);

            return sheet;
        }
    }

    public struct SpriteSheetData
    {
        public int Id { get; set; }
        public string SpriteSheetTextureName { get; set; }
        public SpriteSheetFrame[] Frames { get; set; }
    }
}