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
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 1000, 700)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1000, 0, 1000, 700)));
        }
        public void Draw()
        {
            //Game1._spriteBatch.Draw(Game1._gameOverScreenTexture, new Vector2(0,0), animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0,0),new Vector2(0,0), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            Game1._spriteBatch.Draw(GameSceneManager._gameOverScreenTexture, new Vector2(0, 0), animation.CurrentFrame.sourceRectangle, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime, 3);
        }
    }
}
