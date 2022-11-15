using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct3D9;
using Slime.Input;
using Slime.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Characters
{
    internal class Hero : IGameObject
    {
        Texture2D heroTexture;
        IInputreader inputReader;
        private Vector2 position = new Vector2(500f, 200f);
        Animation animationIdle = new Animation();
        Animation animationRunning = new Animation();
        Animation animationJumping = new Animation();

        public Hero(Texture2D heroTexture, IInputreader inputReader)
        {
            this.heroTexture = heroTexture;
            this.inputReader = inputReader;
            animationIdle.AddFrame(new AnimationFrame(new Rectangle(0, 0, 50, 50)));
            animationIdle.AddFrame(new AnimationFrame(new Rectangle(50, 0, 50, 50)));
            animationRunning.AddFrame(new AnimationFrame(new Rectangle(0, 50, 50, 50)));
            animationRunning.AddFrame(new AnimationFrame(new Rectangle(50, 50, 50, 50)));
            animationJumping.AddFrame(new AnimationFrame(new Rectangle(0, 100, 50, 50)));
            animationJumping.AddFrame(new AnimationFrame(new Rectangle(50, 100, 50, 50)));


        }
        public void LoadContent(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, position, animationIdle.CurrentFrame.sourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime, KeyboardReader kb)
        {
            if(kb.AnimationState == KeyboardReader.states.Idle)
            {
                animationIdle.Update(gameTime);
            }
            if (kb.AnimationState == KeyboardReader.states.RunningRight)
            {
                animationRunning.Update(gameTime);
                
            }
        }
    }
}
