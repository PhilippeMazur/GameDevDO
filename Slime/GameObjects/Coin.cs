using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using SharpDX.Direct2D1;
using Slime.Interfaces;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.GameElements
{
    public class Coin : IGameObject
    {
        private Texture2D texture;
        public bool IsCollected { get { return isCollected; } set { isCollected = value; } }
        private bool isCollected = false;
        private Vector2 position;
        public Rectangle Hitbox { get { return hitbox; } set { hitbox = value; } }
        private Rectangle hitbox;
        private Animation animation = new Animation();
        public int CoinValue { get { return coinValue; } set { coinValue = value; } }
        private int coinValue = 1;
        public enum CoinLevelType
        {
            Level1,
            Level2
        }
        public CoinLevelType level;
        public Coin(Texture2D texturein, Vector2 positionin, CoinLevelType levelin)
        {
            this.texture= texturein;
            this.position = positionin;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);

            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(16, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(32, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(48, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(80, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(96, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(112, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(144, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(160, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(176, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(208, 0, 16, 16)));
            animation.AddFrame(new AnimationFrame(new Rectangle(224, 0, 16, 16)));
            this.level = levelin;
        }
        public void Draw()
        {
            if(!isCollected)
            {
                Game1._spriteBatch.Draw(texture, position, animation.CurrentFrame.sourceRectangle, Color.White, 0, new Vector2(0, 0), new Vector2(3, 3), SpriteEffects.None, 0);
            }
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime, 20);
        }
    }
}
