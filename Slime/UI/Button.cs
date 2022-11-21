using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using SharpDX.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Slime.UI
{
    internal class Button
    {
        private Rectangle recPos;
        private Texture2D texture;
        private Vector2 position;
        private double counter;
        private double counter2;
        private int randNumber = 1000;
        private int xValue;
        private int yValue;
        



        public Button(Rectangle recPosIn, Vector2 positionin, Texture2D textureIn)
        {
            recPos = recPosIn;
            position = positionin;
            texture = textureIn;
            xValue = recPos.X;
            yValue = recPos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(currentState == GameStates.StartScreen)
            {
                spriteBatch.Draw(texture, position, recPos, Color.White);

            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Enter))
            {
                //Write code here
                Debug.WriteLine("test");
                currentState = GameStates.Level1;
            }

            counter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (counter >= 500d)
            {
                counter = 0;
                recPos.X += randNumber;
                recPos.Y += randNumber;
                counter2++;
                if (counter2 >= 2)
                {
                    recPos.X = xValue;
                    recPos.Y = yValue;
                    counter2 = 0;
                }
                
            }

        }

    }
}
