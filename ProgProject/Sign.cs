using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProject
{
    public class Sign
    {
        public Texture2D signTexture;
        public Rectangle signRect;
        public Vector2 signPos;
        public Sign(Texture2D signTexture, Vector2 position)
        {
            this.signTexture = signTexture;
            signPos = position;
        }

        public Rectangle GetSignRect()
        {
            signRect = new Rectangle((int)signPos.X-50, (int)signPos.Y, (int)(signPos.X + signTexture.Width), signTexture.Height);
            return signRect;
        }
        public static void SignText(SpriteBatch spriteBatch, Vector2 pos, string text)
        {
            spriteBatch.DrawString(Game1.font,text ,pos , Color.Black);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(signTexture, signPos, Color.White);
            if (!Game1.showText)
                spriteBatch.DrawString(Game1.font, "Hold E ", new Vector2(signPos.X-3, signPos.Y-30), Color.Black);
            
            // TODO: Add your drawing code here

        }
    }
}
