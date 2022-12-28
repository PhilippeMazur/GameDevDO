using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Animations;
using Slime.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    public class WinningScreen
    {
        Texture2D texture;
        private Rectangle position = new Rectangle(0, 0, 1000, 700);
        Text winningScreenText = new Text("Press 'M' to go to the MENU", new Vector2(250, 450));

        public WinningScreen(Texture2D texturein, Rectangle positionin)
        {
            texture = texturein;
            position = positionin;
        }
        public void Draw(SpriteFont font)
        {
            Game1._spriteBatch.Draw(texture, position, Color.White);
            winningScreenText.Draw(font);
        }
        public void Update(GameTime gameTime)
        {
            winningScreenText.Update(gameTime);
        }
    }
}
