using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgProject
{
    public class Platform
    {
        public Texture2D platformTexture;
        public Rectangle platformRect;
        public Vector2 platformPos;
        public Platform(Texture2D platTexture, Vector2 position)
        {
            platformTexture = platTexture;
            platformPos = position;
        }

        public Rectangle GetRect() 
        {
            platformRect = new Rectangle((int)platformPos.X, (int)platformPos.Y-1, platformTexture.Width, platformTexture.Height+1);
            return platformRect;
        }
        public Rectangle GetLeftRect()
        {
            platformRect = new Rectangle((int)platformPos.X-1, (int)platformPos.Y, 10, platformTexture.Height);
            return platformRect;
        }
        public Rectangle GetRightRect()
        {
            platformRect = new Rectangle((int)platformPos.X + platformTexture.Width-10, (int)platformPos.Y, 10, platformTexture.Height);
            return platformRect;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D deco)
        {
            //ritar platform + sten
            spriteBatch.Draw(platformTexture, platformPos, Color.White);
            spriteBatch.Draw(deco, new Vector2(platformPos.X+20, platformPos.Y-deco.Height), Color.White);

            // TODO: Add your drawing code here

        }
    }
}
