using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct2D1;
using Slime.Characters;
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
    public class NextLevelDoor
    {
        Texture2D texture;
        public bool isOpened = false;
        public Vector2 position;
        public Vector2 positionText;
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

        public NextLevelDoor(Vector2 position, DoorLevel level, Texture2D texturein)
        {
            texture = texturein;
            this.position = position;
            this.positionText = new Vector2((float)position.X + 6, (float)position.Y - 30);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 51, 50);

            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(51, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(102, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(153, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(204, 0, 51, 50)));
            animation.AddFrame(new AnimationFrame(new Rectangle(255, 0, 51, 50)));
            this.level = level;
        }

        public void Draw(GameSceneManager2 gameSceneManager2, Hero hero)
        {
            if(Game1.currentState == Game1.GameStates.Level1 && hero.coinsLevel1 < 2)
            {
                Game1._spriteBatch.DrawString(gameSceneManager2.texture.fontDictionary[Texture.TextureType.Font], $"{hero.coinsLevel1} / 2", positionText, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            if (Game1.currentState == Game1.GameStates.Level2 && hero.coinsLevel2 < 4)
            {
                Game1._spriteBatch.DrawString(gameSceneManager2.texture.fontDictionary[Texture.TextureType.Font], $"{hero.coinsLevel2} / 4", positionText, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
            Game1._spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White);

        }
        public void Update(GameTime gameTime, Hero hero)
        {
            CheckPlayerCoins(hero);
            animation.Update(gameTime, this);
        }

        public void CheckPlayerCoins(Hero hero)
        {
            if(Game1.currentState == Game1.GameStates.Level1 && level == DoorLevel.Level1)
            {
                if (hero.coinsLevel1 >= 2)
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
                if (hero.coinsLevel2 >= 4)
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
