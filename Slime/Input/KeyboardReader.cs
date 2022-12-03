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
    public class KeyboardReader 
    {

        public enum states
        {
            Idle,
            RunningLeft,
            RunningRight,
            Jumping
        }
        public states AnimationState;
        //public bool hasJumped = false;
        float i = 5;
        //public bool isFalling;
        public int speed = 1;

        public int speedLeft = 1;
        public int speedRight = 1;

        
        public Vector2 ReadInput(Vector2 pos, Hero hero)
        {
            AnimationState = states.Idle;
            Vector2 direction = Vector2.Zero;
            KeyboardState kbState = Keyboard.GetState();
            
            if(kbState.IsKeyDown(Keys.Left))
            {
                
                direction.X = -1 * speedLeft;
                AnimationState = states.RunningLeft;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction.X = 1 * speedRight;
                AnimationState = states.RunningRight;
            }

            /*
            
            if (kbState.IsKeyDown(Keys.Space) && hasJumped == false && !isFalling)
            {

                AnimationState = states.Jumping;
                hero.position.Y -= 5f * i;
                direction.Y = -3f * i;
                hasJumped = true;

            }

            /// <summary>GRAVITY</summary>
            if (hasJumped == true)
            {
                isFalling = true;
                direction.Y += 0.15f * i;
                AnimationState = states.Jumping;
            }
            if (hero.position.Y >= hero.currentFloorTile.Y)
            {
                //isFalling = false;
                hasJumped = false;
                
            }

            if (hasJumped == false)
            {
                if (hero.position.Y <= hero.currentFloorTile.Y)
                {
                    isFalling = true;
                    direction.Y = 1;

                }
            }*/
            hero.position += hero.velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hero.hasJumped && !hero.isFalling)
            {
                hero.position.Y -= 10f;
                hero.velocity.Y = -5f;
                hero.hasJumped = true;
            }
            if (hero.hasJumped)
            {
                float i = 10;
                hero.velocity.Y += 0.15f * 1;
            }
            if (!hero.hasJumped)
            {
                hero.velocity.Y = 0;
            }
            if (hero.hasJumped == true)
            {
                hero.isFalling = true;
                //direction.Y += 0.15f * i;
                AnimationState = states.Jumping;
            }
            if (hero.position.Y >= hero.currentFloorTile.Y)
            {
                hero.isFalling = false;
                hero.hasJumped = false;

            }

            if (hero.hasJumped == false)
            {
                if(hero.position.Y >= hero.currentFloorTile.Y)
                {
                    hero.velocity.Y = 3;
                }
                if (hero.position.Y <= hero.currentFloorTile.Y)
                {
                    hero.isFalling = true;
                    direction.Y = 1;

                }
            }
            speed = 1;
            speedLeft = 1;
            speedRight = 1;
            return direction;
            
        }

        
    }
}
