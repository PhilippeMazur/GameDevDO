using Microsoft.Xna.Framework;
using Slime.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Interfaces
{
    public interface IInputreader
    {
        public Vector2 ReadInput(Vector2 pos, Hero hero);
    }
}
