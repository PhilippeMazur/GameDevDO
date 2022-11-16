using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct3D9;
using Slime.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slime.Characters
{
    internal class Hero : IGameObject
    {
        public Texture2D heroTexture;
        IInputreader inputReader;
        public Vector2 position = new Vector2(500f, 200f);
        Animation animation = new Animation();
        private Vector2 snelheid = new Vector2(4, 4);
        public Vector2 floorTile = new Vector2(0, 540);
        public Rectangle hitbox;        
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
            spriteBatch = new SpriteBatch(graphicsDevice);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 75, 75);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(!animation.goingLeft)
            {
                spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White);
            } else
            {
                spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
            }
        }

        public void Update(GameTime gameTime, KeyboardReader kb)
        {
            Move();
            animation.Update(gameTime, kb);
        }
        private void Move()
        {
            Vector2 direction = inputReader.ReadInput(position, 100, this);
            direction *= snelheid;
            position += direction;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }
    }
}
