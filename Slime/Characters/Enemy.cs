using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct3D11;
using Slime.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace Slime.Characters
{
    internal class Enemy : IGameObject
    {
        private Texture2D texture;
        public Vector2 position;
        public int startPosition;
        private int maxMoveDinstance = 150;
        private int speed = 1;
        public bool isAlive;
        public Rectangle hitbox;
        private Texture2D hitboxTexture;
        private bool showHitbox = true;
        private Animation animation = new Animation();
        



        public Enemy(Texture2D texturein, Vector2 posin)
        {
            texture = texturein;
            position = posin;
            startPosition = (int)posin.X;
            isAlive = true;

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
            hitboxTexture = new Texture2D(graphicsDevice, 1, 1);
            hitboxTexture.SetData(new[] { Color.White });
        }
        public void Draw(SpriteBatch spriteBatch)
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
            }
            
        }

        public void drawHitbox(SpriteBatch spriteBatch, bool b)
        {
            if (b)
            {
                spriteBatch.Draw(hitboxTexture, hitbox, Color.Yellow);
            }
        }



        public void Update(GameTime gameTime, KeyboardReader kb)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            Move();
            animation.Update(gameTime);
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            
        }

        public void Move()
        {
            position.X -= speed;
            if(position.X == startPosition - maxMoveDinstance)
            {
                speed *= -1;
            }
            if(position.X == startPosition + maxMoveDinstance)
            {
                speed *= -1;
            }
        }
    }
}
