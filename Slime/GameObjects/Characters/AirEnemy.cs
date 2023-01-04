using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Characters;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;

namespace Slime.GameObjects.Characters
{
    internal class AirEnemy : Enemy, IFlyableEnemy
    {
        public AirEnemy(Texture2D texturein, Vector2 posin, int maxDistance, Type enemyType, Level level, Hero heroin) : base(texturein, posin, maxDistance, enemyType, level, heroin)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Fly();
        }

        public void Fly()
        {
            float speed = 1f;
            if(level == Level.Level1)
            {
                if (currentState == GameStates.Level1)
                {
                    Movement(speed);
                }
            } 
            if(level == Level.Level2)
            {
                if(currentState == GameStates.Level2)
                {
                    Movement(speed);
                }
            }
        }
        private void Movement(float speed)
        {
            if (Hero.Health > 0)
            {
                if (Position.X < Hero.Position.X)
                {
                    Position += new Vector2(speed / 2, 0);
                    animationState = AnimationState.runningRight;
                }
                if (Position.X > Hero.Position.X)
                {
                    Position -= new Vector2(speed / 2, 0);
                    animationState = AnimationState.runningLeft;
                }
                if (Position.Y < Hero.Position.Y)
                {
                    Position += new Vector2(0, speed / 2);
                }
                if (Position.Y > Hero.Position.Y)
                {
                    Position -= new Vector2(0, speed / 2);
                }
            }
        }
    }
}
