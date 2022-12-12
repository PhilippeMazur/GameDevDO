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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Texture = Slime.UI.Texture;

namespace Slime
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch _spriteBatch;
        Hero hero;
        KeyboardReader kb = new KeyboardReader();
        public enum GameStates
        {
            StartScreen,
            Level1,
            Level2,
            WinningScreen,
            GameOver
        }
        public static GameStates currentState;
        private Button startButton;
        GameSceneManager2 gameSceneManager2 = new GameSceneManager2();

        private GameOverScreen gameOverScreen;

        private SpriteFont sf;

        Score scoreUI = new Score();
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
            gameSceneManager2.LoadContent(GraphicsDevice ,Content);
            hero = new Hero(gameSceneManager2.texture.textureDictionary[Texture.TextureType.Hero], kb);
            hero.LoadContent(GraphicsDevice, _spriteBatch);
            startButton = new Button(new Rectangle(0, 0, 500, 20), new Vector2(250, 450));
            currentState = GameStates.StartScreen;
            gameOverScreen = new GameOverScreen();

            sf = Content.Load<SpriteFont>("fonts/File");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            startButton.Update(gameTime, hero);
            gameSceneManager2.Update(gameTime, startButton, hero, gameOverScreen, kb);
            hero.Update(gameTime, kb);
            base.Update(gameTime);           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            gameSceneManager2.Draw(hero, startButton,gameOverScreen);
            _spriteBatch.DrawString(sf, $"{hero.position.X} : {string.Format("{0:F0}",hero.position.Y)}", new Vector2(500, 0), Color.Yellow);

            scoreUI.Draw(_spriteBatch, sf);

            base.Draw(gameTime);
            _spriteBatch.End(); 
            
        }
    }
}