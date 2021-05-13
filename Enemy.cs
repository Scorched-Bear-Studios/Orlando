using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Orlando
{
    class Enemy //  draft class for enemies patrolling
    {
        Texture2D texture;
       
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;

        float rotation = 0f;

        float distance;
        float oldDistance;
        bool right;


        public Enemy ( Texture2D newTexture, Vector2 newPosition,  float newDistance)
        {
            texture = newTexture;
            position = newPosition;
            distance = newDistance;
            


            oldDistance = distance;
        }

        float playerDistance;
        public void Update(Player player, GameTime gameTime)
        {
            position += velocity;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            if (distance <= 0)
            {
                right = true;
                velocity.X = 1f;
            }
            else if(distance <= oldDistance)
            {
                right = false;
                velocity.X = -1f;
            }
            if (right) distance += 1;  else distance -= 1;


            playerDistance = player.Position.X - position.X;

            if (playerDistance >= -200 && playerDistance <= 200)
            {
                if (playerDistance < -1)
                    velocity.X = -1f;
                else if (playerDistance > 1)
                    velocity.X = 1f;
                else if (playerDistance == 0)
                    velocity.X = 0f;

            }


        }

        public void Draw( SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);



        }



    }
}
