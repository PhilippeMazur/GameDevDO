using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Map
{
    public class Block
    {
        public Vector2 Pos { get { return pos; } set { pos = value; } }
        private Vector2 pos;
        public Rectangle RecPos { get { return recPos; } set { recPos = value; } }
        private Rectangle recPos;
        public Rectangle TextureRectangle { get { return textureRectangle; } set { textureRectangle = value; } }
        private Rectangle textureRectangle;
        private int w = 50;
        private int h = 50;
        public typeBlock MyType { get; set; }
        public enum typeBlock
        {
            SKY,
            FLOOR,
            FLOOR2,
            SPIKE,
            SPIKE2,
            CLOUD,
            CLOUD2,
            LAMP,
            LAMP2
        }
        public Block(typeBlock blockType, Vector2 rectIn)
        {
            
            if (blockType == typeBlock.SKY)
            {
                MyType = typeBlock.SKY;
                pos = rectIn;
                textureRectangle = new Rectangle(0, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);
            }
            else if (blockType == typeBlock.FLOOR)
            {
                MyType = typeBlock.FLOOR;

                pos = rectIn;
                textureRectangle = new Rectangle(50, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.FLOOR2)
            {
                MyType = typeBlock.FLOOR2;

                pos = rectIn;
                textureRectangle = new Rectangle(100, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);



            }
            else if (blockType == typeBlock.SPIKE)
            {
                MyType = typeBlock.SPIKE;

                pos = rectIn;
                textureRectangle = new Rectangle(150, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.SPIKE2)
            {
                MyType = typeBlock.SPIKE2;

                pos = rectIn;
                textureRectangle = new Rectangle(200, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.LAMP)
            {
                MyType = typeBlock.LAMP;

                pos = rectIn;
                textureRectangle = new Rectangle(250, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }
            else if (blockType == typeBlock.LAMP2)
            {
                MyType = typeBlock.LAMP2;

                pos = rectIn;
                textureRectangle = new Rectangle(300, 0, w, h);
                recPos = new Rectangle((int)pos.X, (int)pos.Y, w, h);


            }

        }
    }
}
