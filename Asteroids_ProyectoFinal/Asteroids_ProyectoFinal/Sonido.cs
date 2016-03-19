using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Asteroids_ProyectoFinal
{
    public class Sonido
    {
        public SoundEffect playerShoot;
        public SoundEffect explosion;
        public SoundEffect UpSound;
        public SoundEffect OverSound;
        public Song bgMusic;
        //constructor
        public Sonido()
        {
            playerShoot = null;
            explosion = null;
            bgMusic = null;
            UpSound = null;
            OverSound = null;
        }

        //Cargar contenido
        public void LoadContent(ContentManager content)
        {
            playerShoot = content.Load<SoundEffect>("playershoot");
            explosion = content.Load<SoundEffect>("explode");
            bgMusic = content.Load<Song>("theme");
            UpSound = content.Load<SoundEffect>("coin");
            OverSound = content.Load<SoundEffect>("overSound");
        }
    }

}
