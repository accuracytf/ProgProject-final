using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgProject
{
    public class Player
    {

        public Texture2D playerTexture, jumpTexture, charTexture;
        public Vector2 playerPos;
        public Vector2 velocity;
        public Rectangle playerTopRect, playerBotRect, playerRect, playerLeftRect, playerRightRect;
        public bool isGrounded;
        bool movingLeft;
        bool chargingJump = false;
        float jumpStrength = 6;


        public Player(Texture2D pTexture, Texture2D jTexture, Texture2D cTexture, Vector2 position)
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

                //höger vänster
                //getSpecificRect(bool left, bool right, bool top, bool bottom)
                if (GetRect().Intersects(p.GetLeftRect()) && !GetRect("bottom").Intersects(p.GetLeftRect()))
                {
                    velocity.X = -1;
                    touchingside = false;
                }
                if (GetRect().Intersects(p.GetRightRect()) && !GetRect("bottom").Intersects(p.GetRightRect()))
                {
                    velocity.X = 1;
                    touchingside = false;
                }

                if (GetRect("top").Intersects(p.GetRect()))
                {
                    velocity.Y = 1.5f;
                    touchingside = false;
                }

                //upp ner
                if (touchingside)
                {
                    if (GetRect("bottom").Intersects(p.GetRect()))
                    {
                        intersected = true;
                        playerPos.Y = p.GetRect().Top - playerTexture.Height + 1;
                    }
                    if ((playerPos.Y + playerTexture.Height >= 720) == false)
                        isGrounded = intersected;
                }

            }
        }
#nullable enable
        public Rectangle GetRect(string? str = null)
        {
            Rectangle defPlayerRect = new((int)playerPos.X, (int)playerPos.Y, playerTexture.Width, playerTexture.Height);
            if (str != null)
            {
                switch (str)
                {
                    case "top":
                        return new Rectangle((int)playerPos.X, (int)playerPos.Y, playerTexture.Width, 2);
                    case "bottom":
                        return new Rectangle((int)playerPos.X + 5, (int)playerPos.Y + playerTexture.Height + 5, playerTexture.Width - 10, 2);
                    case "left":
                        return new Rectangle((int)playerPos.X - 2, (int)playerPos.Y, 2, playerTexture.Height);
                    case "right":
                        return new Rectangle((int)playerPos.X + playerTexture.Width + 2, (int)playerPos.Y, 2, playerTexture.Height);
                    default:
                        return defPlayerRect;
                }
            }
            else return defPlayerRect;
        }

#nullable disable

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
                if ((jumpStrength >= 17  && ks.IsKeyDown(Keys.Space)) ||  (jumpStrength > 6 && !ks.IsKeyDown(Keys.Space)))
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
                    jumpStrength = 6;
                }
                else
                    velocity.Y = 0f;
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
                if (!isGrounded)
                    spriteBatch.Draw(jumpTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                else if (chargingJump)
                    spriteBatch.Draw(charTexture, new Vector2(playerPos.X, playerPos.Y + (playerTexture.Height - charTexture.Height)), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(playerTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            if (!movingLeft)
                if (!isGrounded)
                    spriteBatch.Draw(jumpTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                else if (chargingJump)
                    spriteBatch.Draw(charTexture, new Vector2(playerPos.X, playerPos.Y + (playerTexture.Height - charTexture.Height)), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(playerTexture, playerPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

            // TODO: Add your drawing code here

        }
    }
}
