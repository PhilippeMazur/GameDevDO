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
        public void Update(TileMap map, Hero hero, KeyboardReader kb, List<NextLevelDoor> doors, List<Enemy> enemyList, List<Coin> coinList, Score score)
        {
            //check if player goes out of bounds
            if(hero.position.X > Game1.graphics.PreferredBackBufferWidth + 200)
            {
                hero.position = new Vector2(0,hero.position.Y);
            } else if(hero.position.X < 0)
            {
                hero.position.X = 1000;
            }
            //check collision in level1
            if(Game1.currentState == Game1.GameStates.Level1)
            {
                foreach (var item in map.blocks)
                {

                    if (item.recPos.Intersects(hero.hitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.position.X + 27 >= item.recPos.X || hero.position.X - 27 <= item.recPos.X && hero.position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.position.Y <= item.recPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {
                                hero.currentFloorTile.X = item.recPos.X + 50;
                                hero.currentFloorTile.Y = item.recPos.Y - 50;
                                hero.previousFloorTile = hero.currentFloorTile;
                            }
                            hero.position.Y = item.recPos.Y - 50;
                            hero.isFalling = false;
                            hero.hasJumped = false;
                        }

                        if((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X >= hero.hitboxBody.X)
                        {
                            kb.speedRight = 0;
                        } else if((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X <= hero.hitboxBody.X)
                        {
                            kb.speedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.position.X >= item.recPos.X - 20)
                            {
                                hero.position.Y -= 100;
                                hero.position.X += 50;

                            }
                            else
                            {
                                hero.position.X -= 100;

                            }
                            hero.health -= 1;
                            hero.hit = true;

                        }
                        if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.currentFloorTile.Y)
                        {
                            hero.currentFloorTile.X = item.recPos.X;
                            hero.currentFloorTile.Y = item.recPos.Y;
                        }

                    }
                }
            }
            //check collision in level2
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var item in coinList)
                {
                    if(item.level == Coin.CoinLevelType.Level1)
                    {
                        item.isCollected = true;
                    }
                }

                foreach (var item in map.blocks2)
                {

                    if (item.recPos.Intersects(hero.hitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.position.X + 27 >= item.recPos.X || hero.position.X - 27 <= item.recPos.X && hero.position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.position.Y <= item.recPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {
                                //hero.previousFloorTile = hero.currentFloorTile;
                                hero.currentFloorTile.X = item.recPos.X + 50;
                                hero.currentFloorTile.Y = item.recPos.Y - 50;
                                hero.previousFloorTile = hero.currentFloorTile;
                            }
                            hero.position.Y = item.recPos.Y - 50;
                            hero.isFalling = false;
                            hero.hasJumped = false;
                        }

                        if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X >= hero.hitboxBody.X)
                        {
                            kb.speedRight = 0;
                        }
                        else if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.hitboxBody.Y && item.recPos.X <= hero.hitboxBody.X)
                        {
                            kb.speedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
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
                            hero.hit = true;

                        }
                        if (hero.position.Y < item.recPos.Bottom && hero.position.Y < hero.currentFloorTile.Y)
                        {
                            hero.currentFloorTile.X = item.recPos.X;
                            hero.currentFloorTile.Y = item.recPos.Y;
                        }

                    }
                }
            }
            
            //Check Enemy collision
            foreach (var item in enemyList)
            {
                if(Game1.currentState == Game1.GameStates.Level1 && item.level == Enemy.Level.Level1)
                {
                    if (hero.hitboxBody.Intersects(item.hitboxBody) && hero.position.Y <= item.position.Y - 15)
                    {
                        item.isAlive = false;

                        score.score += item.scoreValue;
                        item.scoreValue = 0;


                    }
                    else if (hero.hitboxBody.Intersects(item.hitboxBody) && hero.position.Y <= item.position.Y && item.isAlive)
                    {
                        hero.position.X -= 100;
                        hero.health -= 1;
                        hero.hit = true;
                    }
                } 
                if(Game1.currentState == Game1.GameStates.Level2 && item.level == Enemy.Level.Level2)
                {
                    if (hero.hitboxBody.Intersects(item.hitboxBody) && hero.position.Y <= item.position.Y - 15)
                    {
                        item.isAlive = false;

                        score.score += item.scoreValue;
                        item.scoreValue = 0;


                    }
                    else if (hero.hitboxBody.Intersects(item.hitboxBody) && hero.position.Y <= item.position.Y && item.isAlive)
                    {
                        hero.position.X -= 100;
                        hero.health -= 1;
                        hero.hit = true;
                    }
                }
               
            }

            ///Check Coin Collision 
            if(Game1.currentState == Game1.GameStates.StartScreen || Game1.currentState == Game1.GameStates.GameOver)
            {
                hero.coinsLevel1 = 0;
                hero.coinsLevel2 = 0;
                score.score = 0;
                foreach (var item in enemyList)
                {
                    item.isAlive = true;
                    item.scoreValue = 1;
                }
                foreach (var item in coinList)
                {
                    
                   item.isCollected = false;
                   item.value = 1;
                   
                }
            }
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
                        if(item.level == Coin.CoinLevelType.Level2 && Game1.currentState == Game1.GameStates.Level2)
                        {
                            item.isCollected = true;
                            hero.coinsLevel2 += item.value;
                            item.value = 0;
                        }
                        
                    }
                }
            }
            //check door collision
            foreach (var item in doors)
            {
                
                if (item.hitbox.Intersects(hero.hitboxBody) && item.isOpened)
                {
                    if (item.level == NextLevelDoor.DoorLevel.Level1)
                    {
                        Game1.currentState = Game1.GameStates.Level2;
                        hero.Position = new Vector2(0, 400);
                        
                    } 
                    if(item.level == NextLevelDoor.DoorLevel.Level2 && item.isOpened)
                    {
                        Game1.currentState = Game1.GameStates.WinningScreen;
                        
                    }

                }
            }
            
            
            
        }
    }
}
