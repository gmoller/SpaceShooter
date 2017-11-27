using AnimationLibrary;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace TestConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parameters = new SpriteSheetData
            {
                Id = 1,
                SpriteSheetTextureName = "characters",
                Frames = new SpriteSheetFrame[73]
            };

            for (int i = 0; i < 23; i++)
            {
                parameters.Frames[i] = new SpriteSheetFrame(i * 32, 0, 32, 32);
            }

            for (int i = 0; i < 23; i++)
            {
                parameters.Frames[i + 23] = new SpriteSheetFrame(i * 32, 1, 32, 32);
            }

            for (int i = 0; i < 23; i++)
            {
                parameters.Frames[i + 46] = new SpriteSheetFrame(i * 32, 2, 32, 32);
            }

            for (int i = 0; i < 4; i++)
            {
                parameters.Frames[i + 69] = new SpriteSheetFrame(i * 32, 3, 32, 32);
            }

            string json = JsonConvert.SerializeObject(parameters, Formatting.Indented);

            var animDefs = new AnimationDefinitionData[11];
            var animDef = new AnimationDefinitionData("WalkingBabyRight") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i, 200.0d);
            }
            animDefs[0] = animDef;

            animDef = new AnimationDefinitionData("WalkingBabyLeft") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[1] = animDef;

            animDef = new AnimationDefinitionData("WalkingBabyUp") { Frames = new AnimationFrame[5] };
            for (int i = 0; i < 5; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 18, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[2] = animDef;

            animDef = new AnimationDefinitionData("WalkingKingRight") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 23, 200.0d);
            }
            animDefs[3] = animDef;

            animDef = new AnimationDefinitionData("WalkingKingLeft") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 23, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[4] = animDef;

            animDef = new AnimationDefinitionData("WalkingKingUp") { Frames = new AnimationFrame[5] };
            for (int i = 0; i < 5; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 23 + 18, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[5] = animDef;

            animDef = new AnimationDefinitionData("WalkingExplorerRight") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 46, 200.0d);
            }
            animDefs[6] = animDef;

            animDef = new AnimationDefinitionData("WalkingExplorerLeft") { Frames = new AnimationFrame[18] };
            for (int i = 0; i < 18; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 46, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[7] = animDef;

            animDef = new AnimationDefinitionData("WalkingExplorerUp") { Frames = new AnimationFrame[5] };
            for (int i = 0; i < 5; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 46 + 18, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[8] = animDef;

            animDef = new AnimationDefinitionData("MovingSnakeRight") { Frames = new AnimationFrame[4] };
            for (int i = 0; i < 4; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 69, 200.0d);
            }
            animDefs[9] = animDef;

            animDef = new AnimationDefinitionData("MovingSnakeLeft") { Frames = new AnimationFrame[4] };
            for (int i = 0; i < 4; i++)
            {
                animDef.Frames[i] = new AnimationFrame(1, i + 69, 200.0d, SpriteEffects.FlipHorizontally);
            }
            animDefs[10] = animDef;

            json = JsonConvert.SerializeObject(animDefs, Formatting.Indented);
        }
    }
}