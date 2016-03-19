using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids_ProyectoFinal
{
    public class Nave
    {
        public Texture2D texturaNave, texturaBala;
        public Rectangle recNave;

        //centro de la imagen
        public Vector2 origen, posicion, velocidad;
        public float rotacion;
        public float friccion = 0.009f;
        public float VelTan = 2.9f;
        public List<Balas> listaBalas = new List<Balas>();
        public Scores score = new Scores();

        public bool SuperBalas1=false;
        public bool SuperBalas2 = false;
        public bool SuperBalas3 = false;

        public bool SuperEscudo1 = false;
        public bool SuperEscudo2 = false;
        public bool SuperEscudo3 = false;


        public bool DirBalas1 = false;
        public bool DirBalas2 = false;
        public bool DirBalas3 = false;

        public bool bombaActiva = false;

        public bool[][] habilidades =new bool[3][];

        //las vainas del score
        public Texture2D Marco;
        public Texture2D Coin;
        public Texture2D Asteroide1;
        public Texture2D Asteroide2;
        public Texture2D Asteroide3;
        public Texture2D Asteroide4;
        public SpriteFont FontScore;

        public Texture2D[] Escudo= new Texture2D[3];
        public Rectangle[] EscudoRect=new Rectangle[3]; 

        float spawnTimer;
        public float spawnRate = 0.4f;
        public Vector2 direction;
        public Sonido sn = new Sonido();

        public Nave()
        {
            posicion = new Vector2(300, 250);
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < 3; i++)
            {
                habilidades[i] = new bool[3];
            }
            habilidades[0]= new bool[3];

            FontScore = content.Load<SpriteFont>("font1");
            Marco = content.Load<Texture2D>("FrameScore");
            Coin = content.Load<Texture2D>("coinImagen");
            Asteroide1 = content.Load<Texture2D>("MiniAsteroid");
            Asteroide2 = content.Load<Texture2D>("DisparadorMINI");
            Asteroide3 = content.Load<Texture2D>("PerseguidorMINI");
            Asteroide4 = content.Load<Texture2D>("SuperAsteroidemini");







            //cargar la imagen de la nave
            texturaNave = content.Load<Texture2D>("nave");
            texturaBala = content.Load<Texture2D>("bullet");
            //Cargar sonido
            sn.LoadContent(content);
            score.LoadContent(content);
            for (int i = 0; i <3; i++)
            {
                Escudo[i] = content.Load<Texture2D>("escudo");
            }

        }

        public void Update(GameTime gameTime)
        {
            habilidades[0][0] = SuperBalas1;
            habilidades[1][0] = SuperBalas2;
            habilidades[2][0] = SuperBalas3;

            habilidades[0][1] = SuperEscudo1;
            habilidades[1][1] = SuperEscudo2;
            habilidades[2][1] = SuperEscudo3;

            habilidades[0][2] = DirBalas1;
            habilidades[1][2] = DirBalas2;
            habilidades[2][2] = DirBalas3;

            //MouseState curMouse = Mouse.GetState();
            //Vector2 MouseLoc = new Vector2(curMouse.X, curMouse.Y);
            //direction= new Vector2(posicion.X-MouseLoc.X,posicion.Y-MouseLoc.Y);

            if (SuperBalas1)
                spawnRate = 0.2f;
            if (SuperBalas2)
                spawnRate = 0.09f;
            if (SuperBalas3)
                spawnRate = 0.04f;

            score.Update(gameTime);
            recNave = new Rectangle((int)(posicion.X)+5, (int)(posicion.Y)+5, texturaNave.Width-20, texturaNave.Height-10);

            EscudoRect[0] = new Rectangle((int)(posicion.X-50) , (int)(posicion.Y-50) , texturaNave.Width+10 , texturaNave.Height+10 );
            EscudoRect[1] = new Rectangle((int)(posicion.X-70) , (int)(posicion.Y-70) , texturaNave.Width+50 , texturaNave.Height+50 );
            EscudoRect[2] = new Rectangle((int)(posicion.X-90) , (int)(posicion.Y-90) , texturaNave.Width+90 , texturaNave.Height+90);


            origen = new Vector2(texturaNave.Width / 2, texturaNave.Height / 2);
            posicion += velocidad;
            if (posicion.X > 1200)
                posicion.X = 1200;
            if (posicion.X < 0)
                posicion.X = 0;
            if (posicion.Y < 0)
                posicion.Y = 0;
            if (posicion.Y > 710)
                posicion.Y = 710;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Mouse.GetState().LeftButton == ButtonState.Pressed)
                Shoot(rotacion, gameTime);

                
            //rotacion = (float)((Math.Atan2(direction.Y, direction.X))+(Math.PI)); 
            UpdateBullets();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotacion += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotacion -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocidad.X = (float)Math.Cos(rotacion) * VelTan;
                velocidad.Y = (float)Math.Sin(rotacion) * VelTan;
            }

            else if (velocidad != Vector2.Zero)
            {
                Vector2 i = velocidad;
                velocidad -= friccion * i;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaNave, posicion, null, Color.White, rotacion, origen, 1f, SpriteEffects.None, 1);
            foreach (Balas b in listaBalas)
            {
                b.Draw(spriteBatch);
            }

            DrawScore(spriteBatch);

            if (SuperEscudo1)
            {
                spriteBatch.Draw(Escudo[0], EscudoRect[0], Color.White);
            }
            if (SuperEscudo2)
            {
                spriteBatch.Draw(Escudo[1], EscudoRect[1], Color.White);
            }
            if (SuperEscudo3)
            {
                spriteBatch.Draw(Escudo[2], EscudoRect[2], Color.White);
            }

        }


        public void DrawScore(SpriteBatch spriteBatch)
        {
            score.Draw(spriteBatch,Marco,Coin,Asteroide1,Asteroide2,Asteroide3,Asteroide4,FontScore);
        }

        public void UpdateBullets()
        {
            foreach (Balas b in listaBalas)
            {
                b.boundingBox = new Rectangle((int)b.posicion.X, (int)b.posicion.Y, texturaBala.Width, texturaBala.Height);
                b.posicion += b.velocidad;
                if (Vector2.Distance(b.posicion, posicion) > 500)
                {
                    b.esVisible = false;
                }
            }
            for (int i = 0; i < listaBalas.Count; i++)
            {
                if (!listaBalas[i].esVisible || listaBalas[i].posicion.X > 10000 || listaBalas[i].posicion.X < -10000 || posicion.Y>5000||posicion.Y<-5000)
                {
                    listaBalas.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot(float rota, GameTime gameTime)
        {


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += elapsed;


            if (spawnTimer >= spawnRate)
            {
                if(DirBalas2||DirBalas3||(!DirBalas1&&!DirBalas2&&!DirBalas3))
                {
                    Balas nuevaBala = new Balas(texturaBala, rota,Color.White);

                    nuevaBala.velocidad = new Vector2((float)Math.Cos(rota), (float)Math.Sin(rota)) * 5f + velocidad;
                    nuevaBala.posicion = posicion + nuevaBala.velocidad * 5;
                    nuevaBala.esVisible = true;
                    spawnTimer = 0;

                    listaBalas.Add(nuevaBala);


                }


                if (DirBalas1||DirBalas3)
                {

                    Balas nuevaBala2 = new Balas(texturaBala, rota + 0.35f,Color.LightGreen);

                    nuevaBala2.velocidad = new Vector2((float) Math.Cos(rota + 0.35f), (float) Math.Sin(rota + 0.35f))*
                                           5f + velocidad;
                    nuevaBala2.posicion = posicion + nuevaBala2.velocidad*5;
                    nuevaBala2.esVisible = true;
                    spawnTimer = 0;

                    listaBalas.Add(nuevaBala2);


                    Balas nuevaBala3 = new Balas(texturaBala, rota - 0.35f, Color.LightGreen);

                    nuevaBala3.velocidad = new Vector2((float) Math.Cos(rota - 0.35f), (float) Math.Sin(rota - 0.35f))*
                                           5f + velocidad;
                    nuevaBala3.posicion = posicion + nuevaBala3.velocidad*5;
                    nuevaBala3.esVisible = true;
                    spawnTimer = 0;

                    listaBalas.Add(nuevaBala3);

                }


                if (DirBalas3)
                {

                    Balas nuevaBala4 = new Balas(texturaBala, rota + 0.90f,Color.OrangeRed);

                    nuevaBala4.velocidad = new Vector2((float)Math.Cos(rota + 0.90f), (float)Math.Sin(rota + 0.90f)) *5f + velocidad;
                    nuevaBala4.posicion = posicion + nuevaBala4.velocidad * 5;
                    nuevaBala4.esVisible = true;
                    spawnTimer = 0;

                    listaBalas.Add(nuevaBala4);


                    Balas nuevaBala5 = new Balas(texturaBala, rota - 0.90f, Color.OrangeRed);

                    nuevaBala5.velocidad = new Vector2((float)Math.Cos(rota - 0.90f), (float)Math.Sin(rota - 0.90f)) * 5f + velocidad;
                    nuevaBala5.posicion = posicion + nuevaBala5.velocidad * 5;
                    nuevaBala5.esVisible = true;
                    spawnTimer = 0;

                    listaBalas.Add(nuevaBala5);

                }

                sn.playerShoot.Play();

            }

        }
    }
}
