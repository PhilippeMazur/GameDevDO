using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.GameObjects.Characters
{
    internal class GroundEnemy : Enemy
    {
        public GroundEnemy(Texture2D texturein, Vector2 posin, int maxDistance, Type enemyType, Level level, Hero heroin) : base(texturein, posin, maxDistance, enemyType, level, heroin)
        {
        }
    }
}
