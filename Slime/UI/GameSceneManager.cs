using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.UI
{
    internal class GameSceneManager
    {
        private Texture2D _backgroundTexture;
        private Button startButton;
        private SpriteBatch spriteBatch;
        private TileMap map;
        private Texture2D _mapTexture;
        Hero hero;
        Enemy enemy1;
        HealthBar health;
        private Texture2D _healthTexture;
        

        public GameSceneManager(Texture2D backgroundTexture, Button startButton, SpriteBatch spriteBatch, TileMap map, Texture2D mapTexture, Hero hero, Enemy enemy, HealthBar health, Texture2D healthTexture)
        {
            this._backgroundTexture = backgroundTexture;
            this.startButton = startButton;
            this.spriteBatch = spriteBatch;
            this.hero = hero;
            this.enemy1 = enemy;
            this.health = health;
            this._healthTexture = healthTexture;
            this.map = map;

        }
        public void draw()
        {
            if (currentState == GameStates.StartScreen)
            {
                spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(spriteBatch);

            }
            else if (currentState == GameStates.Level1)
            {
                map.Draw(spriteBatch, _mapTexture);
                hero.Draw(spriteBatch);
                enemy1.Draw(spriteBatch);
                health.Draw(spriteBatch, _healthTexture, hero);
            }
        }
    }
}
