using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    public class WinningScreen
    {
        public Animation animation = new Animation();

        public WinningScreen()
        {
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 1000, 700)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1000, 0, 1000, 700)));
        }
        public void Draw(Texture2D texture)
        {
            Game1._spriteBatch.Draw(texture, new Vector2(0, 0), animation.CurrentFrame.sourceRectangle, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime, 3);
        }
    }
}
