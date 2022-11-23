using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.GameElements;
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

        public static void Update(GameTime gameTime ,Button startButton, Hero hero, List<Enemy> enemyList, List<Coin> coinList, List<NextLevelDoor> doors)
        {
            if(currentState == GameStates.StartScreen)
            {
                startButton.Update(gameTime);
            }
            if(currentState == GameStates.Level1)
            {
                UpdateLevel1(hero, enemyList, coinList, doors, gameTime);
            }
            if(currentState == GameStates.Level2)
            {
                UpdateLevel2(hero, enemyList, coinList, doors, gameTime);
            }
        }
        public static void Draw(Hero hero, List<Enemy> enemyList, Button startButton, TileMap map, HealthBar health, List<Coin> coins, List<NextLevelDoor> doors)
        {
            if(currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.Draw(Game1._backgroundTexture, new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(Game1._spriteBatch);

            }

            if (currentState == GameStates.Level1)
            {
                map.Draw(Game1._spriteBatch, Game1._mapTexture);
                hero.Draw(Game1._spriteBatch);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, Game1._healthTexture, hero);

                foreach (var item in coins)
                {
                    item.Draw(_spriteBatch);
                }
                foreach (var item in doors)
                {
                    if(item.level == NextLevelDoor.DoorLevel.Level1)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
            }
            
            if(currentState == GameStates.Level2)
            {
                
                map.Draw(Game1._spriteBatch, Game1._mapTexture);
                hero.Draw(Game1._spriteBatch);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, Game1._healthTexture, hero);

                foreach (var item in coins)
                {
                    item.Draw(_spriteBatch);
                }
                foreach (var item in doors)
                {
                    if(item.level == NextLevelDoor.DoorLevel.Level2)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
            }

        }
        public static void UpdateLevel1(Hero hero, List<Enemy> enemyList, List<Coin> coinList, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime);
            }
            foreach (var item in coinList)
            {
                item.update(gameTime);
            }
            foreach (var item in doors)
            {
                if(item.level == NextLevelDoor.DoorLevel.Level1)
                {
                    item.Update(gameTime, hero);
                }
            }

            if (currentState == GameStates.Level1)
            {
                if (hero.health <= 0)
                {
                    currentState = GameStates.StartScreen;
                    hero.health = 5;
                }
                if (currentState == GameStates.StartScreen)
                {

                    foreach (var item in enemyList)
                    {
                        item.isAlive = true;
                        item.Update(gameTime);
                    }

                }
            }
        }
        public static void UpdateLevel2(Hero hero, List<Enemy> enemyList, List<Coin> coinList, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime);
            }
            foreach (var item in coinList)
            {
                item.update(gameTime);
            }
            foreach (var item in doors)
            {
                if(item.level == NextLevelDoor.DoorLevel.Level2)
                {
                    item.Update(gameTime, hero);

                }
            }
            foreach (var item in doors)
            {
                item.Update(gameTime, hero);
            }

            if (currentState == GameStates.Level1 || currentState == GameStates.Level2)
            {
                if (hero.health <= 0)
                {
                    currentState = GameStates.StartScreen;
                    hero.health = 5;
                }
                if (currentState == GameStates.StartScreen)
                {

                    foreach (var item in enemyList)
                    {
                        item.isAlive = true;
                        item.Update(gameTime);
                    }

                }
            }
        }

    }
}
