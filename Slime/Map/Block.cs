using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Map
{
    internal class Block
    {
        public Vector2 pos;
        public Rectangle recPos;
        public Rectangle textureRectangle;
        public int w = 50;
        public int h = 50;
        public typeBlock myType { get; set; }
        public enum typeBlock
        {
            SKY,
            FLOOR,
            FLOOR2,
            SPIKE,
            SPIKE2,
            CLOUD,
            CLOUD2
            

        }
        public Block(typeBlock blockType, Vector2 rectIn)
        {
            
            if (blockType == typeBlock.SKY)
            {
                myType = typeBlock.SKY;
                pos = rectIn;
                textureRectangle = new Rectangle(0, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.FLOOR)
            {
                myType = typeBlock.FLOOR;

                pos = rectIn;
                textureRectangle = new Rectangle(50, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.FLOOR2)
            {
                myType = typeBlock.FLOOR2;

                pos = rectIn;
                textureRectangle = new Rectangle(100, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.SPIKE)
            {
                myType = typeBlock.SPIKE;

                pos = rectIn;
                textureRectangle = new Rectangle(150, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.SPIKE2)
            {
                myType = typeBlock.SPIKE2;

                pos = rectIn;
                textureRectangle = new Rectangle(200, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.CLOUD)
            {
                myType = typeBlock.CLOUD;

                pos = rectIn;
                textureRectangle = new Rectangle(250, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.CLOUD2)
            {
                myType = typeBlock.CLOUD2;

                pos = rectIn;
                textureRectangle = new Rectangle(300, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }

        }
    }
}
