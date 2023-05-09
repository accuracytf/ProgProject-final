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

        public Texture2D playerTexture, jumpTexture,charTexture;
        public Vector2 playerPos;
        public Vector2 velocity;
        public Rectangle playerTopRect,playerBotRect, playerRect;
        public bool isGrounded;
        bool movingLeft;
        bool chargingJump = false;
        float jumpStrength = 6;
        

        public Player(Texture2D pTexture,Texture2D jTexture, Texture2D cTexture, Vector2 position)
        {
            playerTexture = pTexture;
            jumpTexture = jTexture;
            charTexture = cTexture;
            playerPos = position;
            isGrounded = true;
        }



        public void CollisionCheck(List<Platform> platlist)
        {
            bool intersected = false;
            bool touchingside = true;
            foreach (Platform p in platlist)
            {
                
                
                
                if (GetTopRect().Intersects(p.GetRect()))
                {
                    velocity.Y = 1;
                    touchingside = false;
                }
                //höger vänster
                if (GetRect().Intersects(p.GetLeftRect()))
                {
                    velocity.X = -1;
                    touchingside = false;
                }
                if (GetRect().Intersects(p.GetRightRect()))
                {
                    velocity.X = 1;
                    touchingside = false;
                }


                //upp ner
                if(touchingside)
                {
                    if (GetBotRect().Intersects(p.GetRect()))
                    {
                        intersected = true;
                        playerPos.Y = p.GetRect().Top - playerTexture.Height + 1;
                    }
                    if ((playerPos.Y + playerTexture.Height >= 720) == false)
                        isGrounded = intersected;
                }
                
                
                

            }

        }
        public Rectangle GetRect()
        {
            playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y , playerTexture.Width, playerTexture.Height);
            return playerRect;
        }
        public Rectangle GetTopRect()
        {
            playerTopRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, playerTexture.Width, 2);
            return playerTopRect;
        }
        public Rectangle GetBotRect()
        {
            playerBotRect = new Rectangle((int)playerPos.X, (int)playerPos.Y + playerTexture.Height-2, playerTexture.Width, 2);
            return playerBotRect;
        }
        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            
            playerPos += velocity;
            if (isGrounded)
                velocity.X = 0;
            Debug.WriteLine(isGrounded);
            Debug.WriteLine(jumpStrength);
            //movement
            if (ks.IsKeyDown(Keys.A) && isGrounded && !chargingJump)
            {
                velocity.X = -4f;
                movingLeft = true;
            }
            if (ks.IsKeyDown(Keys.D) && isGrounded && !chargingJump)
            {
                velocity.X = 4f;
                movingLeft = false;
            }
            //groundedcheck
            if (playerPos.Y + playerTexture.Height >= 720 && Game1.level == 1)
            {
                isGrounded = true;
                playerPos.Y = 720 - playerTexture.Height;
                
            }
            if (ks.IsKeyDown(Keys.Space) && isGrounded)
            {
                chargingJump = true;
                jumpStrength += 0.215f;
            }
            if (!isGrounded)
            {
                velocity.Y += 0.52f;
            }


            
            //hopp sak
            if (isGrounded)
            {
                if (jumpStrength > 6 && !ks.IsKeyDown(Keys.Space))
                {
                    chargingJump = false;
                    if (jumpStrength > 17)
                    {
                        jumpStrength = 17;
                    }
                    playerPos.Y -= jumpStrength * 2;
                    velocity.Y = -jumpStrength;
                    isGrounded = false;
                    if (movingLeft)
                    {
                        velocity.X = -4f;
                    }
                    if (!movingLeft)
                    {
                        velocity.X = 4f;
                    }
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
            
            if (playerPos.X + playerTexture.Width > 1280)
            {
                playerPos.X = 1280 - playerTexture.Width;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (movingLeft)
                if(!isGrounded)
                    spriteBatch.Draw(jumpTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                else if(chargingJump)
                    spriteBatch.Draw(charTexture, new Vector2(playerPos.X,playerPos.Y+(playerTexture.Height-charTexture.Height)), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(playerTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            if (!movingLeft)
                if (!isGrounded)
                    spriteBatch.Draw(jumpTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else if(chargingJump)
                    spriteBatch.Draw(charTexture, new Vector2(playerPos.X, playerPos.Y + (playerTexture.Height - charTexture.Height)), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(playerTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            

            // TODO: Add your drawing code here

        }


    }
}
