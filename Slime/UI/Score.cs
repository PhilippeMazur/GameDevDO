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
        public int score = 0;

        public void Draw(SpriteBatch spriteBatch, SpriteFont sf)
        {
            spriteBatch.DrawString(sf, $"Score : {score}", new Vector2(0, 50), Color.White);
        }
    }
}
