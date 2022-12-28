using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime.UI
{
    public class StartMenuScreen
    {
        private Texture2D texture;
        private Rectangle position;
        Text startScreenText = new Text("Press 'Enter' to START", new Vector2(250, 450));


        public StartMenuScreen(Texture2D texturein, Rectangle positionin)
        {
            texture = texturein;
            position = positionin;
        }
        public void Draw(SpriteFont font)
        {
            Game1._spriteBatch.Draw(texture, position, Color.White);
            startScreenText.Draw(font);
        }
        public void Update(GameTime gametime)
        {
            startScreenText.Update(gametime);
        }
    }
}
