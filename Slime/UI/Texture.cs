using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    internal class Texture
    {
        public Texture2D texture;
        public enum TextureType
        {
            Hero,Enemy,Coin,Door,StartScreen,LevelBackground,GameOverScreen,Map,StartButton, Health
        }
        public Dictionary<TextureType, Texture2D> textureDictionary = new Dictionary<TextureType, Texture2D>();

        public Texture()
        {

        }
        public void LoadContent(ContentManager content)
        {
            textureDictionary.Add(TextureType.Hero, content.Load<Texture2D>("SlimeHero"));
            textureDictionary.Add(TextureType.Enemy, content.Load<Texture2D>("SlimeEnemy"));
            textureDictionary.Add(TextureType.Coin, content.Load<Texture2D>("CoinSpriteSheet"));
            textureDictionary.Add(TextureType.Door, content.Load<Texture2D>("PortalDoor"));
            textureDictionary.Add(TextureType.StartScreen, content.Load<Texture2D>("BackgroundWithLogo"));
            textureDictionary.Add(TextureType.LevelBackground, content.Load<Texture2D>("LevelBackground"));
            textureDictionary.Add(TextureType.GameOverScreen, content.Load<Texture2D>("GameOverScreenAnimated"));
            textureDictionary.Add(TextureType.Map, content.Load<Texture2D>("NewTileSet"));
            textureDictionary.Add(TextureType.StartButton, content.Load<Texture2D>("PressStart"));
            textureDictionary.Add(TextureType.Health, content.Load<Texture2D>("HealthHeart"));
        }
    }
}
