using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using Slime.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.UI
{
    public class Score
    {
        public SpriteFont font;
        public int score = 0;
        public Score()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, $"Score : {score}", new Vector2(10, 50), Color.White);
        }

    }
}
