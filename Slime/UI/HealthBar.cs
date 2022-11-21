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
    internal class HealthBar 
    {
        private Vector2 position = new Vector2(0, 0);
        

        public HealthBar()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Hero hero)
        {
            for (int i = 0; i < hero.health * 100; i+=100)
            {
                
                spriteBatch.Draw(texture, new Vector2(i, 0), new Rectangle(0, 0, 100, 100), Color.White);
                
            }
        }

        public void Update()
        {
            
        }
    }
}
