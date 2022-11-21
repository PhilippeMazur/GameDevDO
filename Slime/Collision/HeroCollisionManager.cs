using Microsoft.Xna.Framework;
using Slime.Characters;
using Slime.Input;
using Slime.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Slime.Collision
{
    internal class HeroCollisionManager
    {

        Random r = new Random();
        public HeroCollisionManager()
        {

        }

        public void Update(GameTime gametime, TileMap map, List<Enemy> enemies, Hero hero, KeyboardReader kb)
        {
            hero.floorTileDifference = hero.previousFloorTile.Y - hero.currentFloorTile.Y;
            foreach (var item in map.allTiles)
            {
                
                if (item.recPos.Intersects(hero.hitbox) && (item.myType is Block.typeBlock.FLOOR || item.myType is Block.typeBlock.FLOOR2 || item.myType is Block.typeBlock.SPIKE || item.myType is Block.typeBlock.SPIKE2 || item.myType is Block.typeBlock.CLOUD || item.myType is Block.typeBlock.CLOUD2))
                {
                    
                    
                    if (hero.position.X >= item.recPos.X || hero.position.X <= item.recPos.X && hero.position.Y >= item.recPos.Y + 50)
                    {
                        hero.previousFloorTile = hero.currentFloorTile;
                        

                        hero.position.Y = item.recPos.Y - 50;

                        
                        kb.isFalling = false;
                        kb.hasJumped = false;
                    }

                    if(item.recPos.Intersects(hero.hitboxBody) && hero.position.X >= item.recPos.X)
                    {                      
                        kb.speedLeft = 0;
                    }
                    if(item.recPos.Intersects(hero.hitboxBody) && hero.position.X <= item.recPos.X)
                    {
                        kb.speedRight = 0;
                    }

                    if(item.myType == Block.typeBlock.SPIKE)
                    {
                        hero.position.X -= 100;
                        hero.health -= 1;
                    }
                    


                    if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.currentFloorTile.Y)
                    {
                        hero.currentFloorTile.X = item.recPos.X;
                        hero.currentFloorTile.Y = item.recPos.Y - item.textureRectangle.Height;
                    }
                    
                }
                
                if (item.recPos.Intersects(hero.hitbox) && (item.myType == Block.typeBlock.SKY)) {
                    
                    hero.currentFloorTile.X = item.recPos.X; 
                    hero.currentFloorTile.Y = item.recPos.Y - item.textureRectangle.Height + 50;
                    if(hero.floorTileDifference > 75)
                    {
                        hero.position.Y = hero.previousFloorTile.Y;
                    }
                }
            }
            foreach (var item in enemies)
            {
                if(hero.hitbox.Intersects(item.hitbox) && hero.position.Y  <= item.position.Y - 40)
                {
                    item.isAlive = false;

                } else if(hero.hitbox.Intersects(item.hitbox) && hero.position.Y >= item.position.Y - 40 && item.isAlive)
                {
                    hero.position.X -= 100;
                    hero.health -= 1;
                }
            }
            
        }
    }
}
