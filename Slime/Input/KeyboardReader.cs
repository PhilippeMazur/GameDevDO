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
            Jumping, 
            LostHealth
        }
        public states AnimationState;
        public int speed = 1;

        public int speedLeft = 1;
        public int speedRight = 1;
        public bool canJump;

        
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


            hero.Position += hero.velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hero.isFalling && !hero.hasJumped && hero.position.Y >= hero.currentFloorTile.Y)
            {
                hero.position.Y -= 10f;
                hero.velocity.Y = -5f;
                hero.hasJumped = true;
            }

            if (hero.isFalling)
            {
                AnimationState = states.Jumping;
                hero.velocity.Y += 0.15f * 1;
            }

            if (hero.position.Y >= hero.currentFloorTile.Y + 50)
            {
                hero.isFalling = false;
                hero.hasJumped = false;

            }
            if(hero.position.Y <= hero.currentFloorTile.Y)
            {
                hero.hasJumped = true;
                hero.isFalling = true;

            }

            if (hero.hasJumped == false)
            {
                if(hero.Position.Y >= hero.currentFloorTile.Y)
                {
                    hero.hasJumped = false;
                }
                if (hero.Position.Y <= hero.currentFloorTile.Y)
                {
                    hero.isFalling = true;

                }
            }
            speed = 1;
            speedLeft = 1;
            speedRight = 1;
            return direction;
            
        }

        
    }
}
