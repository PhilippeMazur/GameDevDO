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
    internal class HeroCollisionManager2
    {
        public void Update(TileMap map, Hero hero, KeyboardReader kb, List<NextLevelDoor> doors, List<Enemy> enemyList, List<Coin> coinList, Score score)
        {
            //check if player goes out of bounds
            if (hero.Position.X > Game1.graphics.PreferredBackBufferWidth + 200)
            {
                hero.Position = new Vector2(0, hero.Position.Y);
            }
            else if (hero.Position.X < 0)
            {
                hero.Position = new Vector2(1000, hero.Position.Y);
            }
            //check collision in level1
            if (Game1.currentState == Game1.GameStates.Level1)
            {
                foreach (var item in map.blocks)
                {

                    if (item.recPos.Intersects(hero.HitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= item.recPos.X || hero.Position.X - 27 <= item.recPos.X && hero.Position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.Position.Y <= item.recPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {
                                //hero.currentFloorTile.X = item.recPos.X + 50;
                                //hero.currentFloorTile.Y = item.recPos.Y - 50;
                                //hero.previousFloorTile = hero.currentFloorTile;
                                hero.CurrentFloorTile = new Vector2(item.recPos.X + 50, item.recPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            hero.Position = new Vector2(hero.Position.X, item.recPos.Y - 50);
                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.HitboxBody.Y && item.recPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.HitboxBody.Y && item.recPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= item.recPos.X - 20)
                            {
                                //hero.position.Y -= 100;
                                hero.Position -= new Vector2(0, 100);
                                //hero.position.X += 50;
                                hero.Position += new Vector2(50, 0);

                            }
                            else
                            {
                                //hero.position.X -= 100;
                                hero.Position -= new Vector2(100, 0);
                            }
                            hero.Health -= 1;
                            hero.Hit = true;

                        }
                        if (hero.Position.Y < item.recPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            //hero.currentFloorTile.X = item.recPos.X;
                            //hero.currentFloorTile.Y = item.recPos.Y;
                            hero.CurrentFloorTile = new Vector2(item.recPos.X, item.recPos.Y);
                        }

                    }
                }
            }
            //check collision in level2
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var item in coinList)
                {
                    if (item.level == Coin.CoinLevelType.Level1)
                    {
                        item.IsCollected = true;
                    }
                }

                foreach (var item in map.blocks2)
                {

                    if (item.recPos.Intersects(hero.HitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= item.recPos.X || hero.Position.X - 27 <= item.recPos.X && hero.Position.Y + 13 >= item.recPos.Y)
                        {
                            if (hero.Position.Y <= item.recPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {
                                //hero.previousFloorTile = hero.currentFloorTile;
                                //hero.currentFloorTile.X = item.recPos.X + 50;
                                //hero.currentFloorTile.Y = item.recPos.Y - 50;
                                //hero.previousFloorTile = hero.currentFloorTile;
                                hero.CurrentFloorTile = new Vector2(item.recPos.X + 50, item.recPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            //hero.position.Y = item.recPos.Y - 50;
                            hero.Position = new Vector2(hero.Position.X, item.recPos.Y - 50);

                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.HitboxBody.Y && item.recPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.recPos.Y < hero.HitboxBody.Y && item.recPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= item.recPos.X - 20)
                            {
                                //hero.position.X += 100;
                                hero.Position += new Vector2(100, 0);


                            }
                            else
                            {
                                //hero.position.X -= 100;
                                hero.Position -= new Vector2(100, 0);

                            }
                            hero.Health -= 1;
                            hero.Hit = true;

                        }
                        if (hero.Position.Y < item.recPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            //hero.currentFloorTile.X = item.recPos.X;
                            //hero.currentFloorTile.Y = item.recPos.Y;
                            hero.CurrentFloorTile = new Vector2(item.recPos.X, item.recPos.Y);
                        }

                    }
                }
            }

            //Check Enemy collision
            foreach (var item in enemyList)
            {
                if (Game1.currentState == Game1.GameStates.Level1 && item.level == Enemy.Level.Level1)
                {
                    if (hero.HitboxBody.Intersects(item.hitboxBody) && hero.Position.Y <= item.Position.Y - 15)
                    {
                        item.IsAlive = false;

                        score.ScoreValue += item.scoreValue;
                        item.scoreValue = 0;


                    }
                    else if (hero.HitboxBody.Intersects(item.hitboxBody) && hero.Position.Y <= item.Position.Y && item.IsAlive)
                    {
                        //hero.position.X -= 100;
                        hero.Position -= new Vector2(100, 0);

                        hero.Health -= 1;
                        hero.Hit = true;
                    }
                }
                if (Game1.currentState == Game1.GameStates.Level2 && item.level == Enemy.Level.Level2)
                {
                    if (hero.HitboxBody.Intersects(item.hitboxBody) && hero.Position.Y <= item.Position.Y - 15)
                    {
                        item.IsAlive = false;

                        score.ScoreValue += item.scoreValue;
                        item.scoreValue = 0;


                    }
                    else if (hero.HitboxBody.Intersects(item.hitboxBody) && hero.Position.Y <= item.Position.Y && item.IsAlive)
                    {
                        //hero.position.X -= 100;
                        hero.Position -= new Vector2(100, 0);

                        hero.Health -= 1;
                        hero.Hit = true;
                    }
                }

            }

            ///Check Coin Collision 
            if (Game1.currentState == Game1.GameStates.StartScreen || Game1.currentState == Game1.GameStates.GameOver)
            {
                hero.CoinsLevel1 = 0;
                hero.CoinsLevel2 = 0;
                score.ScoreValue = 0;
                foreach (var item in enemyList)
                {
                    item.IsAlive = true;
                    item.scoreValue = 1;
                }
                foreach (var item in coinList)
                {

                    item.IsCollected = false;
                    item.CoinValue = 1;

                }
            }
            if (Game1.currentState == Game1.GameStates.Level1)
            {
                foreach (var item in coinList)
                {
                    if (hero.Hitbox.Intersects(item.Hitbox) && item.level == Coin.CoinLevelType.Level1)
                    {
                        item.IsCollected = true;
                        hero.CoinsLevel1 += item.CoinValue;
                        item.CoinValue = 0;
                    }
                }
            }
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var item in coinList)
                {

                    if (hero.Hitbox.Intersects(item.Hitbox) && item.level == Coin.CoinLevelType.Level2)
                    {
                        if (item.level == Coin.CoinLevelType.Level2 && Game1.currentState == Game1.GameStates.Level2)
                        {
                            item.IsCollected = true;
                            hero.CoinsLevel2 += item.CoinValue;
                            item.CoinValue = 0;
                        }

                    }
                }
            }
            //check door collision
            foreach (var item in doors)
            {

                if (item.Hitbox.Intersects(hero.HitboxBody) && item.IsOpened)
                {
                    if (item.Level == NextLevelDoor.DoorLevel.Level1)
                    {
                        Game1.currentState = Game1.GameStates.Level2;
                        hero.Position = new Vector2(0, 400);

                    }
                    if (item.Level == NextLevelDoor.DoorLevel.Level2 && item.IsOpened)
                    {
                        Game1.currentState = Game1.GameStates.WinningScreen;

                    }

                }
            }



        }
    }
}
