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
        public static HealthBar Instance => instance ??= new HealthBar(); // deze lijn code vervangt lijn 23 tem 30
        private static HealthBar instance;
        private Texture2D texture;
        private Hero hero;
        private HealthBar()
        {
        }
        /*
        public static HealthBar getInstance()
        {
            if (instance == null)
                instance = new HealthBar();

            return instance;
        }
        */

        public void Initialise(Hero heroin,Texture2D texturein)
        {
            hero = heroin;
            texture = texturein;
        }

        public void Draw()
        {
            for (int i = 0; i < hero.Health * 50; i+=50)
            {
                
                Game1._spriteBatch.Draw(texture, new Vector2(i, 0), new Rectangle(0, 0, 100, 100), Color.White, 0,new Vector2(0,0), 0.5f, SpriteEffects.None, 0 );
                
            }
        }

    }
}
