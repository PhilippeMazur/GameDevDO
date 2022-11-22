using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using Slime.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.Characters
{
    internal class Hero : IGameObject
    {
        public Texture2D heroTexture;
        IInputreader inputReader;
        public Vector2 position = new Vector2(100, 500f);
        Animation animation = new Animation();
        private Vector2 snelheid = new Vector2(7, 4);
        public Vector2 currentFloorTile = new Vector2(0, 540);
        public Vector2 previousFloorTile = new Vector2(0, 540);
        public float floorTileDifference;
        public Rectangle hitbox;
        public Rectangle hitboxBody;
        private Texture2D hitboxTexture;
        public bool showHitbox = true;
        public int health = 5;
        public bool isAlive = true;

        public Hero(Texture2D heroTexture, IInputreader inputReader)
        {
            this.heroTexture = heroTexture;
            this.inputReader = inputReader;
            
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 100, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 100, 50, 50)));
            


        }
        public void LoadContent(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            hitboxBody = new Rectangle((int)position.X, (int)position.Y, 27, 30);
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
                    spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
                }
            } else
            {
                position = new Vector2(100, 500f);
                health = 5;
                isAlive = true;

            }

            
        }

        public void Update(GameTime gameTime, KeyboardReader kb)
        {
            CheckHealth();
            Move();
            animation.Update(gameTime, kb);
        }
        private void Move()
        {
            Vector2 direction = inputReader.ReadInput(position, this);
            direction *= snelheid;
            position += direction;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            
            hitboxBody.X = (int)position.X + 10;
            hitboxBody.Y = (int)position.Y + 15;
        }
        public void drawHitbox(SpriteBatch spriteBatch,bool b)
        {
            
            if(b)
            {
                spriteBatch.Draw(hitboxTexture, hitbox, Color.Pink);
                spriteBatch.Draw(hitboxTexture, hitboxBody, Color.Yellow);
            }
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        private void CheckHealth()
        {
            if(health <= 0)
            {
                isAlive = false;
            }
        }
    }
}
