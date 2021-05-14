using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Orlando
{


    public class HealthBar
    {
        private Texture2D tex;
        private Texture2D hearts;
        private int heartCount = 4;


        public HealthBar(ContentManager content, Color foregroundColour)
        {
            hearts = content.Load<Texture2D>("Player/healthbar_32x96");
        }

        public void DrawHealthBar(SpriteBatch spriteBatch, Vector2 position, float health, float maxHealth = 100)
        {
            // draw the back of the bar
            float width = ((1.0f / maxHealth) * health);

            // choose a bit of this image, depending on the health value
            Rectangle source = new Rectangle
            {
                X = 0,
                Y = 0,
                Width = (int)(width * hearts.Width),
                Height = hearts.Height
            };

            Rectangle full = new Rectangle
            {
                X = 0,
                Y = 0,
                Width = hearts.Width,
                Height = hearts.Height
            };

            int x = (int)position.X - hearts.Width / 2;

            // now draw the front, which shows the health

            // draw background
            Rectangle fullDestinationRectangle = new Rectangle(x, (int)position.Y, hearts.Width, hearts.Height);
            spriteBatch.Draw(hearts, fullDestinationRectangle, full, Color.Gray);

            // draw foreground
            Rectangle destinationRectangle = new Rectangle(x, (int)position.Y, (int)(hearts.Width * width), hearts.Height);
            spriteBatch.Draw(hearts, destinationRectangle, source, Color.White);

        }
    }
}