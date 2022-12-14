using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slime.Interfaces;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Slime.Game1;

namespace Slime.Map
{
    public class TileMap : IGameObject
    {
        private Texture2D levelBackgroundTexture;
        private Texture2D tilesetTexture;
        private Vector2 backgroundPos = new Vector2(0,0);
        private double counter;
        public List<Block> Blocks { get { return blocks; } set { blocks = value; } }
        private List<Block> blocks = new List<Block>();
        public List<Block> Blocks2 { get { return blocks2; } set { blocks2 = value; } }
        private List<Block> blocks2 = new List<Block>();
        private List<Block> allTiles = new List<Block>();
        private int[,] level = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0},
            {0,6,0,0,2,2,2,0,0,0,0,0,0,0,0,0,5,0,0,0},
            {0,5,0,2,1,1,0,0,0,0,0,0,0,0,2,2,2,2,2,2},
            {2,2,2,1,1,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0},
            {1,1,1,1,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,6,0,0,2,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,5,0,2,0,0,0,2,2,2,0,0,0},
            {0,0,0,0,0,0,0,2,2,0,0,0,0,2,1,0,0,0,0,0},
            {0,0,0,0,2,0,0,0,0,0,0,0,2,1,1,0,0,0,0,0},
            {0,0,0,2,1,0,0,0,0,0,0,2,1,1,1,0,0,0,0,0},
            {2,2,2,1,1,3,3,3,2,2,2,1,1,1,1,2,2,2,2,2}

        };       
        private int[,] level2 = new int[,]
        {
            {0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,2,2,2,3,3,3,3,2,2,2,2,2,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {1,1,2,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0},
            {1,1,1,2,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0},
            {1,1,1,1,2,2,2,2,3,3,2,2,2,2,2,2,2,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,2},
            {0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,2,1},
            {2,2,2,0,0,2,0,0,2,0,0,2,0,0,2,0,0,2,1,1},
            {1,1,1,0,0,1,0,0,1,0,0,1,0,0,1,0,0,1,1,1},
            {1,1,1,3,3,1,3,3,1,3,3,1,3,3,1,3,3,1,1,1}

        };
        public TileMap(Texture2D levelBackgroundTexturein, Texture2D tilesetTexturein)
        {
            levelBackgroundTexture = levelBackgroundTexturein;
            tilesetTexture = tilesetTexturein;
            CreateBlocks(level, blocks);            
            CreateBlocks(level2, blocks2);            
        }     
        public void Update(GameTime gameTime)
        {
            counter += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(counter > 50d)
            {
                backgroundPos.X -= 1;
                counter = 0;
            }
            if(backgroundPos.X < -1000)
            {
                backgroundPos.X = 0;
            }
        }
        public void Draw()
        {
            Game1._spriteBatch.Draw(levelBackgroundTexture, new Rectangle((int)backgroundPos.X,0,1000,700), Color.White);
            Game1._spriteBatch.Draw(levelBackgroundTexture, new Rectangle((int)backgroundPos.X + 1000, 0, 1000, 700), Color.White);

            if (Game1.currentState == GameStates.Level1)
            {
                foreach (var item in blocks)
                {
                    if (item.MyType != Block.typeBlock.SKY)
                    {
                        Game1._spriteBatch.Draw(tilesetTexture, item.Pos, item.TextureRectangle, Color.White);

                    }
                }
            } else if(Game1.currentState == GameStates.Level2)
            {
                foreach (var item in blocks2)
                {
                    if (item.MyType != Block.typeBlock.SKY)
                    {
                        Game1._spriteBatch.Draw(tilesetTexture, item.Pos, item.TextureRectangle, Color.White);
                    }
                }
            }
        }
        private void CreateBlocks(int[,] level, List<Block> blocks)
        {
            for (int l = 0; l < level.GetLength(0); l++)
            {

                for (int c = 0; c < level.GetLength(1); c++)
                {
                    if (level[l, c] == 0)
                    {
                        blocks.Add(new Block(Block.typeBlock.SKY, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SKY, new Vector2(c * 50, l * 50)));
                    }
                    else if(level[l, c] == 1)
                    {
                        blocks.Add(new Block(Block.typeBlock.FLOOR, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.FLOOR, new Vector2(c * 50, l * 50)));
                    }
                    else if (level[l, c] == 2)
                    {
                        blocks.Add(new Block(Block.typeBlock.FLOOR2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.FLOOR2, new Vector2(c * 50, l * 50)));
                    }
                    else if (level[l, c] == 3)
                    {
                        blocks.Add(new Block(Block.typeBlock.SPIKE, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SPIKE, new Vector2(c * 50, l * 50)));


                    }
                    else if (level[l, c] == 4)
                    {
                        blocks.Add(new Block(Block.typeBlock.SPIKE2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.SPIKE2, new Vector2(c * 50, l * 50)));

                    }
                    else if (level[l, c] == 5)
                    {
                        blocks.Add(new Block(Block.typeBlock.LAMP, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.LAMP, new Vector2(c * 50, l * 50)));

                    }
                    else if (level[l, c] == 6)
                    {
                        blocks.Add(new Block(Block.typeBlock.LAMP2, new Vector2(c * 50, l * 50)));
                        allTiles.Add(new Block(Block.typeBlock.LAMP2, new Vector2(c * 50, l * 50)));

                    }
                    
                }
            }
        }
    }
}
