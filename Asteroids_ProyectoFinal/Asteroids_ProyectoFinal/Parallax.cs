using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids_ProyectoFinal
{
    public class Parallax
    {
        public Texture2D texture, texture2;
        public Vector2 bgPos1, bgPos2, bgPos1_2, bgPos2_2;
        public int speed, speed2, sum = 8;

        public Parallax()
        {
            //primer fondo
            texture = null;
            bgPos1 = new Vector2(50, 0);
            bgPos2 = new Vector2(50, -700);
            speed = 2;

            //Segundo fondo
            texture2 = null;
            bgPos1_2 = new Vector2(70, 0);
            bgPos2_2 = new Vector2(70, -700);
            speed2 = 1;
        }

        public void loadContent(ContentManager Content)
        {
            texture2 = Content.Load<Texture2D>("parallax");
            texture = Content.Load<Texture2D>("parallax2");
        }
        public void update(GameTime gameTime)
        {
            //Fondo en movimiento
            bgPos1.Y += speed;
            bgPos2.Y += speed;
            bgPos1_2.Y += speed2;
            bgPos2_2.Y += speed2;

            if (bgPos1.Y >= 700)
            {
                bgPos1.Y = 0;
                bgPos2.Y = -700;
                bgPos1_2.Y = 0;
                bgPos2_2.Y = -700;
            }
            if (gameTime.TotalGameTime.Seconds > sum)
            {
                speed += 1;
                speed2 += 1;
                if (sum < 60)
                    sum += 10;
                else
                    sum -= 10;

            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPos1, Color.White);
            spriteBatch.Draw(texture, bgPos2, Color.White);
            spriteBatch.Draw(texture2, bgPos1_2, Color.White);
            spriteBatch.Draw(texture2, bgPos2_2, Color.White);
        }

    }
}
