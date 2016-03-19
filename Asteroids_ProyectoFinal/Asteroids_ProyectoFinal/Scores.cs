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
using System.Xml.Serialization;

namespace Asteroids_ProyectoFinal
{
    [Serializable]
    public class Scores
    {

        public int ScoreJugador;
        public int Destruidos;
        public int Destruidos2;
        public int Destruidos3;
        public int Destruidos4;

        public string valor;
        public short Licencia = 1;

        public Scores()
        {
            ScoreJugador = 20000;
            Destruidos = 0;
            Destruidos2 = 0;
            Destruidos3 = 0;
            Destruidos4 = 0;

        }

        public void LoadContent(ContentManager content)
        {


        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Marco, Texture2D Coin, Texture2D Asteroide1, Texture2D Asteroide2,Texture2D Asteroide3,Texture2D Asteroide4, SpriteFont FontScore)
        {
            spriteBatch.Draw(Marco,new Vector2(900,20),Color.White*0.5f);
            spriteBatch.Draw(Coin,new Vector2(967,43),Color.White );
            spriteBatch.Draw(Asteroide1, new Vector2(967, 75), Color.White);
            spriteBatch.Draw(Asteroide2, new Vector2(967, 97), Color.White);
            spriteBatch.Draw(Asteroide3, new Vector2(967, 119), Color.White);
            spriteBatch.Draw(Asteroide4, new Vector2(967, 141), Color.White);

            spriteBatch.DrawString(FontScore, ScoreJugador.ToString(), new Vector2(990,40), Color.White);
            spriteBatch.DrawString(FontScore, Destruidos3.ToString(), new Vector2(990, 70), Color.White);
            spriteBatch.DrawString(FontScore, Destruidos.ToString(), new Vector2(990, 95), Color.White);
            spriteBatch.DrawString(FontScore, Destruidos2.ToString(), new Vector2(990, 118), Color.White);
            spriteBatch.DrawString(FontScore, Destruidos4.ToString(), new Vector2(990, 140), Color.White);
        }
    }
}
