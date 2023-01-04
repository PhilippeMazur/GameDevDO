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
        public void Update(TileMap map, Hero hero, KeyboardReader kb, List<Door> doors, List<Enemy> enemyList, List<Coin> coinList, Score score)
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
                foreach (var item in map.Blocks)
                {

                    if (item.RecPos.Intersects(hero.HitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= item.RecPos.X || hero.Position.X - 27 <= item.RecPos.X && hero.Position.Y + 13 >= item.RecPos.Y)
                        {
                            if (hero.Position.Y <= item.RecPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {
                                hero.CurrentFloorTile = new Vector2(item.RecPos.X + 50, item.RecPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            hero.Position = new Vector2(hero.Position.X, item.RecPos.Y - 50);
                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.RecPos.Y < hero.HitboxBody.Y && item.RecPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.RecPos.Y < hero.HitboxBody.Y && item.RecPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= item.RecPos.X - 20)
                            {
                                hero.Position -= new Vector2(0, 100);
                                hero.Position += new Vector2(50, 0);

                            }
                            else
                            {
                                hero.Position -= new Vector2(100, 0);
                            }
                            hero.Health -= 1;
                            hero.Hit = true;

                        }
                        if (hero.Position.Y < item.RecPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            hero.CurrentFloorTile = new Vector2(item.RecPos.X, item.RecPos.Y);
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

                foreach (var item in map.Blocks2)
                {

                    if (item.RecPos.Intersects(hero.HitboxBody) && (item.MyType is Block.typeBlock.FLOOR || item.MyType is Block.typeBlock.FLOOR2 || item.MyType is Block.typeBlock.SPIKE || item.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= item.RecPos.X || hero.Position.X - 27 <= item.RecPos.X && hero.Position.Y + 13 >= item.RecPos.Y)
                        {
                            if (hero.Position.Y <= item.RecPos.Y && item.MyType == Block.typeBlock.FLOOR2)
                            {

                                hero.CurrentFloorTile = new Vector2(item.RecPos.X + 50, item.RecPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            hero.Position = new Vector2(hero.Position.X, item.RecPos.Y - 50);

                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.RecPos.Y < hero.HitboxBody.Y && item.RecPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((item.MyType == Block.typeBlock.FLOOR2 || item.MyType == Block.typeBlock.FLOOR) && item.RecPos.Y < hero.HitboxBody.Y && item.RecPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (item.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= item.RecPos.X - 20)
                            {
                                hero.Position += new Vector2(100, 0);
                            }
                            else
                            {
                                hero.Position -= new Vector2(100, 0);
                            }
                            hero.Health -= 1;
                            hero.Hit = true;
                        }
                        if (hero.Position.Y < item.RecPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            hero.CurrentFloorTile = new Vector2(item.RecPos.X, item.RecPos.Y);
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
                    else if (hero.HitboxBody.Intersects(item.hitboxBody) && item.IsAlive && hero.Vulnerable)
                    {
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
                    else if (hero.HitboxBody.Intersects(item.hitboxBody) && item.IsAlive && hero.Vulnerable)
                    {
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
                    if (item.Level == Door.DoorLevel.Level1)
                    {
                        Game1.currentState = Game1.GameStates.Level2;
                        hero.Position = new Vector2(0, 400);

                    }
                    if (item.Level == Door.DoorLevel.Level2 && item.IsOpened)
                    {
                        Game1.currentState = Game1.GameStates.WinningScreen;
                    }

                }
            }
        }
    }
}
