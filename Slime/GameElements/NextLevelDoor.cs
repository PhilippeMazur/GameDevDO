using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using Slime.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.GameElements
{
    internal class NextLevelDoor
    {
        public bool isOpened = false;
        public Vector2 position;
        public Rectangle hitbox;
        public Animation animation = new Animation();
        public enum DoorLevel
        {
            Level1,
            Level2
        }
        public enum AnimationState
        {
            Closed,
            Open
        }
        public DoorLevel level;
        public AnimationState state = AnimationState.Closed;

        public NextLevelDoor(Vector2 position, DoorLevel level)
        {
            this.position = position;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 51, 50);

            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(51, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(102, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(153, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(204, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(255, 0, 51, 50)));
            this.level = level;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1._doorTexture, position, animation.CurrentFrame.sourceRectangle, Color.White);

        }
        public void Update(GameTime gameTime, Hero hero)
        {
            CheckPlayerCoins(hero);
            animation.Update(gameTime, this);
        }

        public void CheckPlayerCoins(Hero hero)
        {
            if(hero.coinsLevel1 >= 2)
            {
                isOpened = true;
                //state = AnimationState.Open;
            } else
            {
                isOpened = false;
            }
        }
    }
}
