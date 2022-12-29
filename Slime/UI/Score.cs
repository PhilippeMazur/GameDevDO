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
        private SpriteFont font;
        public int ScoreValue { get { return scoreValue; } set { scoreValue = value; } }
        private int scoreValue = 0;
        public Score(SpriteFont fontin)
        {
            font = fontin;
        }

        public void Draw()
        {
            Game1._spriteBatch.DrawString(font, $"Score : {scoreValue}", new Vector2(10, 50), Color.White);
        }

    }
}
