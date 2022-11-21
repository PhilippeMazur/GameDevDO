using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;

namespace Slime.Map
{
    internal class TileMap
    {
        public List<Block> blocks = new List<Block>();
        public List<Block> allTiles = new List<Block>();


        public TileMap()
        {
            //block.AddBlock(new Rectangle(100, 100, 224, 224));
            CreateBlocks();
            
        }

        int[,] tiles = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {2,2,2,2,1,2,2,2,2,2,2,2,2,2,3,2,2,2,2,2}

        };

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(_levelBackground, new Rectangle(0,0,1000,700), Color.White);
            foreach (var item in blocks)
            {
                if(item.myType != Block.typeBlock.SKY)
                {
                    spriteBatch.Draw(texture, item.pos, item.textureRectangle, Color.White);

                }


            }

        }

        private void CreateBlocks()
        {
            for (int l = 0; l < tiles.GetLength(0); l++)
            {

                for (int c = 0; c < tiles.GetLength(1); c++)
                {
                    if (tiles[l, c] == 0)
                    {
                        blocks.Add(new Block(Block.typeBlock.SKY, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SKY, new Vector2(c * 50, l * 50)));
                    }
                    else if(tiles[l, c] == 1)
                    {
                        blocks.Add(new Block(Block.typeBlock.FLOOR, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.FLOOR, new Vector2(c * 50, l * 50)));
                    }
                    else if (tiles[l, c] == 2)
                    {
                        blocks.Add(new Block(Block.typeBlock.FLOOR2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.FLOOR2, new Vector2(c * 50, l * 50)));
                    }
                    else if (tiles[l, c] == 3)
                    {
                        blocks.Add(new Block(Block.typeBlock.SPIKE, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SPIKE, new Vector2(c * 50, l * 50)));

                    }
                    else if (tiles[l, c] == 4)
                    {
                        blocks.Add(new Block(Block.typeBlock.SPIKE2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SPIKE2, new Vector2(c * 50, l * 50)));

                    }
                    else if (tiles[l, c] == 5)
                    {
                        blocks.Add(new Block(Block.typeBlock.CLOUD, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.CLOUD, new Vector2(c * 50, l * 50)));

                    }
                    else if (tiles[l, c] == 6)
                    {
                        blocks.Add(new Block(Block.typeBlock.CLOUD2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.CLOUD2, new Vector2(c * 50, l * 50)));

                    }
                    
                }
            }
        }
    }
}
