using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using Slime.Characters;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    public class HealthBar 
    {
        private Vector2 position = new Vector2(0, 0);
        private Texture2D texture;

        public HealthBar(Texture2D texturein)
        {
            texture = texturein;
        }

        public void Draw(Hero hero)
        {
            for (int i = 0; i < hero.Health * 50; i+=50)
            {
                
                Game1._spriteBatch.Draw(texture, new Vector2(i, 0), new Rectangle(0, 0, 100, 100), Color.White, 0,new Vector2(0,0), 0.5f, SpriteEffects.None, 0 );
                
            }
        }

    }
}
