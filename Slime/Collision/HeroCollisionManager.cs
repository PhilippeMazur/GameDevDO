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
                foreach (var block in map.Blocks)
                {

                    if (block.RecPos.Intersects(hero.HitboxBody) && (block.MyType is Block.typeBlock.FLOOR || block.MyType is Block.typeBlock.FLOOR2 || block.MyType is Block.typeBlock.SPIKE || block.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= block.RecPos.X || hero.Position.X - 27 <= block.RecPos.X && hero.Position.Y + 13 >= block.RecPos.Y)
                        {
                            if (hero.Position.Y <= block.RecPos.Y && block.MyType == Block.typeBlock.FLOOR2)
                            {
                                hero.CurrentFloorTile = new Vector2(block.RecPos.X + 50, block.RecPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            hero.Position = new Vector2(hero.Position.X, block.RecPos.Y - 50);
                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((block.MyType == Block.typeBlock.FLOOR2 || block.MyType == Block.typeBlock.FLOOR) && block.RecPos.Y < hero.HitboxBody.Y && block.RecPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((block.MyType == Block.typeBlock.FLOOR2 || block.MyType == Block.typeBlock.FLOOR) && block.RecPos.Y < hero.HitboxBody.Y && block.RecPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (block.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= block.RecPos.X - 20)
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
                        if (hero.Position.Y < block.RecPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            hero.CurrentFloorTile = new Vector2(block.RecPos.X, block.RecPos.Y);
                        }

                    }
                }
            }
            //check collision in level2
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var coin in coinList)
                {
                    if (coin.level == Coin.CoinLevelType.Level1)
                    {
                        coin.IsCollected = true;
                    }
                }

                foreach (var block in map.Blocks2)
                {

                    if (block.RecPos.Intersects(hero.HitboxBody) && (block.MyType is Block.typeBlock.FLOOR || block.MyType is Block.typeBlock.FLOOR2 || block.MyType is Block.typeBlock.SPIKE || block.MyType is Block.typeBlock.SPIKE2))
                    {
                        if (hero.Position.X + 27 >= block.RecPos.X || hero.Position.X - 27 <= block.RecPos.X && hero.Position.Y + 13 >= block.RecPos.Y)
                        {
                            if (hero.Position.Y <= block.RecPos.Y && block.MyType == Block.typeBlock.FLOOR2)
                            {

                                hero.CurrentFloorTile = new Vector2(block.RecPos.X + 50, block.RecPos.Y - 50);
                                hero.PreviousFloorTile = hero.CurrentFloorTile;
                            }
                            hero.Position = new Vector2(hero.Position.X, block.RecPos.Y - 50);

                            hero.IsFalling = false;
                            hero.HasJumped = false;
                        }

                        if ((block.MyType == Block.typeBlock.FLOOR2 || block.MyType == Block.typeBlock.FLOOR) && block.RecPos.Y < hero.HitboxBody.Y && block.RecPos.X >= hero.HitboxBody.X)
                        {
                            kb.SpeedRight = 0;
                        }
                        else if ((block.MyType == Block.typeBlock.FLOOR2 || block.MyType == Block.typeBlock.FLOOR) && block.RecPos.Y < hero.HitboxBody.Y && block.RecPos.X <= hero.HitboxBody.X)
                        {
                            kb.SpeedLeft = 0;
                        }
                        if (block.MyType == Block.typeBlock.SPIKE)
                        {
                            if (hero.Position.X >= block.RecPos.X - 20)
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
                        if (hero.Position.Y < block.RecPos.Bottom && hero.Position.Y < hero.CurrentFloorTile.Y)
                        {
                            hero.CurrentFloorTile = new Vector2(block.RecPos.X, block.RecPos.Y);
                        }

                    }
                }
            }
            //Check Enemy collision
            foreach (var enemy in enemyList)
            {
                if (Game1.currentState == Game1.GameStates.Level1 && enemy.level == Enemy.Level.Level1)
                {
                    if (hero.HitboxBody.Intersects(enemy.hitboxBody) && hero.Position.Y <= enemy.Position.Y - 15)
                    {
                        enemy.IsAlive = false;

                        score.ScoreValue += enemy.scoreValue;
                        enemy.scoreValue = 0;
                    }
                    else if (hero.HitboxBody.Intersects(enemy.hitboxBody) && enemy.IsAlive && hero.Vulnerable)
                    {
                        hero.Position -= new Vector2(100, 0);
                        hero.Health -= 1;
                        hero.Hit = true;
                    }
                }
                if (Game1.currentState == Game1.GameStates.Level2 && enemy.level == Enemy.Level.Level2)
                {
                    if (hero.HitboxBody.Intersects(enemy.hitboxBody) && hero.Position.Y <= enemy.Position.Y - 15)
                    {
                        enemy.IsAlive = false;

                        score.ScoreValue += enemy.scoreValue;
                        enemy.scoreValue = 0;
                    }
                    else if (hero.HitboxBody.Intersects(enemy.hitboxBody) && enemy.IsAlive && hero.Vulnerable)
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
                foreach (var enemy in enemyList)
                {
                    enemy.IsAlive = true;
                    enemy.scoreValue = 1;
                }
                foreach (var coin in coinList)
                {

                    coin.IsCollected = false;
                    coin.CoinValue = 1;

                }
            }
            if (Game1.currentState == Game1.GameStates.Level1)
            {
                foreach (var coin in coinList)
                {
                    if (hero.Hitbox.Intersects(coin.Hitbox) && coin.level == Coin.CoinLevelType.Level1)
                    {
                        coin.IsCollected = true;
                        hero.CoinsLevel1 += coin.CoinValue;
                        coin.CoinValue = 0;
                    }
                }
            }
            if (Game1.currentState == Game1.GameStates.Level2)
            {
                foreach (var coin in coinList)
                {

                    if (hero.Hitbox.Intersects(coin.Hitbox) && coin.level == Coin.CoinLevelType.Level2)
                    {
                        if (coin.level == Coin.CoinLevelType.Level2 && Game1.currentState == Game1.GameStates.Level2)
                        {
                            coin.IsCollected = true;
                            hero.CoinsLevel2 += coin.CoinValue;
                            coin.CoinValue = 0;
                        }

                    }
                }
            }
            //check door collision
            foreach (var door in doors)
            {

                if (door.Hitbox.Intersects(hero.HitboxBody) && door.IsOpened)
                {
                    if (door.Level == Door.DoorLevel.Level1)
                    {
                        Game1.currentState = Game1.GameStates.Level2;
                        hero.Position = new Vector2(0, 400);

                    }
                    if (door.Level == Door.DoorLevel.Level2 && door.IsOpened)
                    {
                        Game1.currentState = Game1.GameStates.WinningScreen;
                    }

                }
            }
        }
    }
}
