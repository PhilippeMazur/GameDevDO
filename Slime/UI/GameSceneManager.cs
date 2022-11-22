using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime.UI
{
      static class GameSceneManager
    {

        public static void Update(GameTime gameTime ,Button startButton, Hero hero, Enemy enemy)
        {
            if(currentState == GameStates.StartScreen)
            {
                startButton.Update(gameTime);
            }
            if(currentState == GameStates.Level1)
            {
                UpdateLevel1(hero, enemy);
            }
        }
        public static void Draw(Hero hero, Enemy enemy1, Button startButton, TileMap map, HealthBar health)
        {
            if(currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.Draw(Game1._backgroundTexture, new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(Game1._spriteBatch);

            } else if (currentState == GameStates.Level1)
            {
                map.Draw(Game1._spriteBatch, Game1._mapTexture);
                hero.Draw(Game1._spriteBatch);
                enemy1.Draw(Game1._spriteBatch, hero);
                health.Draw(Game1._spriteBatch, Game1._healthTexture, hero);
            }
        }
        public static void UpdateLevel1(Hero hero, Enemy enemy)
        {
            if(currentState == GameStates.Level1)
            {
                if (hero.health <= 0)
                {
                    currentState = GameStates.StartScreen;
                    hero.health = 5;
                }
                if (currentState == GameStates.StartScreen)
                {

                    enemy.isAlive = true;

                }
            }
        }
    }
}
