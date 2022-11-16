using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slime.Characters;
using Slime.Input;
using Slime.Map;
using System.Diagnostics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

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
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _heroTexture = Content.Load<Texture2D>("SlimeHero");
            hero = new Hero(_heroTexture, kb);
            hero.LoadContent(GraphicsDevice, _spriteBatch);
            _mapTexture = Content.Load<Texture2D>("MapTiles");

            foreach (var item in map.allTiles)
            {
                if (item.myType == Block.typeBlock.FLOOR2)
                {
                    
                    Debug.WriteLine(item.recPos);
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Debug.WriteLine(hero.hitbox);
            // TODO: Add your update logic here
            foreach (var item in map.allTiles)
            {
                
                if (item.myType == Block.typeBlock.FLOOR2 && item.recPos.Intersects(hero.hitbox))
                {
                    touchCounter++;
                    Debug.WriteLine("Touched " + touchCounter);
                    hero.floorTile.X = item.recPos.X;
                    hero.floorTile.Y = item.recPos.Y - item.textureRectangle.Height;
                }
            }
            hero.Update(gameTime, kb);
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            map.Draw(_spriteBatch, _mapTexture);
            hero.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End(); 
        }
    }
}