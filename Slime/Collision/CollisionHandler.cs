using Microsoft.Xna.Framework;
using Slime.Characters;
using Slime.Input;
using Slime.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.Collision
{
    internal class CollisionHandler
    {

        Random r = new Random();
        public CollisionHandler()
        {

        }

        public void Update(GameTime gametime, TileMap map, Hero hero, KeyboardReader kb)
        {
            foreach (var item in map.allTiles)
            {
                
                if (item.recPos.Intersects(hero.hitbox) && (item.myType is Block.typeBlock.FLOOR || item.myType is Block.typeBlock.FLOOR2 || item.myType is Block.typeBlock.SPIKE || item.myType is Block.typeBlock.SPIKE2))
                {
                    
                    if (hero.position.X >= item.recPos.X || hero.position.X <= item.recPos.X + 50 && hero.position.Y >= item.recPos.Y + 50)
                    {
                        
                        hero.position.Y = item.recPos.Y - 50;

                        
                        kb.isFalling = false;
                        kb.hasJumped = false;
                    }

                    if(item.recPos.Intersects(hero.hitboxBody))
                    {
                        
                        
                        kb.speed = 0;
                    }
                    


                    if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.floorTile.Y)
                    {
                        hero.floorTile.X = item.recPos.X;
                        hero.floorTile.Y = item.recPos.Y - item.textureRectangle.Height;
                    }
                    
                }
                
                if (item.recPos.Intersects(hero.hitbox) && (item.myType == Block.typeBlock.SKY)) {
                    hero.floorTile.X = item.recPos.X; 
                    hero.floorTile.Y = item.recPos.Y - item.textureRectangle.Height + 50;
                }
            }
            
        }
    }
}
