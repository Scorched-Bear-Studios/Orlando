using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using TiledSharp;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;

using System.Collections.Generic;
using System;


// using Comora;

namespace Orlando
{



    enum Dir
    {
        Up,
        Down,
        Left,
        Right

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //private Camera camera;
        private Player player;
        private Enemy enemy;
        private TmxMap map;
        private TileMapManager mapManager;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;



        private Texture2D hearts;

        private List<Enemy> enemies = new List<Enemy>(10);


        Texture2D orlandoSprite;
        Texture2D orlandoWalkD; //down
        Texture2D orlandoWalkU; //up
        Texture2D orlandoWalkR; //right
        Texture2D orlandoWalkL; //left
        public Texture2D arrow;








        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 960;
            // this.camera = new Camera(graphics.GraphicsDevice);
            graphics.ApplyChanges();

            player = new Player(GraphicsDevice, Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // map = new TmxMap("Content/map.tmx");
            // string tilesetName = map.Tilesets[0].Name.ToString();
            // Console.WriteLine($"Looking for tileset in {tilesetName}...");
            // string tilesetLocation = $"Assets/{tilesetName}";
            // var tileset = Content.Load<Texture2D>(tilesetLocation);
            // if (tileset == null)
            // {
            //     throw new Exception($"Could not find tileset at {tilesetLocation}");
            // }
            // var tileWidth = map.Tilesets[0].TileWidth;
            // var tileHeight = map.Tilesets[0].TileHeight;
            // var TileSetTilesWide = tileset.Width / tileWidth;
            // mapManager = new TileMapManager(spriteBatch,map,tileset,TileSetTilesWide,tileWidth,tileHeight);

            orlandoSprite = Content.Load<Texture2D>("Player/orlando");
            orlandoWalkD = Content.Load<Texture2D>("Player/owalkd");
            orlandoWalkU = Content.Load<Texture2D>("Player/owalku");
            orlandoWalkR = Content.Load<Texture2D>("Player/owalkr");
            orlandoWalkL = Content.Load<Texture2D>("Player/owalkl");
            arrow = Content.Load<Texture2D>("Player/arrow");
            //enemy = new Enemy(Content.Load<Texture2D>("Enemy/skeleton"), new Vector2(400, 400), 150);
            // arrow = new Arrow(Content.Load<Texture2D>("Player/arrow2"));
            _tiledMap = Content.Load<TiledMap>("Assets/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);



            CreateXEnemies(10);


            player.animations[0] = new SpriteAnimation(orlandoWalkL, 4, 10);
            player.animations[1] = new SpriteAnimation(orlandoWalkR, 4, 10);
            player.animations[2] = new SpriteAnimation(orlandoWalkD, 4, 10);
            player.animations[3] = new SpriteAnimation(orlandoWalkU, 4, 10);

            // set the origin to the middle of the image, so it moves around 16x16
            for (int i = 0; i < 4; i++)
            {
                player.animations[i].Origin = new Vector2(-16, -16);
            }


            player.anim = player.animations[0];

        }

        private void CreateXEnemies(int count)
        {
            Texture2D texture = Content.Load<Texture2D>("Enemy/skeleton");
            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                float targetSpeed = (float)r.NextDouble();
                if (targetSpeed < 0.7f)
                {
                    targetSpeed = 0.7f;
                }

                Enemy newEnemy = new Enemy(texture, new Vector2(
                    r.Next(0, graphics.PreferredBackBufferWidth),
                    r.Next(0, graphics.PreferredBackBufferHeight)),
                    150, Content, targetSpeed);

                enemies.Add(newEnemy);
            }
        }

        private void UpdateAllEnemies(GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(player, gameTime);
            }
        }

