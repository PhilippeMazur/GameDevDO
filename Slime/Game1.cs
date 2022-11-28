using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.Collision;
using Slime.GameElements;
using Slime.Input;
using Slime.Map;
using Slime.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Slime
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch _spriteBatch;
        Hero hero;
        TileMap map = new TileMap();
        KeyboardReader kb = new KeyboardReader();
        HeroCollisionManager heroCollisionManager = new HeroCollisionManager();
        private List<Enemy> enemyList = new List<Enemy>();
        Enemy enemy1;
        Enemy enemy2;
        public static Texture2D _enemyTexture;
        public static Texture2D _healthTexture;
        private HealthBar health;
        public enum GameStates
        {
            StartScreen,
            Level1,
            Level2,
            WinningScreen,
            GameOver
        }
        public static GameStates currentState;
        public static Texture2D _backgroundTexture;
        private static Texture2D _startButton;
        private Button startButton;


        public static Texture2D _doorTexture;
        private NextLevelDoor doorLevel1;
        List<NextLevelDoor> doors = new List<NextLevelDoor>();
        TileMap map2 = new TileMap();

        public static Texture2D _gameOverScreenTexture;
        private GameOverScreen gameOverScreen;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _enemyTexture = Content.Load<Texture2D>("SlimeEnemy");

            GameSceneManager.LoadContent(GraphicsDevice);

            GameSceneManager._heroTexture = Content.Load<Texture2D>("SlimeHero");
            hero = new Hero(GameSceneManager._heroTexture, kb);
            hero.LoadContent(GraphicsDevice, _spriteBatch);
            GameSceneManager._mapTexture = Content.Load<Texture2D>("MapTiles");
            health = new HealthBar();
            _healthTexture = Content.Load<Texture2D>("HealthHeart");
            _backgroundTexture = Content.Load<Texture2D>("BackgroundWithLogo");
            _startButton = Content.Load<Texture2D>("PressStart");
            startButton = new Button(new Rectangle(0, 0, 500, 20), new Vector2(250, 450), _startButton);
            GameSceneManager._levelBackground = Content.Load<Texture2D>("LevelBackground");
            currentState = GameStates.StartScreen;
            GameSceneManager._coinTexture = Content.Load<Texture2D>("CoinSpritesheet");
            _doorTexture = Content.Load<Texture2D>("PortalDoor");
            doorLevel1 = new NextLevelDoor(new Vector2(300, 150), NextLevelDoor.DoorLevel.Level1);
            doors.Add(doorLevel1);
            _gameOverScreenTexture = Content.Load<Texture2D>("GameOverScreenAnimated");
            gameOverScreen = new GameOverScreen();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            startButton.Update(gameTime, hero);
            heroCollisionManager.Update(gameTime, map, hero, kb, doors);
            hero.Update(gameTime, kb);
            GameSceneManager.Update(gameTime, startButton, hero, doors, gameOverScreen);
            base.Update(gameTime);           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            GameSceneManager.Draw(hero, startButton, map, health, doors, gameOverScreen);
            base.Draw(gameTime);
            _spriteBatch.End(); 
            
        }
    }
}