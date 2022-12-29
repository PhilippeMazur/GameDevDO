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
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.Characters
{
    public class Hero : IGameObject, IMovable
    {
        private Texture2D heroTexture;
        private KeyboardReader inputReader;
        private Animation animation = new Animation();
        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position = new Vector2(100, 500f);
        private Vector2 snelheid = new Vector2(5, 4);
        public Vector2 CurrentFloorTile { get { return currentFloorTile; } set { currentFloorTile = value; } }
        private Vector2 currentFloorTile = new Vector2(0, 500);
        public Vector2 PreviousFloorTile { get { return previousFloorTile; } set { previousFloorTile = value; } }
        private Vector2 previousFloorTile = new Vector2(0, 500);
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }
        private Rectangle hitbox;
        public Rectangle HitboxBody { get { return hitboxBody; } set { hitboxBody = value; } }
        private Rectangle hitboxBody;
        public int Health { get { return health; } set { health = value; } }
        private int health = 5;
        private bool isAlive = true;
        public int CoinsLevel1 { get { return coinsLevel1; } set { coinsLevel1 = value; } }
        private int coinsLevel1 = 0;
        public int CoinsLevel2 { get { return coinsLevel2; } set { coinsLevel2 = value; } }
        private int coinsLevel2 = 0;
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 velocity;
        public bool HasJumped { get { return hasJumped; } set { hasJumped = value; } }
        private bool hasJumped = false;
        public bool IsFalling { get { return isFalling; } set { isFalling = value; } }
        private bool isFalling = false;
        private bool showHitbox;
        private GraphicsDevice graphicsDevice;
        private Texture2D textureHitbox;
        public bool Hit { get { return hit; } set { hit = value; } }
        private bool hit;
        public Hero(Texture2D heroTexture, KeyboardReader inputReader, GraphicsDevice graphicsDeviceIn)
        {
            this.heroTexture = heroTexture;
            this.inputReader = inputReader;
            
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 0, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 50, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 100, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 100, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 150, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 150, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 200, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 200, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 250, 50, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(50, 250, 50, 50)));

            graphicsDevice = graphicsDeviceIn;
        }

        public void LoadContent()
        {
            hitbox = new Rectangle((int)Position.X, (int)Position.Y, 50, 50);
            hitboxBody = new Rectangle((int)Position.X, (int)Position.Y, 30, 35);

            textureHitbox = new Texture2D(graphicsDevice, 1, 1);
            textureHitbox.SetData(new[] { Color.White });
        }
        public void Draw()
        {
            if(showHitbox)
            {
                //Game1._spriteBatch.Draw(textureHitbox, hitbox, Color.Yellow);

                Game1._spriteBatch.Draw(textureHitbox, hitboxBody, Color.Red);
            }

            if(isAlive)
            {
                if (!animation.goingLeft)
                {
                    Game1._spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White);
                }
                else
                {
                    Game1._spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
                }
            } else
            {
                Position = new Vector2(100, 500f);
                health = 5;
                isAlive = true;

            }

            
        }

        public void Update(GameTime gameTime)
        {
            if(hit)
            {
                LostHealth(gameTime);
            }

            CheckHealth();
            Move();
            animation.Update(gameTime, inputReader);

            
        }

        public void Move()
        {
            Vector2 direction = inputReader.ReadInput(Position, this);
            direction *= snelheid;
            Position += direction;
            hitbox.X = (int)Position.X;
            hitbox.Y = (int)Position.Y;
            
            hitboxBody.X = (int)Position.X + 10;
            hitboxBody.Y = (int)Position.Y + 13;
        }
        
        private void CheckHealth()
        {
            if(health <= 0)
            {
                isAlive = false;
                coinsLevel1 = 0;
                Position = new Vector2(100, 500);
            }
        }
        float timer = 0.3f;
        const float TIMER = 0.3f;
        public void LostHealth(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                showHitbox = false;
                hit = false;
                timer = TIMER;
            } else
            {
                showHitbox = true;
            }
        }
    }
}
