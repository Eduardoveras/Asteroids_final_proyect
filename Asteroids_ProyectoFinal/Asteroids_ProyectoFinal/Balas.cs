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
    //Clase que se usa como modelo para las balas
    //no se aplica el movimiento, esto se aplica en
    //el update bullets que esta en la clase Nave
    public class Balas
    {
        public Texture2D textura;
        public Vector2 velocidad, origen,posicion;
        public bool esVisible;
        public float rotacion;
        public Rectangle boundingBox;
        public Color colorBala;

        public Balas(Texture2D newTexture,float rota,Color color)
        {
            textura = newTexture;
            esVisible = false;
            rotacion = rota;
            colorBala = color;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura,posicion,null,colorBala,rotacion,origen,0.5f,SpriteEffects.None,0);
        }


    }
}
