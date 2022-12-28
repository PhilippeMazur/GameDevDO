using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using SharpDX.Win32;
using Slime.Characters;
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
    public class GameKeyHandler
    {
        private Rectangle recPos;
        private Vector2 position;
        private double counter;
        private double counter2;
        private int randNumber = 1000;
        private int xValue;
        private int yValue;        
        public GameKeyHandler(Rectangle recPosIn, Vector2 positionin)
        {
            recPos = recPosIn;
            position = positionin;
            xValue = recPos.X;
            yValue = recPos.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if(currentState == GameStates.StartScreen)
            {
                spriteBatch.Draw(texture, position, recPos, Color.White);

            }
        }

        public void Update(GameTime gameTime, Hero hero)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Enter) && currentState == GameStates.StartScreen)
            {
                
                currentState = GameStates.Level1;
                hero.Position = new Vector2(100, 500f);
            }
            if(kbState.IsKeyDown(Keys.Enter) && currentState == GameStates.GameOver)
            {
                currentState = GameStates.StartScreen;
            }
            if (kbState.IsKeyDown(Keys.P))
            {
                hero.coinsLevel1--;
            }
            if(kbState.IsKeyDown(Keys.O))
            {
                hero.coinsLevel1++;
            }
            if (kbState.IsKeyDown(Keys.M) && currentState == GameStates.WinningScreen)
            {
                currentState = GameStates.StartScreen;
                hero.Position = new Vector2(0, 150);
            }
            if (kbState.IsKeyDown(Keys.N))
            {
                currentState = GameStates.WinningScreen;
            }
            if (kbState.IsKeyDown(Keys.B))
            {
                currentState = GameStates.WinningScreen;
            }
            if (kbState.IsKeyDown(Keys.V))
            {
                currentState = GameStates.GameOver;
            }

            counter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (counter >= 1000d)
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
