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

    //Esta clase se utiliza como modelo para los asteroides
    //en esta clase no se usan metodos, los metodos de los
    //asteroides estan en la clase "asteroides".
    public class Asteroid
    {
        
        public float Velocityx, Velocityy;
        public Vector2 Position;
        public Rectangle boundingBox;
        public bool esVisible;
        public float Escala;
        public bool tePersigue,teDispara,esFuerte;
        public int vida = 1;
        public Asteroid(float velocityX, float velocityY, Vector2 position,float escala)
        {
            tePersigue = false;
            teDispara = false;
            esFuerte = false;
            esVisible = true;
            Velocityx = velocityX;
            Velocityy = velocityY;
            Position = position;
            Escala = escala/100;
        }


    }
}
