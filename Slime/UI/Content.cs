using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
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
    static class Content
    {
        public static Texture2D _coinTexture;
        public static Texture2D _levelBackground;
        public static Texture2D _heroTexture;
        public static Texture2D _mapTexture;
        public static Texture2D _enemyTexture;
        public static Texture2D _backgroundTexture;
        public static Texture2D _healthTexture;
        public static Texture2D _startButton;
        public static Texture2D _doorTexture;
        public static Texture2D _gameOverScreenTexture;



        public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _coinTexture = content.Load<Texture2D>("CoinSPriteSheet");
            _levelBackground = content.Load<Texture2D>("LevelBackground");
            _heroTexture = content.Load<Texture2D>("SlimeHero");
            _mapTexture = content.Load<Texture2D>("MapTiles");
            _enemyTexture = content.Load<Texture2D>("SlimeEnemy");
            _backgroundTexture = content.Load<Texture2D>("BackgroundWithLogo");
            _healthTexture = content.Load<Texture2D>("HealthHeart");
            _startButton = content.Load<Texture2D>("PressStart");
            _doorTexture = content.Load<Texture2D>("PortalDoor");
            _gameOverScreenTexture = content.Load<Texture2D>("GameOverScreenAnimated");

        }
    }
}