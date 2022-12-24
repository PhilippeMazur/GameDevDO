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
using static Slime.GameElements.NextLevelDoor;

namespace Slime.UI
{
    public class GameSceneManager2
    {
        public Texture texture = new Texture();

        public Hero hero;
        KeyboardReader kb = new KeyboardReader();

        List<Enemy> enemyList = new List<Enemy>();
        List<Coin> coinList = new List<Coin>();
        HeroCollisionManager heroCollisionManager = new HeroCollisionManager();
        List<NextLevelDoor> doors = new List<NextLevelDoor>();
        HealthBar health = new HealthBar();
        TileMap map = new TileMap();
        Score scoreUI = new Score();
        //Ability abilityJump = new Ability(new Vector2(450, 700));
        public void LoadContent(GraphicsDevice graphicsDevice ,ContentManager content, SpriteBatch spriteBatch)
        {

            texture.LoadContent(content);
            hero = hero = new Hero(texture.textureDictionary[Texture.TextureType.Hero], kb);
            hero.LoadContent();
            generateEnemies();
            generateCoins();
            generateDoors();
        }
        public void generateDoors()
        {
            NextLevelDoor doorLevel1 = new NextLevelDoor(new Vector2(300, 150), DoorLevel.Level1);
            doors.Add(doorLevel1);
            NextLevelDoor doorLevel2 = new NextLevelDoor(new Vector2(800, 50), DoorLevel.Level2);
            doors.Add(doorLevel2);
        }
        public void generateCoins()
        {
            Coin coin1 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(750, 595), Coin.CoinLevelType.Level1);
            coinList.Add(coin1);
            Coin coin2 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(950, 195), Coin.CoinLevelType.Level1);
            coinList.Add(coin2);
            Coin coin4 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(250, 500), Coin.CoinLevelType.Level2);
            coinList.Add(coin4);
            Coin coin5 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(400, 500), Coin.CoinLevelType.Level2);
            coinList.Add(coin5);
            Coin coin6 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(550, 500), Coin.CoinLevelType.Level2);
            coinList.Add(coin6);
            Coin coin7 = new Coin(texture.textureDictionary[Texture.TextureType.Coin], new Vector2(700, 500), Coin.CoinLevelType.Level2);
            coinList.Add(coin7);
        }
        public void generateEnemies()
        {
            Enemy groundEnemy1 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(850, 604), 100, Enemy.Type.Ground, Enemy.Level.Level1);
            Enemy groundEnemy2 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(825, 204), 150, Enemy.Type.Ground, Enemy.Level.Level1);
            Enemy enemy3 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(500, 204), 100, Enemy.Type.Air, Enemy.Level.Level1);
            groundEnemy1.LoadContent();
            groundEnemy2.LoadContent();
            enemy3.LoadContent();
            enemyList.Add(groundEnemy1);
            enemyList.Add(groundEnemy2);
            enemyList.Add(enemy3);
            Enemy groundEnemy3 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(275, 300), 75, Enemy.Type.Ground, Enemy.Level.Level2);
            groundEnemy3.LoadContent();
            enemyList.Add(groundEnemy3);
            Enemy groundEnemy4 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(650, 300), 125, Enemy.Type.Ground, Enemy.Level.Level2);
            groundEnemy4.LoadContent();
            enemyList.Add(groundEnemy4);
            Enemy groundEnemy5 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(200, 50), 40, Enemy.Type.Ground, Enemy.Level.Level2);
            groundEnemy5.LoadContent();
            enemyList.Add(groundEnemy5);
            //Enemy enemy4 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(250, 50), 100, Enemy.Type.Air, Enemy.Level.Level2);
            //Enemy enemy5 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(400, 50), 150, Enemy.Type.Air, Enemy.Level.Level2);
            //Enemy enemy6 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(550, 50), 100, Enemy.Type.Air, Enemy.Level.Level2);
            //Enemy enemy7 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(700, 50), 100, Enemy.Type.Air, Enemy.Level.Level2);
            //enemy4.LoadContent();
            //enemy5.LoadContent();
            //enemy6.LoadContent();
            //enemy7.LoadContent();
            //enemyList.Add(enemy4);
            //enemyList.Add(enemy5);
            //enemyList.Add(enemy6);
            //enemyList.Add(enemy7);

        }
        public void Update(GameTime gameTime, Button startButton, GameOverScreen gameOverScreen, WinningScreen winningScreen)
        {
            hero.Update(gameTime, kb);
            heroCollisionManager.Update(gameTime, map, hero, kb, doors, enemyList, coinList, scoreUI);
            map.Update(gameTime);
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
                UpdateGameOver(gameOverScreen, gameTime);
            }
            if(currentState == GameStates.WinningScreen)
            {
                UpdateWinningScreen(winningScreen, gameTime);
            }
        }
        public void Draw(Button startButton, GameOverScreen gameOverScreen)
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
                    if(item.level == Enemy.Level.Level1)
                    {
                        item.Draw(Game1._spriteBatch, hero);
                    }
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
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Door], this, hero);
                    }
                }
                scoreUI.Draw(Game1._spriteBatch, texture.fontDictionary[Texture.TextureType.Font]);
                _spriteBatch.Draw(texture.textureDictionary[Texture.TextureType.AbilityBar], new Rectangle(0, 700, 1000, 80), Color.White);

                //abilityJump.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.AbilityJump]);

            }

            if (currentState == GameStates.Level2)
            {
                map.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Map], texture.textureDictionary[Texture.TextureType.LevelBackground]);
                hero.Draw(Game1._spriteBatch, texture.textureDictionary[Texture.TextureType.Hero]);
                foreach (var item in enemyList)
                {
                    if (item.level == Enemy.Level.Level2)
                    {
                        item.Draw(Game1._spriteBatch, hero);
                    }
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
                        item.Draw(_spriteBatch, texture.textureDictionary[Texture.TextureType.Door], this, hero);
                    }
                }
                scoreUI.Draw(Game1._spriteBatch, texture.fontDictionary[Texture.TextureType.Font]);
            }
            if (currentState == GameStates.GameOver)
            {
                gameOverScreen.Draw(texture.textureDictionary[Texture.TextureType.GameOverScreen]);
            }
            if(currentState == GameStates.WinningScreen)
            {
                gameOverScreen.Draw(texture.textureDictionary[Texture.TextureType.WinningScreen]);
            }
        }
        private void UpdateLevel1(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime, hero);
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
                        item.Update(gameTime, hero);
                    }
                }
            }
        }
        private void UpdateLevel2(Hero hero, List<NextLevelDoor> doors, GameTime gameTime)
        {
            foreach (var item in enemyList)
            {
                item.Update(gameTime, hero);
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
                        item.Update(gameTime, hero);
                    }
                }
            }
        }

        private void UpdateGameOver(GameOverScreen gameOverScreen, GameTime gameTime)
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
                    item.Update(gameTime, hero);
                }
            }
        }
        private void UpdateWinningScreen(WinningScreen winningScreen, GameTime gameTime)
        {
            winningScreen.Update(gameTime);
            hero.health = 5;
            foreach (var item in coinList)
            {
                item.isCollected = false;
            }
            hero.position = new Vector2(100, 500);

            foreach (var item in enemyList)
            {
                item.isAlive = true;
                item.Update(gameTime, hero);
            }
        }
    }
}
