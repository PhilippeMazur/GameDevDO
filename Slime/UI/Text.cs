using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;

namespace Slime.UI
{
    public class Text
    {
        private Rectangle recPos;
        private Vector2 position;
        private double counter;
        private double counter2;
        private int randNumber = 1000;
        private float xValue;
        private float yValue;

        private string message;
        
        public Text(string messagein, Vector2 positionin)
        {
            message = messagein;
            position = positionin;
            xValue = position.X;
            yValue = position.Y;
            

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font)
        {
            if (currentState == GameStates.StartScreen)
            {
                _spriteBatch.DrawString(font, message, position, Color.White);
            }
        }
        public void Draw(SpriteFont font)
        {
            if (currentState == GameStates.StartScreen)
            {
                _spriteBatch.DrawString(font, message, position, Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (counter >= 500d)
            {
                counter = 0;
                position.X += randNumber;
                position.Y += randNumber;
                counter2++;
                if (counter2 >= 2)
                {
                    position.X = xValue;
                    position.Y = yValue;
                    counter2 = 0;
                }

            }
            
        }
        public void Update(GameTime gameTime, Hero hero)
        {
            counter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (counter >= 1000d)
            {
                counter = 0;
                recPos.X += randNumber;
                recPos.Y += randNumber;
                counter2++;
                if (counter2 >= 2)
                {
                    recPos.X = (int)xValue;
                    recPos.Y = (int)yValue;
                    counter2 = 0;
                }

            }
        }
    }
}
