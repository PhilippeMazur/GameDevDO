using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Animations;
using Slime.Interfaces;
using Slime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.GameScreen
{
    internal class StartScreen : Screen, IWritable
    {
        public Text Text { get; set; }
        public StartScreen(Texture2D texturein, Rectangle positionin, Animation Animationin, Text textin) : base(texturein, positionin, Animationin)
        {
            Text  = textin;
        }
        public override void Draw()
        {
            base.Draw();
            Text.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Text.Update(gameTime);
        }
    }
}
