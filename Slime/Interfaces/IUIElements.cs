using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Interfaces
{
     
    internal interface IUIElements
    {
        public void Draw(SpriteBatch spriteBatch, Texture2D texture);
        public void Update();

    }
}