        private void DrawAllEnemies()
        {
            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            UpdateAllEnemies(gameTime);


            //this.camera.Position = player.new;
            //------------> this is the line of code that does not work
            //this.camera.Update(gameTime);



            foreach (Arrow arr in Arrow.arrows) // list contaninig all arrow objects
            {
                arr.Update(gameTime);


            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //draw background last

            // draw arrow
            foreach (Arrow arr in Arrow.arrows)
            {
                // arrow.Draw(spriteBatch);

                spriteBatch.Draw(arrow, new Vector2(arr.Position.X - 50, arr.Position.Y - 50), null, Color.White);
            }

            // draw the player
            player.Draw(spriteBatch);

            DrawAllEnemies();

            // mapManager.Draw(Matrix.Identity, spriteBatch);

            _tiledMapRenderer.Draw();
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}



// using Comora;
#region Original
/*namespace Orlando
    {
        enum Dir
        {
            Up,
            Down,
            Left,
            Right

        }
        public class Game1 : Game
        {
            private GraphicsDeviceManager graphics;
            private SpriteBatch spriteBatch;
            //private Camera camera;
            Player player;
            Enemy enemy;
            //Arrow _arrow;
            TileMapManager mapManager;
            HealthBar healthBar;
            private Texture2D hearts;
            public Texture2D arrow;




            public TiledMap _tiledMap;
            public TiledMapRenderer _tiledMapRenderer;


            Texture2D orlandoSprite;
            Texture2D orlandoWalkD; //down
            Texture2D orlandoWalkU; //up
            Texture2D orlandoWalkR; //right
            Texture2D orlandoWalkL; //left
          
            //Texture2D levelone;







        public Game1()
            {
                graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
            }

            protected override void Initialize()
            {
            player = new Player(GraphicsDevice,Content);


            graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 960;
               // graphics.IsFullScreen = true; // play in full screen


               // this.camera = new Camera(graphics.GraphicsDevice);
                graphics.ApplyChanges();


                base.Initialize();
            }

            protected override void LoadContent()
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);


                orlandoSprite = Content.Load<Texture2D>("Player/orlando");
                orlandoWalkD = Content.Load<Texture2D>("Player/owalkd");
                orlandoWalkU = Content.Load<Texture2D>("Player/owalku");
                orlandoWalkR = Content.Load<Texture2D>("Player/owalkr");
                orlandoWalkL = Content.Load<Texture2D>("Player/owalkl");
                arrow = Content.Load<Texture2D>("Player/arrow");
            
                enemy = new Enemy(Content.Load<Texture2D>("Enemy/skeleton"), new Vector2(400, 400), 150);
            // arrow = new Arrow(Content.Load<Texture2D>("Player/arrow2"));
                _tiledMap = Content.Load<TiledMap>("Assets/map");
                _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);


                player.animations[0] = new SpriteAnimation(orlandoWalkL, 4, 10);
                player.animations[1] = new SpriteAnimation(orlandoWalkR, 4, 10);
                player.animations[2] = new SpriteAnimation(orlandoWalkD, 4, 10);
                player.animations[3] = new SpriteAnimation(orlandoWalkU, 4, 10);

                for (int i = 0; i < 4; i++)
                {
                player.animations[i].Origin = new Vector2(-16, -16);
                }


                player.anim = player.animations[0];
           

            }

            protected override void Update(GameTime gameTime)
            { 
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                player.Update(gameTime);
                enemy.Update(player, gameTime);
              
            

                //this.camera.Position = player.new;
                //------------> this is the line of code that does not work
                //this.camera.Update(gameTime);



                foreach (Arrow arr in Arrow.arrows) // list contaninig all arrow objects
                {
                    arr.Update(gameTime);


                }

            
            base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);


                spriteBatch.Begin();

              
            // draw arrow
            foreach (Arrow arr in Arrow.arrows)
                {
                //_arrow.Draw(spriteBatch);

                   spriteBatch.Draw(arrow, new Vector2(arr.Position.X - 25, arr.Position.Y - 25), null, Color.White);
                }
                player.anim.Draw(spriteBatch);
                enemy.Draw(spriteBatch);
                //mapManager.Draw(spriteBatch, Matrix.Identity);
               _tiledMapRenderer.Draw();
                spriteBatch.End();


                base.Draw(gameTime);
            }
        }
    }*/

#endregion
