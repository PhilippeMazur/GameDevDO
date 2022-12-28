using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Interfaces
{
    internal interface IGameObject
    {
        void Draw();
        void Update(GameTime gameTime);
    }
}
