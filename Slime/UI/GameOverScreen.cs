using Microsoft.Xna.Framework;
using Project1.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    internal class GameOverScreen
    {
        Animation animation = new Animation();

        public GameOverScreen()
        {
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 500, 500)));
            animation.AddFrame(new AnimationFrame(new Rectangle(500, 0, 500, 500)));
        }
        public void Draw(GameTime gameTime)
        {
            //Game1._spriteBatch.Draw(Game1._gameOverTexture, new Vector2(0,0), animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0,0),new Vector2(2,2), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);

        }
        public void update(GameTime gameTime)
        {
            animation.Update(gameTime, 3);
        }
    }
}
