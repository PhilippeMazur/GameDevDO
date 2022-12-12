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
    public class Enemy
    {
        private Texture2D texture;
        public Vector2 position;
        public Vector2 startPosition;
        private int maxMoveDinstance;
        private float speedGround = 1f;
        private float speedAir = 1f;
        public bool isAlive = true;
        public Rectangle hitbox;
        private Texture2D hitboxTexture;
        private bool showHitbox = false;
        private Animation animation = new Animation();
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

        public int scoreValue;

        public Enemy(Texture2D texturein, Vector2 posin, int maxDistance,Type enemyType,  Level level)
        {
            texture = texturein;
            position = posin;
            startPosition.X = (int)posin.X;
            startPosition.Y = (int)posin.Y;
            isAlive = true;
            maxMoveDinstance = maxDistance;
            this.level = level;

            EnemyType = enemyType;

            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 0, 50, 50)));

        }
        public void LoadContent(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            hitboxBody = new Rectangle((int)position.X, (int)position.Y, 27, 30);
            hitboxTexture = new Texture2D(graphicsDevice, 1, 1);
            hitboxTexture.SetData(new[] { Color.White });
        }
        public void Draw(SpriteBatch spriteBatch, Hero hero)
        {
            if(isAlive)
            {
                drawHitbox(spriteBatch, showHitbox);

                if (!animation.goingLeft)
                {
                    spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
                }
            } else
            {
                position = startPosition;
            }
            
        }

        public void drawHitbox(SpriteBatch spriteBatch, bool b)
        {
            if (b)
            {
                spriteBatch.Draw(hitboxTexture, hitbox, Color.Pink);
                spriteBatch.Draw(hitboxTexture, hitboxBody, Color.Yellow);

            }
        }



        public void Update(GameTime gameTime, Hero hero)
        {
            animation.UpdateEnemy(gameTime ,this);
            Move(hero);
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;

            hitboxBody.X = (int)position.X + 10;
            hitboxBody.Y = (int)position.Y + 15;

        }

        public void Move(Hero hero)
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
                if(position.X < hero.position.X)
                {
                    position.X += speedGround / 2;
                    animationState = AnimationState.runningRight;
                }
                if (position.X > hero.position.X)
                {
                    position.X -= speedGround / 2;
                    animationState = AnimationState.runningLeft;
                }
                if (position.Y < hero.position.Y)
                {
                    position.Y += speedGround / 2;
                }
                if (position.Y > hero.position.Y)
                {
                    position.Y -= speedGround / 2;
                }

            }
            
        }

    }
}
