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
        public List<Platform> platforms = new List<Platform>();
        public List<Platform> platforms2 = new List<Platform>();
        int width, heigth;
        public static int level = 1;
        bool camHasChanged;
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
            
            //gör spelaren mindre!!!!!
            player = new(Content.Load<Texture2D>("Character_Left"), Content.Load<Texture2D>("Character_Right"), Content.Load<Texture2D>("Jumping_Left"), Content.Load<Texture2D>("Jumping_Right"), new Vector2(50, 720 - Content.Load<Texture2D>("Character_Right").Height));
            
            
            for(int i = 0; i < 5; i++)
            {
                Platform p = new(Content.Load<Texture2D>("GrPlatform"),new Vector2(200 + (i*100), 500 - (100* i)));
                platforms.Add(p);
            }

            platforms2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(200, 200)));
            platforms2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(300, 300)));
            platforms2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(400, 400)));
            platforms2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(700, 600)));
        
      

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Debug.WriteLine(level);
            camHasChanged = false;
            if(level == 1)
            {
                player.CollisionCheck(platforms);
            }
            else if(level == 2)
            {
                player.CollisionCheck(platforms2);
            }
            

            if (player.playerPos.Y + player.playerTexture_Right.Height < 0)
            {
                if (!camHasChanged)
                {
                    level++;
                    camHasChanged=true;
                }
                player.playerPos.Y = 720 - player.playerTexture_Right.Height;
            }
            if (player.playerPos.Y + player.playerTexture_Right.Height > 720 && level != 1)
            {
                if (!camHasChanged)
                {
                    level--;
                    camHasChanged = true;
                }
                player.playerPos.Y = 0 - player.playerTexture_Right.Height;
            }

            player.Update();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            if (level == 1)
            {
                foreach (Platform p in platforms)
                    p.Draw(_spriteBatch);
            }
            else
                foreach (Platform p in platforms2)
                    p.Draw(_spriteBatch);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}