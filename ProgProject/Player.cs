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

        public Texture2D playerTexture_Left, playerTexture_Right, jumpTexture_Left, jumpTexture_Right;
        public Vector2 playerPos;
        public Vector2 velocity;
        public Rectangle playerTopRect,playerBotRect;
        public bool isGrounded;
        bool movingLeft;
        bool chargingJump = false;
        float jumpStrength = 6;
        

        public Player(Texture2D pTexture_Left, Texture2D pTexture_Right,Texture2D jTexture_Left, Texture2D jTexture_Right, Vector2 position)
        {
            playerTexture_Left = pTexture_Left;
            playerTexture_Right = pTexture_Right;
            jumpTexture_Left = jTexture_Left;
            jumpTexture_Right = jTexture_Right;
            playerPos = position;
            isGrounded = true;
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
                if((playerPos.Y + playerTexture_Right.Height >= 720) == false)
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
            if (playerPos.Y + playerTexture_Right.Height >= 720 && Game1.level == 1)
            {
                Debug.WriteLine("hej2");
                isGrounded = true;
                Debug.WriteLine(isGrounded + "hej");
                playerPos.Y = 720 - playerTexture_Right.Height;
                
            }
            if (ks.IsKeyDown(Keys.Space) && isGrounded)
            {
                chargingJump = true;
                jumpStrength += 0.195f;
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
                    Debug.WriteLine("hej");
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
            
            if (playerPos.X + playerTexture_Right.Width > 1280)
            {
                playerPos.X = 1280 - playerTexture_Right.Width;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (movingLeft)
                if(!isGrounded)
                    spriteBatch.Draw(jumpTexture_Left, playerPos, Color.White);
                else
                    spriteBatch.Draw(playerTexture_Left, playerPos, Color.White);
            if (!movingLeft)
                if (!isGrounded)
                    spriteBatch.Draw(jumpTexture_Right, playerPos, Color.White);
                else
                    spriteBatch.Draw(playerTexture_Right, playerPos, Color.White);


            // TODO: Add your drawing code here

        }


    }
}
