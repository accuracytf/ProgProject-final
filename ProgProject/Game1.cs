using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ProgProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player player;
        public List<Platform> platlvl1 = new();
        public List<Platform> platlvl2 = new();
        public List<Platform> platlvl3 = new();
        public List<Platform> platlvl4 = new();
        public List<Platform> platlvl5 = new();
        int width, heigth;
        public static int level = 1;
        Sign sign1, signFinal, sign3;
        Texture2D background1, background2, background3, stone;
        public static SoundEffect jumpEffect;
        public static SoundEffect landingEffect;
        public static SoundEffect hitHeadEffect;
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
            //bakgrund
            background1 = Content.Load<Texture2D>("background_0");
            background2 = Content.Load<Texture2D>("background_1");
            background3 = Content.Load<Texture2D>("background_2");
            //font
            font = Content.Load<SpriteFont>("Font");
            //soundfx
            jumpEffect = Content.Load<SoundEffect>("Jump1");
            landingEffect = Content.Load<SoundEffect>("Landing");
            hitHeadEffect = Content.Load<SoundEffect>("HeadBonk");

            //player character
            player = new(Content.Load<Texture2D>("Character"), Content.Load<Texture2D>("cJumping"), Content.Load<Texture2D>("cCharging"), new Vector2(50, 720 - Content.Load<Texture2D>("Character").Height));
            
            //level 1
            for(int i = 0; i < 5; i++)
            {
                if(i % 2 == 0)
                {
                    Platform p = new(Content.Load<Texture2D>("GrPlatform"), new Vector2(200 + (i * 100), 500 - (100 * i)));
                    platlvl1.Add(p);
                }
                
            }
            stone = Content.Load<Texture2D>("Stone");
            //level 2
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(200, 200)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(400, 400)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(800, 600)));
            platlvl2.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(600, -25)));

            //level 3
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(500, 150)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(800, 410)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatform"), new Vector2(600, 695)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(70, -25)));
            platlvl3.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(250, -25)));

            //level 4
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(70, 695)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(250, 695)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(300, 460)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(500, 460)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(800, 460)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(990, 300)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(920, 170)));
            platlvl4.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(800, -25)));

            //level 5
            platlvl5.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(800, 690)));
            platlvl5.Add(new(Content.Load<Texture2D>("GrPlatformShort"), new Vector2(500, 500)));

            //skylt 
            sign1 = new Sign(Content.Load<Texture2D>("sign2"), new Vector2(100, 720 - Content.Load<Texture2D>("sign2").Height));
            signFinal = new Sign(Content.Load<Texture2D>("sign2"), new Vector2(520, 500- Content.Load<Texture2D>("sign2").Height));
            sign3 = new Sign(Content.Load<Texture2D>("sign2"), new Vector2(1010, 300 - Content.Load<Texture2D>("sign2").Height));
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            camHasChanged = false;
            if(level == 1)
                player.CollisionCheck(platlvl1);
            else if(level == 2)
                player.CollisionCheck(platlvl2);
            else if (level == 3)
                player.CollisionCheck(platlvl3);
            else if (level == 4)
                player.CollisionCheck(platlvl4);
            else if (level == 5)
                player.CollisionCheck(platlvl5);


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
            showText = false;

            //hold e vid skylt = visa text
            if (ks.IsKeyDown(Keys.E))
            {
                if (level==1)
                    if (player.GetRect().Intersects(sign1.GetSignRect()))
                        showText = true;
                
                if (level == 4)
                    if (player.GetRect().Intersects(sign3.GetSignRect()))
                        showText = true;
                if (level == 5)
                    if (player.GetRect().Intersects(signFinal.GetSignRect()))
                        showText = true;

            }
            
            player.Update();

            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //bakgrund
            _spriteBatch.Draw(background1, new Vector2(0, 0), Color.White);
            //speciell bakgrund för lvl 1
            if(level == 1)
            {
                _spriteBatch.Draw(background2, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(background3, new Vector2(0, 0), Color.White);
            }
            
            
            //level draw + sign draw beronde på lvl
            if (level == 1)
            {
                sign1.Draw(_spriteBatch);
                foreach (Platform p in platlvl1)
                    p.Draw(_spriteBatch,stone);
            }
            else if (level == 2)
                foreach (Platform p in platlvl2)
                    p.Draw(_spriteBatch,stone);
            else if (level == 3)
                foreach (Platform p in platlvl3)
                    p.Draw(_spriteBatch, stone);
            else if (level == 4)
            {
                sign3.Draw(_spriteBatch);
                foreach (Platform p in platlvl4)
                    p.Draw(_spriteBatch, stone);
            }
            else if (level == 5)
            {
                signFinal.Draw(_spriteBatch);
                foreach (Platform p in platlvl5)
                    p.Draw(_spriteBatch, stone);
            }
             
            player.Draw(_spriteBatch);
            //skylt text
            if (showText)
                if (level == 1)
                    Sign.SignText(_spriteBatch, new Vector2(sign1.signPos.X + 57, sign1.signPos.Y + 13), "A - Walk left , D - Walk right, SPACE - Jump, E - Interact");
                else if (level == 4)
                    Sign.SignText(_spriteBatch, new Vector2(sign3.signPos.X+5, sign3.signPos.Y-50), " :) ");
                else if (level == 5)
                    Sign.SignText(_spriteBatch, new Vector2(signFinal.signPos.X - 480, signFinal.signPos.Y + 15), "Congratulations, you made it to the top");
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}