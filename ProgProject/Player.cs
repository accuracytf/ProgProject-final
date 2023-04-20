using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProject
{
    public class Player
    {

        public Texture2D playerTexture_Left, playerTexture_Right;
        public Vector2 playerPos;
        public Vector2 velocity;
        public Rectangle playerTopRect,playerBotRect;
        public bool isGrounded;
        bool movingLeft;
        float jumpStrength = 6;
        

        public Player(Texture2D pTexture_Left, Texture2D pTexture_Right, Vector2 position)
        {
            this.playerTexture_Left = pTexture_Left;
            this.playerTexture_Right = pTexture_Right;
            this.playerPos = position;
            isGrounded = false;
        }



        public void CollisionCheck(List<Platform> platlist)
        {
            bool intersected = false;
            foreach (Platform p in platlist)
            {
                if (GetBotRect().Intersects(p.GetRect()))
                {
                    intersected = true;
                    playerPos.Y = p.GetRect().Top - playerTexture_Right.Height + 1;
                }
                else if (GetTopRect().Intersects(p.GetRect()))
                {
                    velocity.Y = 1;
                }
                isGrounded = intersected;
            }

            
           
        }
        public Rectangle GetTopRect()
        {
            playerTopRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, playerTexture_Right.Width, 2);
            return playerTopRect;
        }
        public Rectangle GetBotRect()
        {
            playerBotRect = new Rectangle((int)playerPos.X, (int)playerPos.Y + playerTexture_Right.Height-2, playerTexture_Right.Width, 2);
            return playerBotRect;
        }
        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            
            playerPos += velocity;
            velocity.X = 0;
            if (ks.IsKeyDown(Keys.A))
            {
                velocity.X = -4f;
                movingLeft = true;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                velocity.X = 4f;
                movingLeft = false;
            }
            if (playerPos.Y + playerTexture_Right.Height >= 720 && Game1.level == 1)
            {
                playerPos.Y = 720 - playerTexture_Right.Height;
                isGrounded = true;
            }
            if (ks.IsKeyDown(Keys.Space) && isGrounded)
            {
                jumpStrength += 0.195f;
            }
            if (!isGrounded)
            {
                float i = 1;
                velocity.Y += 0.52f * i;
            }


            
            //hopp sak
            if (isGrounded)
            {
                if (jumpStrength > 6 && !ks.IsKeyDown(Keys.Space))
                {
                    if (jumpStrength > 15)
                        jumpStrength = 15;
                    playerPos.Y -= jumpStrength * 2;
                    velocity.Y = -jumpStrength;
                    isGrounded = false;
                }
                else
                    velocity.Y = 0f;
            }

            if (!ks.IsKeyDown(Keys.Space))
            {
                jumpStrength = 6;
            }

            //inte köra ut ur skärmen

            if (playerPos.X < 0)
            {
                playerPos.X = 0;
            } 
            
            if (playerPos.X + playerTexture_Right.Width > 1280)
            {
                playerPos.X = 1280 - playerTexture_Right.Width;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (movingLeft)
                spriteBatch.Draw(playerTexture_Left, playerPos, Color.White);
            if (!movingLeft)
                spriteBatch.Draw(playerTexture_Right, playerPos, Color.White);


            // TODO: Add your drawing code here

        }


    }
}
