using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    public class Screen
    {
        private Texture2D texture;
        private Rectangle Position;
        public Animation animation;
        private Text text;
        public Screen(Texture2D texturein, Rectangle positionin, Animation Animationin, Text textin)
        {
            texture = texturein;
            Position = positionin;  
            animation = Animationin;  
            text = textin;
        }
        public void Draw()
        {
            Game1._spriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), animation.CurrentFrame.sourceRectangle, Color.White);
        }
        public void Draw(SpriteFont font)
        {
            Game1._spriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), animation.CurrentFrame.sourceRectangle, Color.White);
            text.Draw(font);

        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime, 3);
            text.Update(gameTime);
        }
    }
}
