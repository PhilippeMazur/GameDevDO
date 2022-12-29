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
        private Texture texture = new Texture();
        private Hero hero;
        private KeyboardReader kb = new KeyboardReader();
        private List<Enemy> enemyList = new List<Enemy>();
        private List<Coin> coinList = new List<Coin>();
        private HeroCollisionManager2 heroCollisionManager = new HeroCollisionManager2();
        private List<NextLevelDoor> doors = new List<NextLevelDoor>();
        private HealthBar health;
        private TileMap map;
        private Score scoreUI;
        private GameKeyHandler startButton;
        private Screen startMenuScreen;
        private Screen winningScreen;
        private Screen gameOverScreen;



        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevicein)
        {

            texture.LoadContent(content);
            scoreUI = new Score(texture.fontDictionary[Texture.TextureType.Font]);
            map = new TileMap(texture.textureDictionary[Texture.TextureType.LevelBackground], texture.textureDictionary[Texture.TextureType.Map]);
            hero = hero = new Hero(texture.textureDictionary[Texture.TextureType.Hero], kb, graphicsDevicein);
            hero.LoadContent();
            health = new HealthBar(texture.textureDictionary[Texture.TextureType.Health]);
            startButton = new GameKeyHandler(new Rectangle(0, 0, 500, 20), new Vector2(250, 450));
            generateScreens();
            generateEnemies();
            generateCoins();
            generateDoors();
        }

        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            heroCollisionManager.Update(map, hero, kb, doors, enemyList, coinList, scoreUI);
            map.Update(gameTime);
            startButton.Update(gameTime, hero);
            if (currentState == GameStates.StartScreen)
            {
                startMenuScreen.Update(gameTime);
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
                UpdateGameOver(gameTime);
            }
            if(currentState == GameStates.WinningScreen)
            {
                winningScreen.Update(gameTime);
                ResetObjects(gameTime);
            }
        }
        public void Draw()
        {
            if (currentState == GameStates.StartScreen)
            {
                startMenuScreen.Draw(texture.fontDictionary[Texture.TextureType.Font]);
            }

            if (currentState == GameStates.Level1)
            {
                map.Draw();
                hero.Draw();

                foreach (var item in enemyList)
                {
                    if(item.level == Enemy.Level.Level1)
                    {
                        item.Draw();
                    }
                }
                health.Draw(hero);

                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level1)
                    {
                        item.Draw();
                    }
                }
                foreach (var item in doors)
                {
                    if (item.Level == NextLevelDoor.DoorLevel.Level1)
                    {
                        item.Draw();
                    }
                }
                scoreUI.Draw();
                _spriteBatch.Draw(texture.textureDictionary[Texture.TextureType.AbilityBar], new Rectangle(0, 700, 1000, 80), Color.White);
            }

            if (currentState == GameStates.Level2)
            {
                map.Draw();
                hero.Draw();
                foreach (var item in enemyList)
                {
                    if (item.level == Enemy.Level.Level2)
                    {
                        item.Draw();
                    }
                }
                health.Draw(hero);

                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level2)
                    {
                        item.Draw();
                    }
                }
                foreach (var item in doors)
                {
                    if (item.Level == NextLevelDoor.DoorLevel.Level2)
                    {
                        item.Draw();
                    }
                }
                scoreUI.Draw();
            }
            if (currentState == GameStates.GameOver)
            {
                gameOverScreen.Draw();
            }
            if (currentState == GameStates.WinningScreen)
            {
                winningScreen.Draw();
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
                    item.Update(gameTime);

                }
            }
            foreach (var item in doors)
            {
                if (item.Level == NextLevelDoor.DoorLevel.Level1)
                {
                    item.Update(gameTime);
                }
            }

            if (currentState == GameStates.Level1)
            {
                if (hero.Health <= 0)
                {
                    currentState = GameStates.GameOver;
                    hero.Health = 5;
                    foreach (var item in coinList)
                    {
                        if (item.level == Coin.CoinLevelType.Level1)
                        {
                            item.IsCollected = false;
                            item.CoinValue = 1;
                        }
                    }

                }
                else
                {
                    foreach (var item in doors)
                    {
                        item.CheckPlayerCoins();
                    }
                }
                if (currentState == GameStates.StartScreen)
                {

                    foreach (var item in enemyList)
                    {
                        item.IsAlive = true;
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
                if (item.level == Coin.CoinLevelType.Level2)
                {
                    item.Update(gameTime);
                }
            }
            foreach (var item in doors)
            {
                if (item.Level == DoorLevel.Level2)
                {
                    item.Update(gameTime);
                }
            }
            foreach (var item in doors)
            {
                item.Update(gameTime);
            }

            if (currentState == GameStates.Level1 || currentState == GameStates.Level2)
            {
                if (hero.Health <= 0)
                {
                    currentState = GameStates.GameOver;
                    hero.Health = 5;

                    foreach (var item in coinList)
                    {
                        item.IsCollected = false;
                        item.CoinValue = 1;
                    }
                }
                else
                {
                    foreach (var item in doors)
                    {
                        item.CheckPlayerCoins();
                    }
                }
                if (currentState == GameStates.StartScreen)
                {
                    foreach (var item in enemyList)
                    {
                        item.IsAlive = true;
                        item.Update(gameTime);
                    }
                }
            }
        }
        private void UpdateGameOver(GameTime gameTime)
        {
            gameOverScreen.Update(gameTime);
            hero.Health = 5;
            foreach (var item in coinList)
            {
                item.IsCollected = false;
            }

            if (currentState == GameStates.GameOver)
            {
                hero.Position = new Vector2(100, 500);

                foreach (var item in enemyList)
                {
                    item.IsAlive = true;
                    item.Update(gameTime);
                }
            }
        }
        private void ResetObjects(GameTime gameTime)
        {
            hero.Health = 5;
            foreach (var item in coinList)
            {
                item.IsCollected = false;
            }
            hero.Position = new Vector2(100, 500);

            foreach (var item in enemyList)
            {
                item.IsAlive = true;
                item.Update(gameTime);
            }
        }
        private void generateScreens()
        {
            winningScreen = new Screen(texture.textureDictionary[Texture.TextureType.WinningScreen], new Rectangle(0, 0, 1000, 700), new Project1.Animations.Animation(), new Text("", new Vector2(0, 0), texture.fontDictionary[Texture.TextureType.Font]));
            winningScreen.Animation.AddFrame(new Project1.Animations.AnimationFrame(new Rectangle(0, 0, 1000, 700)));
            winningScreen.Animation.AddFrame(new Project1.Animations.AnimationFrame(new Rectangle(1000, 0, 1000, 700)));

            gameOverScreen = new Screen(texture.textureDictionary[Texture.TextureType.GameOverScreen], new Rectangle(0, 0, 1000, 700), new Project1.Animations.Animation(), new Text("", new Vector2(0, 0), texture.fontDictionary[Texture.TextureType.Font]));
            gameOverScreen.Animation.AddFrame(new Project1.Animations.AnimationFrame(new Rectangle(0, 0, 1000, 700)));
            gameOverScreen.Animation.AddFrame(new Project1.Animations.AnimationFrame(new Rectangle(1000, 0, 1000, 700)));

            startMenuScreen = new Screen(texture.textureDictionary[Texture.TextureType.StartScreen], new Rectangle(0, 0, 1000, 700), new Project1.Animations.Animation(), new Text("Press 'ENTER' to Start !", new Vector2(200, 450), texture.fontDictionary[Texture.TextureType.Font]));
            startMenuScreen.Animation.AddFrame(new Project1.Animations.AnimationFrame(new Rectangle(0, 0, 1000, 700)));
        }
        private void generateDoors()
        {
            NextLevelDoor doorLevel1 = new NextLevelDoor(new Vector2(300, 150), DoorLevel.Level1, texture.textureDictionary[Texture.TextureType.Door], hero, texture.fontDictionary[Texture.TextureType.Font]);
            doors.Add(doorLevel1);
            NextLevelDoor doorLevel2 = new NextLevelDoor(new Vector2(800, 50), DoorLevel.Level2, texture.textureDictionary[Texture.TextureType.Door], hero, texture.fontDictionary[Texture.TextureType.Font]);
            doors.Add(doorLevel2);
        }
        private void generateCoins()
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
        private void generateEnemies()
        {
            Enemy groundEnemy1 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(850, 604), 100, Enemy.Type.Ground, Enemy.Level.Level1, hero);
            Enemy groundEnemy2 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(825, 204), 150, Enemy.Type.Ground, Enemy.Level.Level1, hero);
            Enemy enemy3 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy2], new Vector2(500, 204), 100, Enemy.Type.Air, Enemy.Level.Level1, hero);
            groundEnemy1.LoadContent();
            groundEnemy2.LoadContent();
            enemy3.LoadContent();
            enemyList.Add(groundEnemy1);
            enemyList.Add(groundEnemy2);
            enemyList.Add(enemy3);
            Enemy groundEnemy3 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(275, 300), 75, Enemy.Type.Ground, Enemy.Level.Level2, hero);
            groundEnemy3.LoadContent();
            enemyList.Add(groundEnemy3);
            Enemy groundEnemy4 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(650, 300), 125, Enemy.Type.Ground, Enemy.Level.Level2, hero);
            groundEnemy4.LoadContent();
            enemyList.Add(groundEnemy4);
            Enemy groundEnemy5 = new Enemy(texture.textureDictionary[Texture.TextureType.Enemy], new Vector2(200, 50), 40, Enemy.Type.Ground, Enemy.Level.Level2, hero);
            groundEnemy5.LoadContent();
            enemyList.Add(groundEnemy5);

        }
        
        
    }
}
