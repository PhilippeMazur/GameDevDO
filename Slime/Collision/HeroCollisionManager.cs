using Microsoft.Xna.Framework;
using Slime.Characters;
using Slime.Input;
using Slime.Map;
using System;

namespace Slime.Collision
{
    internal class HeroCollisionManager
    {

        Random r = new Random();
        public HeroCollisionManager()
        {

        }

        public void Update(GameTime gametime, TileMap map, Hero hero, KeyboardReader kb)
        {
            hero.floorTileDifference = hero.previousFloorTile.Y - hero.currentFloorTile.Y;
            foreach (var item in map.allTiles)
            {
                
                if (item.recPos.Intersects(hero.hitbox) && (item.myType is Block.typeBlock.FLOOR || item.myType is Block.typeBlock.FLOOR2 || item.myType is Block.typeBlock.SPIKE || item.myType is Block.typeBlock.SPIKE2 || item.myType is Block.typeBlock.CLOUD || item.myType is Block.typeBlock.CLOUD2))
                {
                    
                    
                    if (hero.position.X >= item.recPos.X || hero.position.X <= item.recPos.X + 50 && hero.position.Y >= item.recPos.Y + 50)
                    {
                        hero.previousFloorTile = hero.currentFloorTile;

                        hero.position.Y = item.recPos.Y - 50;

                        
                        kb.isFalling = false;
                        kb.hasJumped = false;
                    }

                    if(item.recPos.Intersects(hero.hitboxBody))
                    {
                        
                        
                        kb.speed = 0;
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
            
        }
    }
}
