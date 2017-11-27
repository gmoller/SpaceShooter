using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationLibrary
{
    public class SpriteSheets
    {
        private readonly Dictionary<int, SpriteSheet> _sheets = new Dictionary<int, SpriteSheet>();

        public SpriteSheet this[int key] => _sheets[key];

        public void Add(SpriteSheet sheet)
        {
            _sheets.Add(sheet.Id, sheet);
        }
    }

    public class SpriteSheet
    {
        public int Id { get; set; }
        public Texture2D Texture { get; }
        public SpriteSheetFrame[] Frames { get; }

        public SpriteSheet(int id, Texture2D texture, SpriteSheetFrame[] frames)
        {
            Id = id;
            Texture = texture;
            Frames = frames;
        }
    }
}