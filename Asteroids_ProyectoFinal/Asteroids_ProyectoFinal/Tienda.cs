using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids_ProyectoFinal
{
    public class Tienda
    {

        public Texture2D[] balas = new Texture2D[3];
        public Texture2D[] escudos = new Texture2D[3];
        public Texture2D[] direcciones = new Texture2D[3];
        public bool[] balasComp= new bool[3];
        public bool[] escudosComp= new bool[3];
        public bool[] direccionesComp= new bool[3];
        public int x = 0, y = 0;

        public Texture2D seleccionado;
        public Rectangle[,] arreglo = new Rectangle[3, 3];
        public Vector2[,] arregloPos = new Vector2[3,3];

        public Texture2D[] bomba= new Texture2D[2];
        public Rectangle bombaRect;
        public Vector2 bombaPos = new Vector2(365, 560);
        public int[,] precios= new int[3,3];

        public int fps = 0, maxfps = 100;

        public bool mensaje=false;
        public int duracionMensaje=0;

        public Rectangle AtrasRect;
        public Texture2D[] AtrasText = new Texture2D[2];

        public Texture2D fondo, NoBuyText;
        public SpriteFont fuente;

        public bool salir = false;
        public int score=0;

        public Tienda()
        {

        }

        public void LoadContent(ContentManager cm)
        {
            //Asignmacion no automatica de precios
            precios[0, 0] =600;
            precios[1, 0] =1200;
            precios[2, 0] =2400;

            precios[0, 1] =100;
            precios[1, 1] =400;
            precios[2, 1] =600;

            precios[0, 2] =1000;
            precios[1, 2] =2000;
            precios[2, 2] =3100;

            fondo = cm.Load<Texture2D>(@"Tienda\wallpaperTienda");
            seleccionado = cm.Load<Texture2D>(@"Tienda\Brillohhh");
            for (int i = 0; i < 2; i++)
            {
                bomba[i] = cm.Load<Texture2D>(@"Tienda\bomb" + Convert.ToString(i+1));
                AtrasText[i] = cm.Load<Texture2D>(@"botonAtras" + Convert.ToString(i + 1));
            }



            NoBuyText = cm.Load<Texture2D>("NoBuy");
            bombaRect = new Rectangle(0,0,512,512);
            fuente = cm.Load<SpriteFont>(@"font1");

            for (int i = 0; i < 3; i++)
            {
                balas[i] = cm.Load<Texture2D>(@"Tienda\BalasRapidas"+Convert.ToString(i+1));
                direcciones[i] = cm.Load<Texture2D>(@"Tienda\CajaBalas"+Convert.ToString(i+1));
                escudos[i]= cm.Load<Texture2D>(@"Tienda\CajaEscudo"+Convert.ToString(i+1));
                balasComp[i]=false;
                escudosComp[i]=false;
                direccionesComp[i]=false;
            }
            for (int i = 0; i < 9; i++)
            {
                arregloPos[i / 3, i % 3] = new Vector2(160 + (i/3 * 180), 125 + (i%3 * 140));
            }

        }

        public void Update(GameTime gt,Nave jugador)
        {
            score=jugador.score.ScoreJugador;
            KeyboardState kb = Keyboard.GetState();

            fps += gt.ElapsedGameTime.Milliseconds;
            
            if(mensaje)
                duracionMensaje += gt.ElapsedGameTime.Milliseconds;
            if(duracionMensaje>=900)
            {
                duracionMensaje = 0;
                mensaje = false;
            }
            if (fps > maxfps)
            {
                fps = 0;
                if (kb.IsKeyDown(Keys.Up) && y > -1)
                {
                    y--;
                }
                if (kb.IsKeyDown(Keys.Down) && y < 3)
                {
                    y++;
                }
                if (kb.IsKeyDown(Keys.Right) && x < 2)
                {
                    x++;
                }
                if (kb.IsKeyDown(Keys.Left) && x > 0)
                {
                    x--;
                }

                if(x>=0&&x<3&&y>=0&&y<3)
                {
                    if (kb.IsKeyDown(Keys.Enter) && jugador.score.ScoreJugador >= precios[x, y])
                    {
                        if (jugador.score.ScoreJugador < 0)
                        {
                            jugador.score.ScoreJugador = 0;
                        }

                        if (x == 0 && y == 0 && !jugador.SuperBalas1)
                        {
                            jugador.SuperBalas1 = true;
                            jugador.score.ScoreJugador -= precios[x, y];

                        }

                        else if (x == 1 && y == 0 && !jugador.SuperBalas2)
                        {
                            jugador.SuperBalas2 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 2 && y == 0 && !jugador.SuperBalas3)
                        {
                            jugador.SuperBalas3 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }

                        else if (x == 0 && y == 1 && !jugador.SuperEscudo1)
                        {
                            jugador.SuperEscudo1 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 1 && y == 1 && jugador.SuperEscudo2)
                        {
                            jugador.SuperEscudo2 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 2 && y == 1 && !jugador.SuperEscudo3)
                        {
                            jugador.SuperEscudo3 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 0 && y == 2 && !jugador.DirBalas1)
                        {
                            jugador.DirBalas1 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 1 && y == 2 && !jugador.DirBalas2)
                        {
                            jugador.DirBalas2 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }
                        else if (x == 2 && y == 2 && !jugador.DirBalas3)
                        {
                            jugador.DirBalas3 = true;
                            jugador.score.ScoreJugador -= precios[x, y];
                        }

                        else
                        {
                            mensaje = true;
                        }
                    }
                }
                else if (y==3)
                {
                    if (kb.IsKeyDown(Keys.Enter))
                    {
                        if(!jugador.bombaActiva && jugador.score.ScoreJugador >= 2500)
                        {
                            jugador.bombaActiva = true;
                            jugador.score.ScoreJugador -= 2500;
                        }
                        else mensaje = true;
                    }
                }
                else
                {
                    if(kb.IsKeyDown(Keys.Enter))
                    salir = true;
                }
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(fondo,new Vector2(0),new Rectangle(0,0,950,600),Color.White,0f
                ,Vector2.Zero,1.27f,SpriteEffects.None,0.6f);

            sb.Draw(bomba[0], bombaPos, bombaRect, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
            sb.Draw(AtrasText[0], new Vector2(100,30), Color.White);
            
            if(x>=0&&x<3&&y>=0&&y<3)
            {
                sb.Draw(seleccionado,new Vector2(arregloPos[x,y].X-20,arregloPos[x,y].Y-20),Color.White);
            }
            else 
                if(y>=3)
                    sb.Draw(bomba[1], bombaPos, bombaRect, Color.White, 0f, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
                else 
                    if (y == -1)
                            sb.Draw(AtrasText[1], new Vector2(100,30), Color.White);
                        
                            

            

            for (int i = 0; i < 3; i++)
            {
                sb.Draw(balas[i], arregloPos[i,0], Color.White);
                sb.Draw(escudos[i], arregloPos[i,1], Color.White);
                sb.Draw(direcciones[i], arregloPos[i,2 ], Color.White);
            }

            

            //Precios balas
            sb.DrawString(fuente,Convert.ToString(precios[0,0]), new Vector2(arregloPos[0,0].X+20,arregloPos[0,0].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
            sb.DrawString(fuente, Convert.ToString(precios[1, 0]), new Vector2(arregloPos[1, 0].X + 20, arregloPos[1, 0].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
            sb.DrawString(fuente, Convert.ToString(precios[2, 0]), new Vector2(arregloPos[2, 0].X + 20, arregloPos[2, 0].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            //Precio de  escudos
            sb.DrawString(fuente, Convert.ToString(precios[0, 1]), new Vector2(arregloPos[0, 1].X + 20, arregloPos[0, 1].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            sb.DrawString(fuente, Convert.ToString(precios[1, 1]), new Vector2(arregloPos[1, 1].X + 20, arregloPos[1, 1].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            sb.DrawString(fuente, Convert.ToString(precios[2, 1]), new Vector2(arregloPos[2, 1].X + 20, arregloPos[2, 1].Y),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            //Precios de las direcciones
            sb.DrawString(fuente, Convert.ToString(precios[0, 2]), new Vector2(arregloPos[0, 2].X + 20, arregloPos[0, 2].Y),
                Color.LightGray, 0f, Vector2.Zero,1.5f, SpriteEffects.None, 1f);

            sb.DrawString(fuente, Convert.ToString(precios[1, 2]), new Vector2(arregloPos[1, 2].X + 20, arregloPos[1, 2].Y),
                Color.LightGray, 0f, Vector2.Zero,1.5f, SpriteEffects.None, 1f);

            sb.DrawString(fuente, Convert.ToString(precios[2, 2]), new Vector2(arregloPos[2, 2].X + 20, arregloPos[2, 2].Y),
                Color.LightGray, 0f, Vector2.Zero,1.5f, SpriteEffects.None, 1f);

            sb.DrawString(fuente, "2500", new Vector2(arregloPos[0, 2].X+220, arregloPos[2, 2].Y+210),
                Color.LightGray, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            //Mensaje de advertencia
            if(mensaje)
            sb.Draw(NoBuyText,new Vector2(200,200),Color.White );

            sb.DrawString(fuente,"Money: "+Convert.ToString(score),new Vector2(755,560),
                Color.LightGray, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        }

    }
}
