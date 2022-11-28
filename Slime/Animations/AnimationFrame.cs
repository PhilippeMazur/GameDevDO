using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Animations
{
   
    public class AnimationFrame
    {
        public Rectangle sourceRectangle { get; set; }
        public AnimationFrame(Rectangle sourceRectangle, Vector2 hitbox)
        {
            this.sourceRectangle = sourceRectangle;
        }
        public AnimationFrame(Rectangle sourceRectangle)
        {
            this.sourceRectangle = sourceRectangle;
        }
    }
}
