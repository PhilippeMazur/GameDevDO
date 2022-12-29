using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct3D11;
using Slime.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Slime.Game1;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace Slime.Characters
{
    public class Enemy : IGameObject, IMovable
    {

        public Vector2 Position { get { return position; } set { position = value; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        private Texture2D texture;
        private Vector2 position;
        private Vector2 startPosition;
        private int maxMoveDinstance;
        private float speedGround = 1f;
        private bool isAlive = true;
        private Rectangle hitbox;
        private Animation animation = new Animation();
        private Hero hero;
        public enum AnimationState
        {
            runningLeft,
            runningRight
        }
        public AnimationState animationState;
        public Rectangle hitboxBody;
        public enum Level
        {
            Level1,
            Level2
        }
        public Level level;
        public enum Type
        {
            Ground, Air
        }
        public Type EnemyType;

        public int scoreValue = 1;

        public Enemy(Texture2D texturein, Vector2 posin, int maxDistance,Type enemyType,  Level level, Hero heroin)
        {
            texture = texturein;
            position = posin;
            startPosition.X = (int)posin.X;
            startPosition.Y = (int)posin.Y;
            isAlive = true;
            maxMoveDinstance = maxDistance;
            this.level = level;
            int width = 50;
            int height = 50;
            EnemyType = enemyType;
            hero = heroin;

            animation.AddFrame(new AnimationFrame(new Rectangle(0, 1, width, height)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 1, width, height)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 51, width, height)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 51, width, height)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 1, width, height)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 1, width, height)));

        }

        public void LoadContent()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            hitboxBody = new Rectangle((int)position.X, (int)position.Y, 27, 30);
        }
        public void Draw()
        {
            if(isAlive)
            {

                if (!animation.goingLeft)
                {
                    Game1._spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White);
                }
                else
                {
                    Game1._spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
                }
            } else
            {
                position = startPosition;
            }
            
        }
        public void Update(GameTime gameTime)
        {
            animation.UpdateEnemy(gameTime ,this);
            Move();
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;

            hitboxBody.X = (int)position.X + 10;
            hitboxBody.Y = (int)position.Y + 15;

        }

        public void Move()
        {
            if(EnemyType == Type.Ground)
            {
                position.X -= speedGround;
                if (position.X == startPosition.X - maxMoveDinstance)
                {
                    speedGround *= -1;
                    animationState = AnimationState.runningRight;
                }
                if (position.X == startPosition.X + maxMoveDinstance)
                {
                    speedGround *= -1;
                    animationState = AnimationState.runningLeft;
                }
            } else
            {
                if(hero.Health > 0 && currentState == GameStates.Level1)
                {
                    if (position.X < hero.Position.X)
                    {
                        position.X += speedGround / 2;
                        animationState = AnimationState.runningRight;
                    }
                    if (position.X > hero.Position.X)
                    {
                        position.X -= speedGround / 2;
                        animationState = AnimationState.runningLeft;
                    }
                    if (position.Y < hero.Position.Y)
                    {
                        position.Y += speedGround / 2;
                    }
                    if (position.Y > hero.Position.Y)
                    {
                        position.Y -= speedGround / 2;
                    }
                }
                

            }
        }

    }
}
