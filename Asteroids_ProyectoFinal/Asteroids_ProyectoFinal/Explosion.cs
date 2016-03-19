using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Asteroids_ProyectoFinal
{
    public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer;
        public float interval;
        public Vector2 origen;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;
        public float escala;

        //Constructor
        public Explosion(Texture2D textura, Vector2 newPosition,float esc)
        {
            texture = textura;
            position = newPosition;
            timer = 0f;
            interval = 11f;
            currentFrame = 1;
            spriteWidth = 128;
            spriteHeight = 128;
            isVisible = true;
            escala = esc;
        }

        //Update
        public void Update(GameTime gameTime)
        {
            //aumentar el timer en milisegundos
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //Funciono!!
            if (timer > interval)
            {
                //mostrar el proximo frame
                currentFrame++;
                timer = 0f;
            }

            //Si estamos en el ultimo frame, resetear al primero
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origen = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

        }

        //Dibujar
        public void Draw(SpriteBatch spriteBatch)
        {
            //si es visible, se dibuja
            if (isVisible)
            {

                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origen,( escala/2)+0.3f, SpriteEffects.None, 0);
            }
        }
    }



}
