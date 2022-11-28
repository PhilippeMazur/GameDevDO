using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    internal class Textures
    {
        public enum AllTextureTypes
        {
            Coin, Background, Hero, Map, Enemy, Health
        }
        public static Dictionary<AllTextureTypes, Texture2D> textures = new Dictionary<AllTextureTypes, Texture2D>();

        public Texture2D Texture2D { get; set; }
        public AllTextureTypes TextureType { get; set; }

        public Textures(Texture2D texture2D, AllTextureTypes textureType)
        {
            Texture2D = texture2D;
            TextureType = textureType;
        }
    }
}
