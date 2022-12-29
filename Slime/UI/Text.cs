using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
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
        private SpriteFont font;
        private string message;
        
        public Text(string messagein, Vector2 positionin, SpriteFont fontin)
        {
            message = messagein;
            position = positionin;
            xValue = position.X;
            yValue = position.Y;
            font = fontin;

        }
        public void Draw()
        {
            if (currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.DrawString(font, message, position, Color.White);
            }
        }
        public void Draw(SpriteFont font)
        {
            if (currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.DrawString(font, message, position, Color.White);
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
