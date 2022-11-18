using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Slime.Characters;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public bool hasJumped = false;
        float i = 5;
        public bool isFalling = true;
        public bool isGrounded = false;
        public int speed = 1;
        
        public Vector2 ReadInput(Vector2 pos, int width, Hero hero)
        {
            AnimationState = states.Idle;
            Vector2 direction = Vector2.Zero;
            KeyboardState kbState = Keyboard.GetState();
            
            if(kbState.IsKeyDown(Keys.Left))
            {
                
                direction.X = -1 * speed;
                AnimationState = states.RunningLeft;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction.X = 1 * speed;
                AnimationState = states.RunningRight;
            }



            if (kbState.IsKeyDown(Keys.Space) && hasJumped == false && !isFalling)
            {
                
                AnimationState = states.Jumping;
                hero.position.Y -= 0.1f * i;
                direction.Y += -5f * i;
                hasJumped = true;
                
            }

            /// <summary>GRAVITY</summary>
            if (hasJumped == true)
            {
                isFalling = true;
                direction.Y += 0.25f * i;
                AnimationState = states.Jumping;
            }
            if (hero.position.Y >= hero.floorTile.Y)
            {
                isFalling = false;
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                isFalling = false;
                if (hero.position.Y <= hero.floorTile.Y)
                {
                    isFalling = true;
                    direction.Y = 1;

                }
            }
            speed = 1;
            return direction;
            
        }
    }
}
