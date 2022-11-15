using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Vector2 ReadInput(Vector2 pos, int width)
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState kbState = new KeyboardState();
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
            if (kbState.IsKeyDown(Keys.Space))
            {
                AnimationState = states.Jumping;
                direction.Y = -1;
            }
            AnimationState = states.Idle;
            return direction;
        }
    }
}
