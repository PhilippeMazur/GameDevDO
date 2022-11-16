using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Slime.Characters;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Slime.Input
{
    internal class KeyboardReader : IInputreader
    {

        public enum states
        {
            Idle,
            RunningLeft,
            RunningRight,
            Jumping
        }
        public states AnimationState;
        private bool hasJumped = false;
        float i = 5;
        private bool isFalling = true;
        public Vector2 ReadInput(Vector2 pos, int width, Hero hero)
        {
            AnimationState = states.Idle;
            Vector2 direction = Vector2.Zero;
            KeyboardState kbState = Keyboard.GetState();
            if(kbState.IsKeyDown(Keys.Left))
            {
                
                direction.X = -1;
                AnimationState = states.RunningLeft;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
                AnimationState = states.RunningRight;
            }
            

            if (kbState.IsKeyDown(Keys.Space) && hasJumped == false)
            {
                
                AnimationState = states.Jumping;
                //hier tekst: praten met de capybara
                hero.position.Y -= 0.15f * i;
                //hoe hoog jumpen
                direction.Y += -3f * i;
                hasJumped = true;
            }
            if (hasJumped == true)
            {
                //de snelheid van de jump
                
                direction.Y += 0.25f * i;
                AnimationState = states.Jumping;
            }
            if (hero.position.Y >= hero.floorTile.Y)
            {
                hasJumped = false;
            }

            if(hasJumped == false)
            {
                if(hero.position.Y <= hero.floorTile.Y)
                direction.Y = 1;    
            }
            return direction;
            
        }
    }
}
