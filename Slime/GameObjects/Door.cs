using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.Interfaces;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Texture = Slime.UI.Texture;

namespace Slime.GameElements
{
    public class Door : IGameObject
    {
        private Texture2D texture;
        private SpriteFont font;
        public bool IsOpened { get { return isOpened; } set { isOpened = value; } }
        private bool isOpened = false;
        private Vector2 position;
        private Vector2 positionText;
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }
        private Rectangle hitbox;
        private Animation animation = new Animation();
        private Hero hero;
        public enum DoorLevel
        {
            Level1,
            Level2
        }
        public DoorLevel Level { get { return level; } set { level = value; } }
        private DoorLevel level;
        public Door(Vector2 position, DoorLevel level, Texture2D texturein, Hero heroin, SpriteFont fontin)
        {
            texture = texturein;
            this.position = position;
            this.positionText = new Vector2((float)position.X + 6, (float)position.Y - 30);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 51, 50);
            hero = heroin;
            font = fontin;
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(51, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(102, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(153, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(204, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(255, 0, 51, 50)));
            this.level = level;
        }
        public void Draw()
        {
            if(Game1.currentState == Game1.GameStates.Level1 && hero.CoinsLevel1 < 2)
            {
                Game1._spriteBatch.DrawString(font, $"{hero.CoinsLevel1} / 2", positionText, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            if (Game1.currentState == Game1.GameStates.Level2 && hero.CoinsLevel2 < 4)
            {
                Game1._spriteBatch.DrawString(font, $"{hero.CoinsLevel2} / 4", positionText, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            Game1._spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            CheckPlayerCoins();
            animation.Update(gameTime, this);
        }
        public void CheckPlayerCoins()
        {
            if(Game1.currentState == Game1.GameStates.Level1 && level == DoorLevel.Level1)
            {
                if (hero.CoinsLevel1 >= 2)
                {
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
            }
            if(Game1.currentState == Game1.GameStates.Level2 && level == DoorLevel.Level2)
            {
                if (hero.CoinsLevel2 >= 4)
                {
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
            }
        }
    }
}
