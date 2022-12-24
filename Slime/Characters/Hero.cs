using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.Characters
{
    public class Hero
    {
        public Texture2D heroTexture;
        KeyboardReader inputReader;
        public Vector2 position = new Vector2(100, 500f);
        Animation animation = new Animation();
        private Vector2 snelheid = new Vector2(5, 4);
        public Vector2 currentFloorTile = new Vector2(0, 500);
        public Vector2 previousFloorTile = new Vector2(0, 500);
        public float floorTileDifference;
        public Rectangle hitbox;
        public Rectangle hitboxBody;
        private Texture2D hitboxTexture;
        public bool showHitbox = false;
        public int health = 5;
        public bool isAlive = true;
        public int coinsLevel1 = 0;
        public int coinsLevel2 = 0;

        public Vector2 velocity;
        public bool hasJumped = false;
        public bool isFalling = false;

        public Hero(Texture2D heroTexture, KeyboardReader inputReader)
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
        public void LoadContent()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            hitboxBody = new Rectangle((int)position.X, (int)position.Y, 30, 35);

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D heroTexture)
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
            Move(gameTime);
            animation.Update(gameTime, kb);

            
        }
        private void Move(GameTime gameTime)
        {
            Vector2 direction = inputReader.ReadInput(position, this);
            direction *= snelheid;
            position += direction;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            
            hitboxBody.X = (int)position.X + 10;
            hitboxBody.Y = (int)position.Y + 13;
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
                coinsLevel1 = 0;
                position = new Vector2(100, 500);
            }
        }
    }
}
