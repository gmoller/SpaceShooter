namespace AnimationLibrary
{
    public struct SpriteSheetFrame
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SpriteSheetFrame(int x, int y, int width, int height) : this()
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Width: {Width} Height: {Height}";
        }
    }
}