using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Slime.Characters;
using Slime.Collision;
using Slime.GameElements;
using Slime.Input;
using Slime.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpDX.Utilities;
using static Slime.Game1;

namespace Slime.UI
{
    public class GameSceneManager2
    {
        Texture texture = new Texture();
        List<Enemy> enemyList = new List<Enemy>();
        List<Coin> coinList = new List<Coin>();
        HeroCollisionManager heroCollisionManager = new HeroCollisionManager();
        public GameSceneManager2()
        {

        }
        public void LoadContent(GraphicsDevice graphicsDevice ,ContentManager content)
        {
            texture.LoadContent(content);
            Enemy enemy1 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(850, 604), 100);
            Enemy enemy2 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(825, 254), 150);
            enemy1.LoadContent(graphicsDevice, _spriteBatch);
            enemy2.LoadContent(graphicsDevice, _spriteBatch);
            enemyList.Add(enemy1);
            enemyList.Add(enemy2);
            Coin coin1 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(750, 595), Coin.CoinLevelType.Level1);
            coinList.Add(coin1);
            Coin coin2 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(950, 245), Coin.CoinLevelType.Level1);
            coinList.Add(coin2);
            Coin coin3 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(750, 595), Coin.CoinLevelType.Level2);
            coinList.Add(coin3);
            Coin coin4 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(950, 245), Coin.CoinLevelType.Level2);
            coinList.Add(coin4);
        }
        public void Update(GameTime gameTime, Button startButton, Hero hero, List<NextLevelDoor> doors, GameOverScreen gameOverScreen, TileMap map, KeyboardReader kb)
        {
            heroCollisionManager.Update(gameTime, map, hero, kb, doors, enemyList, coinList);
            map.Update(gameTime);
            Debug.WriteLine(map.backgroundPos);
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
        public void Draw(Hero hero, Button startButton, TileMap map, HealthBar health, List<NextLevelDoor> doors, GameOverScreen gameOverScreen)
        {
            if (currentState == GameStates.StartScreen)
            {
                Game1._spriteBatch.Draw(texture.textureDictionary[Texture.TextureType.StartScreen], new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.StartButton]);

            }

            if (currentState == GameStates.Level1)
            {
                map.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Map], texture.textureDictionary[Texture.TextureType.LevelBackground]);
                hero.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Hero]);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Health], hero);

                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level1)
                    {
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Coin]);
                    }
                }
                foreach (var item in doors)
                {
                    if (item.level == NextLevelDoor.DoorLevel.Level1)
                    {
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Door]);
                    }
                }
            }

            if (currentState == GameStates.Level2)
            {

                map.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Map], texture.textureDictionary[Texture.TextureType.LevelBackground]);
                hero.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Hero]);
                foreach (var item in enemyList)
                {
                    item.Draw(Game1._spriteBatch, hero);
                }
                health.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Health], hero);

                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level2)
                    {
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Coin]);
                    }
                }
                foreach (var item in doors)
                {
                    if (item.level == NextLevelDoor.DoorLevel.Level2)
                    {
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Door]);
                    }
                }
            }
            if (currentState == GameStates.GameOver)
            {
                gameOverScreen.Draw(texture.textureDictionary[Texture.TextureType.GameOverScreen]);
            }

        }
        private void UpdateLevel1(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
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
                    currentState = GameStates.GameOver;
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
        private void UpdateLevel2(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime);
            }
            foreach (var item in coinList)
            {
                Debug.WriteLine("inside coinlistUpdate");
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

        private void UpdateGameOver(Hero hero, List<NextLevelDoor> doors, GameTime gameTime, GameOverScreen gameOverScreen)
        {
            gameOverScreen.Update(gameTime);
            hero.health = 5;
            foreach (var item in coinList)
            {
                item.isCollected = false;
            }

            if (currentState == GameStates.GameOver)
            {
                hero.position = new Vector2(100, 500);

                foreach (var item in enemyList)
                {
                    item.isAlive = true;
                    item.Update(gameTime);
                }

            }



        }

    }
}
