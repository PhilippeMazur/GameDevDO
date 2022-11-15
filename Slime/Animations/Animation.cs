using Microsoft.Xna.Framework;
using Slime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Animations
{
    class Animation : Game1
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private List<AnimationFrame> framesRun;
        private int counter;
        static Random r = new Random();
        private double secondCounter = 0;
        private int fps = 2;
        private int fps2 = 15;      
        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
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
