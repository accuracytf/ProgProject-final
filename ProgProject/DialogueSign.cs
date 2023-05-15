using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProgProject
{
    internal class DialogueSign
    {
        public Vector2 _position;

        public string[] textList;
        public string textToShow;

        public int currentString;
        public int level;

        public float distance;
        float dissapearDistance;

        public bool showPromptText;
        public bool isActive;
        bool hasPressed = false;

        public DialogueSign(Vector2 pos, string[] textList, float distance, float dissapearDistance, int levelToShow)
        {
            this.currentString = 0;
            this.textToShow = textList[0];
            this.textList = textList;
            this._position = pos;
            this.distance = distance;
            this.dissapearDistance = dissapearDistance;
            this.showPromptText = false;
            this.isActive = false;
            this.level = levelToShow;

        }

        public void Update()
        {
            KeyboardState ks = Keyboard.GetState();
            if (!isActive && level == Game1.level)
            {
                if (getDistanceToPlayer(_position) < this.distance)
                {
                    showPromptText = true;
                }
                else
                {
                    showPromptText = false;
                }

                if (showPromptText && ks.IsKeyDown(Keys.E))
                {
                    isActive = true;
                    showPromptText = false;
                }
            }
            if (isActive)
            {
                if (ks.IsKeyDown(Keys.Back)) isActive = false;
                if (ks.IsKeyDown(Keys.Enter) && !hasPressed)
                {
                    {
                        currentString++;
                        if (currentString == textList.Length)
                        {
                            isActive = false;
                            textToShow = textList[0];
                            currentString = 0;
                        }
                        else
                        {
                            textToShow = textList[currentString];
                        }
                    }
                }
                else
                {
                    hasPressed = false;
                }
            }
            if(isActive && getDistanceToPlayer(_position) > dissapearDistance) 
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(showPromptText && !isActive)
            {
                spriteBatch.DrawString(Game1.font, "Press E", new Vector2(_position.X - 3, _position.Y - 30), Color.Black);
            }else if(isActive){
                spriteBatch.DrawString(Game1.font, textToShow, new Vector2(_position.X - 3, _position.Y - 30), Color.Black);
            }
        }

        private static float getDistanceToPlayer(Vector2 pos)
        {
            float a = Math.Abs(pos.X - Game1.player.playerPos.X);
            float b = Math.Abs(pos.Y - Game1.player.playerPos.Y);
            return (float)Math.Sqrt(a * a + b * b);
        }
    }
}
