using Microsoft.Xna.Framework;
using Slime.Characters;
using Slime.GameElements;
using Slime.Input;
using Slime.Map;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Slime.Collision
{
    internal class HeroCollisionManager
    {

        public HeroCollisionManager()
        {

        }

        public void Update(GameTime gametime, TileMap map, Hero hero, KeyboardReader kb, List<NextLevelDoor> doors, List<Enemy> enemyList, List<Coin> coinList)
        {
            hero.floorTileDifference = hero.previousFloorTile.Y - hero.currentFloorTile.Y;
            if(hero.floorTileDifference < 0)
            {
                hero.hasJumped = false;
                hero.isFalling = true;
            }

            if(hero.position.X > Game1.graphics.PreferredBackBufferWidth + 200)
            {
                hero.position.X = 0;
            } else if(hero.position.X < 0)
            {
                hero.position.X = 1000;
            }
            if(Game1.currentState == Game1.GameStates.Level1)
            {
                
                foreach (var item in map.blocks)
                {

                    if (item.recPos.Intersects(hero.hitboxBody) && (item.myType is Block.typeBlock.FLOOR || item.myType is Block.typeBlock.FLOOR2 || item.myType is Block.typeBlock.SPIKE || item.myType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.position.X + 27 >= item.recPos.X || hero.position.X - 27 <= item.recPos.X && hero.position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.position.Y <= item.recPos.Y && item.myType == Block.typeBlock.FLOOR2)
                            {
                                hero.previousFloorTile = hero.currentFloorTile;
                            }
                            hero.position.Y = item.recPos.Y - 50;
                            hero.isFalling = false;
                            hero.hasJumped = false;
                        }

                        if((item.myType == Block.typeBlock.FLOOR2 || item.myType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X >= hero.hitboxBody.X)
                        {
                            kb.speedRight = 0;
                        } else if((item.myType == Block.typeBlock.FLOOR2 || item.myType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X <= hero.hitboxBody.X)
                        {
                            kb.speedLeft = 0;
                        }
                        if (item.myType == Block.typeBlock.SPIKE)
                        {
                            if (hero.position.X >= item.recPos.X - 20)
                            {
                                hero.position.X += 100;

                            }
                            else
                            {
                                hero.position.X -= 100;

                            }
                            hero.health -= 1;
                        }
                        if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.currentFloorTile.Y)
                        {
                            hero.currentFloorTile.X = item.recPos.X;
                            hero.currentFloorTile.Y = item.recPos.Y - item.textureRectangle.Height;
                        }

                    }
                    if (item.recPos.Intersects(hero.hitbox) && (item.myType == Block.typeBlock.SKY))
                    {

                        hero.currentFloorTile.X = item.recPos.X;
                        hero.currentFloorTile.Y = item.recPos.Y;
                        if (hero.floorTileDifference > 75)
                        {
                            //hero.position.Y = hero.previousFloorTile.Y;
                        }
                    }
                }
            }
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var item in coinList)
                {
                    if(item.level == Coin.CoinLevelType.Level1)
                    {
                        item.isCollected = true;
                    }
                }
                foreach (var item in enemyList)
                {
                    if(item.level == Enemy.Level.Level1)
                    {
                        item.isAlive = false;
                    }
                }
                foreach (var item in map.blocks2)
                {

                    if (item.recPos.Intersects(hero.hitboxBody) && (item.myType is Block.typeBlock.FLOOR || item.myType is Block.typeBlock.FLOOR2 || item.myType is Block.typeBlock.SPIKE || item.myType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.position.X + 27 >= item.recPos.X || hero.position.X - 27 <= item.recPos.X && hero.position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.position.Y <= item.recPos.Y)
                            {
                                hero.previousFloorTile = hero.currentFloorTile;
                            }
                            hero.position.Y = item.recPos.Y - 50;
                            //hero.isFalling = false;
                            //hero.hasJumped = false;
                        }

                        if ((item.myType == Block.typeBlock.FLOOR2 || item.myType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X >= hero.hitboxBody.X)
                        {
                            kb.speedRight = 0;
                        }
                        else if ((item.myType == Block.typeBlock.FLOOR2 || item.myType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X <= hero.hitboxBody.X)
                        {
                            kb.speedLeft = 0;
                        }
                        if (item.myType == Block.typeBlock.SPIKE)
                        {
                            if (hero.position.X >= item.recPos.X - 20)
                            {
                                hero.position.X += 100;

                            }
                            else
                            {
                                hero.position.X -= 100;

                            }
                            hero.health -= 1;
                        }
                        if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.currentFloorTile.Y)
                        {
                            hero.currentFloorTile.X = item.recPos.X;
                            hero.currentFloorTile.Y = item.recPos.Y - item.textureRectangle.Height;
                        }

                    }
                    if (item.recPos.Intersects(hero.hitbox) && (item.myType == Block.typeBlock.SKY))
                    {

                        hero.currentFloorTile.X = item.recPos.X;
                        hero.currentFloorTile.Y = item.recPos.Y - item.textureRectangle.Height + 50;
                        if (hero.floorTileDifference > 75)
                        {
                            hero.position.Y = hero.previousFloorTile.Y;
                        }
                    }
                }
            }

            foreach (var item in enemyList)
            {
                if(hero.hitboxBody.Intersects(item.hitbox) && hero.position.Y  <= item.position.Y - 40)
                {
                    item.isAlive = false;

                } else if(hero.hitboxBody.Intersects(item.hitbox) && hero.position.Y <= item.position.Y && item.isAlive)
                {
                    hero.position.X -= 100;
                    hero.health -= 1;
                }
            }
            ///Check Coin Collision 
            if(Game1.currentState == Game1.GameStates.Level1)
            {
                foreach (var item in coinList)
                {
                    if (hero.hitbox.Intersects(item.hitbox) && item.level == Coin.CoinLevelType.Level1)
                    {
                        item.isCollected = true;
                        hero.coinsLevel1 += item.value;
                        item.value = 0;
                    }
                }
            }
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var item in coinList)
                {
                    if (hero.hitbox.Intersects(item.hitbox) && item.level == Coin.CoinLevelType.Level2)
                    {
                        item.isCollected = true;
                        hero.coinsLevel1 += item.value;
                        item.value = 0;
                    }
                }
            }

            foreach (var item in doors)
            {

                if (item.hitbox.Intersects(hero.hitboxBody) && item.isOpened)
                {
                    Game1.currentState = Game1.GameStates.Level2;
                    hero.position = new Vector2(0, 150);
                    Debug.WriteLine("level2");
                    hero.coinsLevel1 = 0;
                }
            }
            
            
            
        }
    }
}
