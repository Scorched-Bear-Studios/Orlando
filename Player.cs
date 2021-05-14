 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;


/// <summary>
/// Reference: Monogame Udemy Course, Kyle Schaub
/// </summary>
namespace Orlando
{
    class Player
    {


        private const int MAX_HEALTH = 100;
        private const int MAX_LIVES = 5;

        private Vector2 position = new Vector2(150, 900);
        private int speed = 250;
        private Dir direction = Dir.Down;
        private bool isMoving = false;
        private KeyboardState kStateOld = Keyboard.GetState();



        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];


        public Color playerColour = Color.Aqua;

        private float health = MAX_HEALTH;              // player's health
        private float lives = MAX_LIVES;                        // player's lives

        private HealthBar healthBar;

        public Player(GraphicsDevice device, ContentManager contentManager)
        {
            // create new player health bar
            healthBar = new HealthBar(contentManager, playerColour);
        }


        public Vector2 Position
        {
            get
            {
                return position;

            }
        }

        public void StartGame(bool isDead, Vector2 position) //start game position
        {
            if (!isDead)
            {
                
            }
        }
        public void HurtPlayer(float byHowMuch = 12.5f)     // 12.5 is a quarter of 4 hearts
        {
            health -= byHowMuch;

            if (health < 0)
            {
                PlayerIsDead();
            }
        }

        /// <summary>
        /// Player has died. Remove life and do something accordingly.
        /// </summary>
        private void PlayerIsDead()
        {
            // remove a life, etc
            lives--;

            if (lives < 0)
            {
                // player is completely dead, end of game. Show menu.
                return;
            }

            // do something here, like move the player to the start position
            // reset their health to max health
            health = MAX_HEALTH;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            // draw the anim
            anim.Draw(spritebatch);

            //spritebatch.DrawPoint(position, Color.Azure, 2);
            //spritebatch.DrawCircle(position, 50, 10, Color.Azure, 3);

            // draw the health bar slightly above the player
            Vector2 healthBarPosition = new Vector2(50, 30);
            healthBar.DrawHealthBar(spritebatch, healthBarPosition, health, MAX_HEALTH);
        }

        public void setX(float newX)
        {
            position.X = newX;
        }

        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (kState.IsKeyDown(Keys.A) && !kStateOld.IsKeyDown(Keys.A)) // code for enemy attack
            {
                HurtPlayer(12.5f); // hurt player half heart 
                Debug.WriteLine($"Reduced health to: {health}");
            }


            isMoving = false;

            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
            {
                direction = Dir.Right;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
            {
                direction = Dir.Left;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
            {
                direction = Dir.Up;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
            {
                direction = Dir.Down;
                isMoving = true;
            }

            if (isMoving) // check this if player stops moving
            {
                switch (direction)
                {
                    case Dir.Right:
                        if (position.X < 1240)
                            position.X += speed * dt;
                        break;
                    case Dir.Left:
                        if (position.X > 100) //this
                            position.X -= speed * dt;
                        break;
                    case Dir.Down:
                        if (position.Y < 900)
                            position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        if (position.Y > 100) // this
                            position.Y -= speed * dt;
                        break;
                }
            }

            switch (direction)
            {
                case Dir.Left:
                    anim = animations[0];
                    break;
                case Dir.Right:
                    anim = animations[1];
                    break;
                case Dir.Down:
                    anim = animations[2];
                    break;
                case Dir.Up:
                    anim = animations[3];
                    break;
            }



            anim.Position = new Vector2(position.X -50, position.Y -50);

            if (kState.IsKeyDown(Keys.Space))
                anim.setFrame(0);
            else if (isMoving)
                anim.Update(gameTime);
            else
                anim.setFrame(1);

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
                Arrow.arrows.Add(new Arrow(position, direction));

            kStateOld = kState;

        }
        #region CodeOne
        /*
        private Vector2 position = new Vector2(500, 300);
        private int speed = 150;
        private Dir direction = Dir.Down;
        private bool isMoving = false;
        private KeyboardState kStateOld = Keyboard.GetState();
        public bool isDead = false;
         



        public SpriteAnimation anim;
        public SpriteAnimation[] animations = new SpriteAnimation[4];




        public Vector2 Position
        {
            get
            {
                return position;

            }
        }



        public void setX(float newX)
        {
            position.X = newX;
        }

        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;





            isMoving = false;

            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
            {
                direction = Dir.Right;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
            {
                direction = Dir.Left;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
            {
                direction = Dir.Up;
                isMoving = true;
            }

            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
            {
                direction = Dir.Down;
                isMoving = true;
            }

            if (isMoving) // check this if player stops moving
            {
                switch (direction)
                {
                    case Dir.Right:
                        if (position.X < 1240)
                            position.X += speed * dt;
                        break;
                    case Dir.Left:
                        if (position.X > 100) //this
                            position.X -= speed * dt;
                        break;
                    case Dir.Down:
                        if (position.Y < 900)
                            position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        if (position.Y > 100) // this
                            position.Y -= speed * dt;
                        break;
                }
            }

            switch (direction)
            {
                case Dir.Left:
                    anim = animations[0];
                    break;
                case Dir.Right:
                    anim = animations[1];
                    break;
                case Dir.Down:
                    anim = animations[2];
                    break;
                case Dir.Up:
                    anim = animations[3];
                    break;
            }



            anim.Position = new Vector2(position.X - 50, position.Y - 50);

            if (kState.IsKeyDown(Keys.Space))
                anim.setFrame(0);
            else if (isMoving)
                anim.Update(gameTime);
            else
                anim.setFrame(1);

            if (kState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
                Arrow.arrows.Add(new Arrow(position, direction));

            kStateOld = kState;

        }*/
        #endregion

    }
}
