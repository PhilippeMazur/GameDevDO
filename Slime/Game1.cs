using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using Slime.Characters;
using Slime.Collision;
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
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _heroTexture;
        private Texture2D _mapTexture;
        Hero hero;
        TileMap map = new TileMap();
        KeyboardReader kb = new KeyboardReader();
        private int touchCounter = 0;
        HeroCollisionManager heroCollisionManager = new HeroCollisionManager();
        private List<Enemy> enemyList = new List<Enemy>();
        Enemy enemy1;
        public Texture2D _enemyTexture;
        public Texture2D _healthTexture;
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
        public Texture2D _backgroundTexture;

        private Texture2D _startButton;
        private Button startButton;

        public static Texture2D _levelBackground;
        //private GameSceneManager sceneManager;

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
            _heroTexture = Content.Load<Texture2D>("SlimeHero");
            hero = new Hero(_heroTexture, kb);
            _enemyTexture = Content.Load<Texture2D>("SlimeEnemy");
            enemy1 = new Enemy(_enemyTexture, new Vector2(500, 604));
            enemyList.Add(enemy1);
            enemy1.LoadContent(GraphicsDevice, _spriteBatch);
            hero.LoadContent(GraphicsDevice, _spriteBatch);
            _mapTexture = Content.Load<Texture2D>("MapTiles");
            health = new HealthBar();
            _healthTexture = Content.Load<Texture2D>("HealthHeart");
            _backgroundTexture = Content.Load<Texture2D>("BackgroundWithLogo");
            _startButton = Content.Load<Texture2D>("PressStart");
            startButton = new Button(new Rectangle(0, 0, 500, 20), new Vector2(250, 450), _startButton);
            _levelBackground = Content.Load<Texture2D>("LevelBackground");
            //sceneManager = new GameSceneManager(_backgroundTexture, startButton, _spriteBatch, map, _mapTexture, hero, enemy1, health, _healthTexture);
            currentState = GameStates.StartScreen;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            startButton.Update(gameTime);
            heroCollisionManager.Update(gameTime, map, enemyList, hero, kb);
            hero.Update(gameTime, kb);           
            enemy1.Update(gameTime);
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            if(currentState == GameStates.StartScreen)
            {
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 1000, 700), Color.White);
                startButton.Draw(_spriteBatch);

            } else if(currentState == GameStates.Level1)
            {
                map.Draw(_spriteBatch, _mapTexture);
                hero.Draw(_spriteBatch);
                enemy1.Draw(_spriteBatch);
                health.Draw(_spriteBatch, _healthTexture, hero);
            }
            base.Draw(gameTime);
            _spriteBatch.End(); 
        }
    }
}