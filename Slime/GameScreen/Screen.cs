using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using Slime.Interfaces;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Slime.GameScreen
{
    public abstract class Screen : IGameObject
    {
        private Texture2D texture;
        private Rectangle position;
        public Animation Animation { get { return animation; } set { animation = value; } }
        private Animation animation;
        //private Text text;
        public Screen(Texture2D texturein, Rectangle positionin, Animation Animationin)
        {
            texture = texturein;
            position = positionin;
            animation = Animationin;
        }
        public virtual void Draw()
        {
            Game1._spriteBatch.Draw(texture, new Vector2(position.X, position.Y), animation.CurrentFrame.sourceRectangle, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime, 3);
        }
    }
}
