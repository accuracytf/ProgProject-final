using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player player;
        public List<Platform> platlvl1 = new List<Platform>();
        public List<Platform> platlvl2 = new List<Platform>();
        public List<Platform> platlvl3 = new List<Platform>();
        int width, heigth;
        public static int level = 1;
        Sign sign, sign2;
        Texture2D background;
        bool camHasChanged;
        public static bool showText;
        public static SpriteFont font;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            width = 1280;
            heigth = 720;


            _graphics.PreferredBackBufferHeight = heigth;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Sunny Cloud Background");
            font = Content.Load<SpriteFont>("Font");
            //gör spelaren mindre!!!!!
            player = new(Content.Load<Texture2D>("Character"), Content.Load<Texture2D>("cJumping"), Content.Load<Texture2D>("cCharging"), new Vector2(50, 720 - Content.Load<Texture2D>("Character").Height));
            
            
            for(int i = 0; i < 5; i++)
            {
                if(i % 2 == 0)
                {
                    Platform p = new(Content.Load<Texture2D>("GrPlatform"), new Vector2(200 + (i * 100), 500 - (100 * i)));
                    platlvl1.Add(p);
                }
                
            }

            //level 2
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(200, 200)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(400, 400)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(800, 600)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(600, -25)));

            //level 3
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(500, 150)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(800, 410)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(600, 695)));

            //skylt 
            sign = new Sign(Content.Load<Texture2D>("sign2"), new Vector2(520, 150- Content.Load<Texture2D>("sign2").Height));
            sign2 = new Sign(Content.Load<Texture2D>("sign2"), new Vector2(100, 720 - Content.Load<Texture2D>("sign2").Height));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Debug.WriteLine(level);
            camHasChanged = false;
            if(level == 1)
                player.CollisionCheck(platlvl1);
            else if(level == 2)
                player.CollisionCheck(platlvl2);
            else if (level == 3)
                player.CollisionCheck(platlvl3);


            //kamera upp ner
            if (player.playerPos.Y + player.playerTexture.Height < 0)
            {
                if (!camHasChanged)
                {
                    level++;
                    camHasChanged=true;
                }
                player.playerPos.Y = 720 - player.playerTexture.Height;
            }
            if (player.playerPos.Y + player.playerTexture.Height > 720 && level != 1)
            {
                if (!camHasChanged)
                {
                    level--;
                    camHasChanged = true;
                }
                player.playerPos.Y = 0 - player.playerTexture.Height;
            }

            if (player.GetRect().Intersects(sign2.GetRect()) && ks.IsKeyDown(Keys.E) || player.GetRect().Intersects(sign.GetRect()) && ks.IsKeyDown(Keys.E))
                showText = true;
            else
                showText = false;
            player.Update();

            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            player.Draw(_spriteBatch);
            if (level == 1)
            {
                sign2.Draw(_spriteBatch);
                foreach (Platform p in platlvl1)
                    p.Draw(_spriteBatch);
            }
            else if (level == 2)
                foreach (Platform p in platlvl2)
                    p.Draw(_spriteBatch);
            else if (level == 3)
            {
                sign.Draw(_spriteBatch);
                foreach (Platform p in platlvl3)
                    p.Draw(_spriteBatch);
            }
            if(showText)
                if(level == 1)
                    Sign.SignText(_spriteBatch,new Vector2 (sign2.signPos.X+55, sign2.signPos.Y + 15), "A - Walk left , D - Walk right, SPACE - Jump, E - Interact");
                else if (level == 3)
                    Sign.SignText(_spriteBatch, new Vector2(sign.signPos.X -480, sign.signPos.Y + 15), "Congratulations, you made it to the top");

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}