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

        public enum States
        {
            Idle,
            RunningLeft,
            RunningRight,
            Jumping, 
            LostHealth
        }
        public States AnimationState { get { return animationState; } set { animationState = value; } }
        private States animationState;

        public int SpeedLeft { get { return speedLeft; } set { speedLeft = value; } }
        private int speedLeft = 1;
        public int SpeedRight { get { return speedRight; } set { speedRight = value; } }
        private int speedRight = 1;

        
        public Vector2 ReadInput(Vector2 pos, Hero hero)
        {
            AnimationState = States.Idle;
            Vector2 direction = Vector2.Zero;
            KeyboardState kbState = Keyboard.GetState();
            
            if(kbState.IsKeyDown(Keys.Left))
            {
                
                direction.X = -1 * speedLeft;
                AnimationState = States.RunningLeft;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction.X = 1 * speedRight;
                AnimationState = States.RunningRight;
            }


            hero.Position += hero.Velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hero.IsFalling && !hero.HasJumped && hero.Position.Y >= hero.CurrentFloorTile.Y)
            {
                hero.Position -= new Vector2(0, 10f);
                //hero.velocity.Y = -5f;
                hero.Velocity = new Vector2(hero.Velocity.X, -5f);
                hero.HasJumped = true;
            }

            if (hero.IsFalling)
            {
                AnimationState = States.Jumping;
                //hero.velocity.Y += 0.15f * 1;
                hero.Velocity += new Vector2(0, 0.15f * 1);

            }

            if (hero.Position.Y >= hero.CurrentFloorTile.Y + 50)
            {
                hero.IsFalling = false;
                hero.HasJumped = false;

            }
            if(hero.Position.Y <= hero.CurrentFloorTile.Y)
            {
                hero.HasJumped = true;
                hero.IsFalling = true;

            }

            if (hero.HasJumped == false)
            {
                if(hero.Position.Y >= hero.CurrentFloorTile.Y)
                {
                    hero.HasJumped = false;
                }
                if (hero.Position.Y <= hero.CurrentFloorTile.Y)
                {
                    hero.IsFalling = true;

                }
            }
            speedLeft = 1;
            speedRight = 1;
            return direction;
            
        }

        
    }
}
