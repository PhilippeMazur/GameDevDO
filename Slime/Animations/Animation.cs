using Microsoft.Xna.Framework;
using Slime;
using Slime.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Animations
{
    class Animation : Game1
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        private int counter2 = 2;
        private int counter3 = 4;
        static Random r = new Random();
        private double secondCounter = 0;
        private int fps = 2;
        public bool goingLeft = false;
        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }
        
        public void Update(GameTime gameTime, KeyboardReader kb)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (kb.AnimationState == KeyboardReader.states.Idle)
            {
                goingLeft = false;               
                if (secondCounter >= 1000d / fps)
                {
                    secondCounter = 0;
                    CurrentFrame = frames[counter];
                    counter++;
                    if (counter > 1)
                    {
                        counter = 0;
                    }
                }
            }
            if (kb.AnimationState == KeyboardReader.states.RunningRight)
            {
                goingLeft = false;                
                if (secondCounter >= 1000d / fps)
                {
                    secondCounter = 0;
                    CurrentFrame = frames[counter2];
                    counter2++;
                    if (counter2 > 3)
                    {
                        counter2 = 2;
                    }
                }
            }
            if(kb.AnimationState == KeyboardReader.states.RunningLeft)
            {
                goingLeft = true;
                

                if (secondCounter >= 1000d / fps)
                {
                    secondCounter = 0;
                    CurrentFrame = frames[counter2];
                    counter2++;
                    if (counter2 > 3)
                    {
                        counter2 = 2;
                    }
                }
            }
            if (kb.AnimationState == KeyboardReader.states.Jumping)
            {
                goingLeft = false;                
                if (secondCounter >= 1000d / fps)
                {
                    secondCounter = 0;
                    CurrentFrame = frames[counter3];
                    counter3++;
                    if (counter3 > 5)
                    {
                        counter3 = 4;
                    }
                }
            }     
        }
        
        public void Update(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (secondCounter >= 1000d / fps)
            {
                secondCounter = 0;
                CurrentFrame = frames[counter];
                counter++;
                if (counter >= frames.Count)
                {
                    counter = 0;
                }
            }
        }

    }
}
