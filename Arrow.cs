using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Orlando
{
    class Arrow
    {
        public static List<Arrow> arrows = new List<Arrow>();
        private Vector2 position;
        private int speed = 400;
        private Dir direction;
        private bool collided = false;
        public Texture2D arrow;
        private float m_movementAngle = 0;


        public Arrow(Vector2 position, Dir direction, Texture2D sprite)
        {
            this.position = position;
            this.direction = direction;
            this.arrow = sprite;


        }

        public Arrow(Vector2 position, Dir direction)
        {
            this.position = position;
            this.direction = direction;
        }

        /*public Arrow(Texture2D arrow)
        {
            this.arrow = arrow;
        }*/

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }



        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            switch (direction)
            {
                case Dir.Right:
                    position.X += speed * dt;
                    m_movementAngle = 90;           // these might need tweaking (by 90s degrees)
                    break;
                case Dir.Left:
                    position.X -= speed * dt;
                    m_movementAngle = -90;           // these might need tweaking (by 90s degrees)
                    break;
                case Dir.Down:
                    position.Y += speed * dt;
                    m_movementAngle = 180;           // these might need tweaking (by 90s degrees)
                    break;
                case Dir.Up:
                    position.Y -= speed * dt;
                    m_movementAngle = 0;           // these might need tweaking (by 90s degrees)
                    break;
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Arrow arr in Arrow.arrows)
            {
                float angle = MathHelper.ToRadians(m_movementAngle);


                spriteBatch.Draw(arrow, new Vector2(arr.Position.X - 50, arr.Position.Y - 50), null, Color.White, MathHelper.PiOver2, new Vector2(arrow.Width / 2, arrow.Height / 2), angle, SpriteEffects.None, 0);
            }
        }


    }



}
