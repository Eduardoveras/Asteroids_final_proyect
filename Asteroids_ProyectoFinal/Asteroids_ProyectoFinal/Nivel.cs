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
    public class Nivel
    {


        /*
         * 
         *En esta clase se generan aleatoriamente asteroides
         *en toda la pantalla, estos de generan en las zonas fueras de la 
         *pantalla y se dirigen apuntando hacia la pantalla
         *Tambien se agregan explosiones y se eliminan los asteroides
         *cada vez que un asteroide colisiona con una nave
         *
         */


        Random rand = new Random();
        public Texture2D textura;
        public Texture2D texturaExplosion;
        public Texture2D texturaBala;
        public Texture2D textPerseg;
        public Texture2D textDispara;
        public Texture2D textFuerte;
        float spawnTimer = 0;
        float spawnTimerBalas;
        float spawnTimer2 = 0;
        float spawnTimer3 = 0;
        float spawnTimer4 = 0;
        public float spawnRate;
        public float spawnRate2;
        public float spawnRate3;
        public float spawnRate4;
        public float spawnRateBalas;
        public float spawnBalas;
        public List<Asteroid> ListaAster = new List<Asteroid>();
        public List<Balas> listaBalas = new List<Balas>();
        public int posX, posY;
        public bool Murio = false;
        List<Explosion> ExplosionList = new List<Explosion>();
        public int maxScore;
        //Para que el score para ganar sea siempre mas de lo que se tiene 
        public bool once=true;
        public bool level_passed = false;
        //Para que no pase de nivel solo con pasar un solo mundo
        public bool level_up = false;



        //Vainas del Laser
        public Lasers laser=new Lasers();
        float timer = 6;         //Initialize a 10 second timer
        const float TIMER = 6;
        




        public Nivel(float dif1, float dif4, float dif3,float dif2, int scoremaximo)
        {
            maxScore = scoremaximo;
            spawnRate = dif1;
            spawnRate2 = dif2;
            spawnRate3 = dif3;
            spawnRate4 = dif4;
            spawnRateBalas = 1;
        }



        public void loadContent(ContentManager content)
        {
            //Aqui se cargan las imagenes de los asteroides y el spritesheet de la explosion
            textura = content.Load<Texture2D>("asteroid");
            texturaExplosion = content.Load<Texture2D>("explosion");
            textPerseg = content.Load<Texture2D>("Perseguidor");
            textDispara = content.Load<Texture2D>("Disparador");
            texturaBala = content.Load<Texture2D>("bullet");
            textFuerte = content.Load<Texture2D>("SuperAsteroide");
            laser.LoadContent(content);
            
        }

        public void Update(GameTime gameTime, Nave jugador)
        {
            laser.update(gameTime,jugador);
            
            if(once)
            {
                maxScore +=jugador.score.Destruidos + jugador.score.Destruidos2 + jugador.score.Destruidos3 + jugador.score.Destruidos4 ;
                //no se volvera a hacer
                once = false;
            }
            if (jugador.score.Destruidos + jugador.score.Destruidos2 + jugador.score.Destruidos3 + jugador.score.Destruidos4 <= maxScore)
            {
                //Esto activa las explosiones cuando estan activas

                foreach (Explosion exe in ExplosionList)
                {
                    exe.Update(gameTime);
                }


                KeyboardState kb = Keyboard.GetState();
                if(kb.IsKeyDown(Keys.D)&&jugador.bombaActiva)
                {
                    foreach (Asteroid aster in ListaAster)
                    {
                        ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(aster.Position.X, aster.Position.Y), aster.Escala));
                        jugador.sn.explosion.Play();
                        aster.esVisible = false;
                    }
                    foreach (Balas balitas in listaBalas)
                    {
                        balitas.esVisible = false;
                    }
                    jugador.bombaActiva = false;
                }


                //Este timer se usa para generar los asteroides, cada X tiempo
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                spawnTimer += elapsed;
                spawnTimer2 += elapsed;
                spawnTimer3 += elapsed;
                spawnTimer4 += elapsed;



                float elapsed2 = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed2;
                if (timer <= 0)
                {

                    if (jugador.recNave.Intersects(laser.RecLaser) && laser.LaserVisible)

                        if (!laser.LaserVisible)
                            laser.LaserVisible = true;
                        //else
                          //  laser.LaserVisible = false;
                    //Timer expired, execute action
                    timer = TIMER;   //Reset Timer
                }



                if (spawnTimer >= spawnRate)
                {
                    do
                    {
                        posX = rand.Next(-100, 1300);
                    } while (posX > 0 && posX < 1200);

                    do
                    {
                        posY = rand.Next(-100, 820);
                    } while (posY > 0 && posY < 720);
                    Asteroid yolo = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40), new Vector2(posX, posY),
                                                 rand.Next(30, 200));
                    //yolo.tePersigue = true;

                    ListaAster.Add(yolo);

                    spawnTimer = 0;
                }

                if (spawnTimer2 >= spawnRate2)
                {
                    do
                    {
                        posX = rand.Next(-100, 1300);
                    } while (posX > 0 && posX < 1200);

                    do
                    {
                        posY = rand.Next(-100, 820);
                    } while (posY > 0 && posY < 720);
                    Asteroid yolo = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40), new Vector2(posX, posY),
                                                 rand.Next(30, 200));
                    yolo.teDispara = true;
                    yolo.Escala = 1;
                    ListaAster.Add(yolo);

                    spawnTimer2 = 0;
                }

                
                
                if (spawnTimer3 >= spawnRate3)
                {
                    do
                    {
                        posX = rand.Next(-100, 1300);
                    } while (posX > 0 && posX < 1200);

                    do
                    {
                        posY = rand.Next(-100, 820);
                    } while (posY > 0 && posY < 720);
                    Asteroid yolo = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40), new Vector2(posX, posY),
                                                 rand.Next(30, 200));
                    yolo.esFuerte = true;
                    yolo.vida = 21;
                    yolo.Escala = 1;
                    ListaAster.Add(yolo);

                    spawnTimer3 = 0;
                }
                

                
                
                if (spawnTimer4 >= spawnRate4)
                {
                    do
                    {
                        posX = rand.Next(-100, 1300);
                    } while (posX > 0 && posX < 1200);

                    do
                    {
                        posY = rand.Next(-100, 820);
                    } while (posY > 0 && posY < 720);
                    Asteroid yolo = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40), new Vector2(posX, posY),
                                                 rand.Next(30, 200));
                    yolo.tePersigue = true;
                    yolo.Escala = 1;
                    ListaAster.Add(yolo);

                    spawnTimer4 = 0;
                }





                foreach (Balas listaBala in listaBalas)
                {
                    if(listaBala.boundingBox.Intersects(jugador.EscudoRect[0])&&jugador.SuperEscudo1)
                    {
                        jugador.SuperEscudo1 = false;
                        listaBala.esVisible = false;
                        continue;
                    }

                    if (listaBala.boundingBox.Intersects(jugador.EscudoRect[1])&&jugador.SuperEscudo2)
                    {
                        listaBala.esVisible = false;
                        jugador.SuperEscudo2 = false;
                        continue;
                    }
                    if (listaBala.boundingBox.Intersects(jugador.EscudoRect[2])&&jugador.SuperEscudo3)
                    {
                        listaBala.esVisible = false;
                        jugador.SuperEscudo3 = false;
                        continue;
                    }

                    if(listaBala.boundingBox.Intersects(jugador.recNave))
                    {
                        Murio = true;
                    }
                }


                for (int i = 0; i < ListaAster.Count; i++)//Actualizar cada asteroide
                {

                    if (ListaAster[i].teDispara && ListaAster[i].Position.X > 0 && ListaAster[i].Position.Y > 0 && ListaAster[i].Position.X < 1200 && ListaAster[i].Position.Y < 720)
                    {
                        float rad;
                        rad =(float) Math.Atan(
                                      (jugador.posicion.Y-ListaAster[i].Position.Y )
                                      / (jugador.posicion.X-ListaAster[i].Position.X));
                        //rad *= ((2f*3.14159265359f)/360);
                        Shoot(rad, gameTime, ListaAster[i]);
                    }

                    UpdateBullets(gameTime);
                    if (jugador.SuperEscudo1)
                    {
                        if (jugador.EscudoRect[0].Intersects(ListaAster[i].boundingBox))
                        {
                            ListaAster[i].esVisible = false;
                            jugador.SuperEscudo1 = false;
                            ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                            jugador.sn.explosion.Play();
                        }
                    }
                    if (jugador.SuperEscudo2)
                    {
                        if (jugador.EscudoRect[1].Intersects(ListaAster[i].boundingBox))
                        {
                            ListaAster[i].esVisible = false;
                            jugador.SuperEscudo2 = false;
                            ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                            jugador.sn.explosion.Play();
                        }
                    }
                    if (jugador.SuperEscudo3)
                    {
                        if (jugador.EscudoRect[2].Intersects(ListaAster[i].boundingBox))
                        {
                            ListaAster[i].esVisible = false;
                            jugador.SuperEscudo3 = false;
                            ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                            jugador.sn.explosion.Play();
                        }
                    }

                    if (jugador.recNave.Intersects(
                        new Rectangle(ListaAster[i].boundingBox.X + 5, ListaAster[i].boundingBox.Y + 5, ListaAster[i].boundingBox.Width - 5, ListaAster[i].boundingBox.Height-5)))
                        Murio = true;

                    UpdateAsteroid(ListaAster[i], elapsed, jugador);
                    foreach (Balas balitas in jugador.listaBalas)
                    {
                        if (ListaAster[i].boundingBox.Intersects(balitas.boundingBox)&&ListaAster[i].esFuerte)
                        {
                            if (ListaAster[i].vida <= 0)
                            {
                                ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), 5));
                                ListaAster[i].esVisible = false;
                                jugador.score.Destruidos4 += 1;
                                jugador.score.ScoreJugador += 50;

                            }
                            else
                            {
                                ListaAster[i].vida--;
                                balitas.esVisible = false;
                                ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                                jugador.sn.explosion.Play();
                            }

                        }                        
                    }
                    
                    for (int j = 0; j < jugador.listaBalas.Count; j++)
                    {

                        if (ListaAster[i].boundingBox.Intersects(jugador.listaBalas[j].boundingBox) && ListaAster[i].Escala >= 1 && jugador.listaBalas[j].esVisible && ListaAster[i].esVisible && !ListaAster[i].esFuerte)
                        {
                            if (jugador.listaBalas[j].boundingBox.X > 0 && jugador.listaBalas[j].boundingBox.Y > 0 && jugador.listaBalas[j].boundingBox.Y < 720 && jugador.listaBalas[j].boundingBox.X < 1200)
                            {
                                jugador.score.ScoreJugador += 100;

                                if (!ListaAster[i].esFuerte && !ListaAster[i].teDispara && !ListaAster[i].tePersigue)
                                {
                                    jugador.score.Destruidos3 += 1;
                                    jugador.score.ScoreJugador += 10;
                                }
                                if (ListaAster[i].tePersigue)
                                {
                                    jugador.score.Destruidos2 += 1;
                                    jugador.score.ScoreJugador += 25;
                                }
                                if (ListaAster[i].teDispara)
                                {
                                    jugador.score.Destruidos += 1;
                                    jugador.score.ScoreJugador += 100;
                                }
                            }
                            ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                            Asteroid asterito = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40),
                                                             new Vector2(ListaAster[i].Position.X + 2,
                                                                         ListaAster[i].Position.Y + 2),
                                                             (float) rand.Next(30, 100));
                            if (ListaAster[i].tePersigue)
                                asterito.tePersigue = true;
                            ListaAster.Add(asterito);
                            Asteroid asterito2 = new Asteroid(rand.Next(-60, 60), rand.Next(-40, 40),
                                 new Vector2(ListaAster[i].Position.X + 2,
                                             ListaAster[i].Position.Y + 2),
                                 (float)rand.Next(30, 100));
                            if (ListaAster[i].tePersigue)
                                asterito2.tePersigue = true;
                            ListaAster.Add(asterito2);
                            ListaAster.RemoveAt(i);
                            jugador.listaBalas[j].esVisible = false;
                            jugador.sn.explosion.Play();


                        }
                        if (ListaAster[i].boundingBox.Intersects(jugador.listaBalas[j].boundingBox) && ListaAster[i].Escala < 1 && jugador.listaBalas[j].esVisible && ListaAster[i].esVisible)
                        {
                            if (jugador.listaBalas[j].boundingBox.X > 0 && jugador.listaBalas[j].boundingBox.Y > 0 && jugador.listaBalas[j].boundingBox.Y < 720 && jugador.listaBalas[j].boundingBox.X < 1200)
                            {
                                jugador.score.ScoreJugador += 10;

                                if (!ListaAster[i].esFuerte && !ListaAster[i].teDispara && !ListaAster[i].tePersigue)
                                {
                                    jugador.score.Destruidos3 += 1;
                                    jugador.score.ScoreJugador += 5;
                                }
                                if (ListaAster[i].tePersigue)
                                {
                                    jugador.score.Destruidos2 += 1;
                                    jugador.score.ScoreJugador += 20;
                                }
                                if (ListaAster[i].teDispara)
                                {
                                    jugador.score.Destruidos += 1;
                                    jugador.score.ScoreJugador += 50;
                                }


                            }
                            ExplosionList.Add(new Explosion(texturaExplosion, new Vector2(ListaAster[i].Position.X, ListaAster[i].Position.Y), ListaAster[i].Escala));
                            ListaAster[i].esVisible = false;
                            jugador.listaBalas[j].esVisible = false;
                            jugador.sn.explosion.Play();

                        }
                    }
                    //al los asteroides colisionar, se hacen invisibles y al hacerse invisibles se eliminan 
                    //de la lista, esto ayuda a la eficiencia del juego
                    if (!ListaAster[i].esVisible)
                    {
                        ListaAster.RemoveAt(i);

                    }
                }

            }
            else
            {
                if (!level_up)
                {
                    level_up = true;
                    jugador.score.Licencia++;
                }
                level_passed = true;
                once = true;
            }



        }


        public void UpdateBullets(GameTime gameTime)
        {
                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimerBalas += elapsed;


            if (spawnTimerBalas >= spawnRateBalas)
            {
                foreach (Balas b in listaBalas)
                {
                    b.boundingBox = new Rectangle((int)b.posicion.X, (int)b.posicion.Y, texturaBala.Width, texturaBala.Height);
                    b.posicion += b.velocidad;
                }
                spawnTimerBalas = 0;
            }

            for (int i = 0; i < listaBalas.Count; i++)
            {
                if (!listaBalas[i].esVisible)
                {
                    listaBalas.RemoveAt(i);
                    i--;
                }
            }
        }




        public void Shoot(float rota, GameTime gameTime, Asteroid aster)
        {


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimerBalas += elapsed;


            if (spawnTimerBalas >= spawnRateBalas)
            {


                Balas nuevaBala = new Balas(texturaBala, rota, Color.White);
                nuevaBala.velocidad=new Vector2((float)Math.Cos(rota),(float)Math.Sin(rota));
                
                nuevaBala.posicion = aster.Position;

                nuevaBala.esVisible = true;
                listaBalas.Add(nuevaBala);
                spawnTimerBalas = 0;


            }
        }






        public void UpdateAsteroid(Asteroid a, float elapsed, Nave jugador)
        {

            //Aqui se le asigna el movimiento a los asteroides
            
            a.boundingBox = new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)(textura.Width * a.Escala), (int)(textura.Height * a.Escala));
            if (a.tePersigue)
            {
                if (a.Position.X > jugador.posicion.X)
                {
                    a.Position.X -= ((a.Position.X - jugador.posicion.X) * 0.001f);
                }
                if (a.Position.Y > jugador.posicion.Y)
                {
                    a.Position.Y -= (a.Position.Y - jugador.posicion.Y) * 0.001f;
                }
                if (a.Position.X < jugador.posicion.X)
                {
                    a.Position.X += (a.Position.X + jugador.posicion.X) * 0.001f;
                }
                if (a.Position.Y < jugador.posicion.Y)
                {
                    a.Position.Y += ((a.Position.Y + jugador.posicion.Y) * 0.001f);
                }
            }
            else
            {
                a.Position.Y += a.Velocityy * elapsed;
                a.Position.X += a.Velocityx * elapsed;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            laser.Draw(spriteBatch);
            foreach (Balas b in listaBalas)
            {
                b.Draw(spriteBatch);
            }
            //Aqui se dibuja la lista de explosiones
            foreach (Explosion ex in ExplosionList)
            {
                ex.Draw(spriteBatch);
            }
            //Aqui se dibuja la lista de asteroides
            foreach (Asteroid asteroid in ListaAster)
            {
                if(asteroid.esVisible)
                {
                    if (asteroid.teDispara)
                    {
                        spriteBatch.Draw(textDispara, asteroid.Position, null,Color.White, 0f, new Vector2(textura.Width / 2, textura.Height / 2), asteroid.Escala, SpriteEffects.None, 0f);
                    }
                    else if(asteroid.tePersigue)
                    {
                        spriteBatch.Draw(textPerseg, asteroid.Position, null,Color.White, 0f, new Vector2(textura.Width / 2, textura.Height / 2), asteroid.Escala, SpriteEffects.None, 0f);
                    }
                    else if(asteroid.esFuerte)
                    {
                        spriteBatch.Draw(textFuerte, asteroid.Position, null, Color.White, 0f, new Vector2(textura.Width / 2, textura.Height / 2), asteroid.Escala, SpriteEffects.None, 0f);
                    }
                    else 
                        spriteBatch.Draw(textura, asteroid.Position, null, Color.White, 0f, new Vector2(textura.Width / 2, textura.Height / 2), asteroid.Escala, SpriteEffects.None, 0f);

                }

            }


        }


    }
}
