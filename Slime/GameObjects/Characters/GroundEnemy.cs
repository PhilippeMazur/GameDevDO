using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Characters;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.GameObjects.Characters
{
    internal class GroundEnemy : Enemy, IMovable
    {
        public GroundEnemy(Texture2D texturein, Vector2 posin, int maxDistance, Type enemyType, Level level, Hero heroin) : base(texturein, posin, maxDistance, enemyType, level, heroin)
        {
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
        }
        public void Move()
        {
            if (EnemyType == Type.Ground)
            {
                Position -= new Vector2(-SpeedGround, 0);
                if (Position.X == StartPosition.X - MaxMoveDistance)
                {
                    SpeedGround *= -1;
                    animationState = AnimationState.runningRight;
                }
                if (Position.X == StartPosition.X + MaxMoveDistance)
                {
                    SpeedGround *= -1;
                    animationState = AnimationState.runningLeft;
                }
            }
        }
    }
}
