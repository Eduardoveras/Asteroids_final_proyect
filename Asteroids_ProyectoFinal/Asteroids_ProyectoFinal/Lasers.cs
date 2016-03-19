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
    public class Lasers
    {
        public Rectangle RecLaser;


        public bool LaserVisible = false;
        public Texture2D textLaser;

        public Lasers()
        {
        }

        public void LoadContent(ContentManager content)
        {
            textLaser = content.Load<Texture2D>("Laser");
        }

        public void update(GameTime gameTime, Nave jugador)
        {
            RecLaser = new Rectangle(-30, 500, textLaser.Width, textLaser.Height);


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (LaserVisible)
                spriteBatch.Draw(textLaser, RecLaser, Color.White);

        }
    }
}
