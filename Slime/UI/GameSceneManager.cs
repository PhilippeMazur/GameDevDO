using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static List<Enemy> enemyList = new List<Enemy>();
        public static List<Coin> coinList = new List<Coin>();
        public static Texture2D _coinTexture;
        public static Texture2D _levelBackground;
        public static Texture2D _heroTexture;
        public static Texture2D _mapTexture;


        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
            Enemy enemy1 = new Enemy(_enemyTexture, new Vector2(850, 604), 100);
            Enemy enemy2 = new Enemy(_enemyTexture, new Vector2(825, 254), 150);
            enemy1.LoadContent(graphicsDevice, _spriteBatch);
            enemy2.LoadContent(graphicsDevice, _spriteBatch);
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);
            Coin coin1 = new Coin(_coinTexture, new Vector2(750, 595), Coin.CoinLevelType.Level1);
            coinList.Add(coin1);
            Coin coin2 = new Coin(_coinTexture, new Vector2(950, 245), Coin.CoinLevelType.Level1);
            coinList.Add(coin2);
            Coin coin3 = new Coin(_coinTexture, new Vector2(750, 595), Coin.CoinLevelType.Level2);
            coinList.Add(coin3);
            Coin coin4 = new Coin(_coinTexture, new Vector2(950, 245), Coin.CoinLevelType.Level2);
            coinList.Add(coin4);
        }

        public static void Update(GameTime gameTime, Button startButton, Hero hero, List<NextLevelDoor> doors, GameOverScreen gameOverScreen)
        {
            if (currentState == GameStates.StartScreen)
            {
                startButton.Update(gameTime, hero);
            }
            if (currentState == GameStates.Level1)
            {
                UpdateLevel1(hero, doors, gameTime);
            }
            if (currentState == GameStates.Level2)
            {
                UpdateLevel2(hero, doors, gameTime);
            }
            if (currentState == GameStates.GameOver)
            {
                UpdateGameOver(hero, doors, gameTime, gameOverScreen);
            }
        }
        public static void Draw(Hero hero, Button startButton, TileMap map, HealthBar health, List<NextLevelDoor> doors, GameOverScreen gameOverScreen)
        {
            if (currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.Draw(Game1._backgroundTexture, new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(Game1._spriteBatch);

            }

            if (currentState == GameStates.Level1)
            {
                map.Draw(Game1._spriteBatch, _mapTexture);
                hero.Draw(Game1._spriteBatch);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, Game1._healthTexture, hero);

                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level1)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
                foreach (var item in doors)
                {
                    if (item.level == NextLevelDoor.DoorLevel.Level1)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
            }

            if (currentState == GameStates.Level2)
            {

                map.Draw(Game1._spriteBatch, _mapTexture);
                hero.Draw(Game1._spriteBatch);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, Game1._healthTexture, hero);

                foreach (var item in GameSceneManager.coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level2)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
                foreach (var item in doors)
                {
                    if (item.level == NextLevelDoor.DoorLevel.Level2)
                    {
                        item.Draw(_spriteBatch);
                    }
                }
            }
            if (currentState == GameStates.GameOver)
            {
                gameOverScreen.Draw();
            }

        }
        public static void UpdateLevel1(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime);
            }
            foreach (var item in coinList)
            {
                if (item.level == Coin.CoinLevelType.Level1)
                {
                    item.update(gameTime);

                }
            }
            foreach (var item in doors)
            {
                if (item.level == NextLevelDoor.DoorLevel.Level1)
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
                    foreach (var item in coinList)
                    {
                        if (item.level == Coin.CoinLevelType.Level1)
                        {
                            item.isCollected = false;
                            item.value = 1;
                        }

                    }

                }
                else
                {
                    foreach (var item in doors)
                    {
                        item.CheckPlayerCoins(hero);
                    }
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
        public static void UpdateLevel2(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime);
            }
            foreach (var item in coinList)
            {
                if (item.level == Coin.CoinLevelType.Level2)
                {
                    item.update(gameTime);
                }
            }
            foreach (var item in doors)
            {
                if (item.level == NextLevelDoor.DoorLevel.Level2)
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
                    currentState = GameStates.GameOver;
                    hero.health = 5;

                    foreach (var item in coinList)
                    {
                        item.isCollected = false;
                        item.value = 1;


                    }

                }
                else
                {
                    foreach (var item in doors)
                    {
                        item.CheckPlayerCoins(hero);
                    }
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

        public static void UpdateGameOver(Hero hero, List<NextLevelDoor> doors, GameTime gameTime, GameOverScreen gameOverScreen)
        {
            gameOverScreen.Update(gameTime);
            hero.health = 5;
            foreach (var item in coinList)
            {
                item.isCollected = false;
            }
 
            if (currentState == GameStates.GameOver)
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
