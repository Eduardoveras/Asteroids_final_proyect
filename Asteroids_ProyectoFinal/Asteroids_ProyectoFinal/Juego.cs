using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;
using System.IO;

namespace Asteroids_ProyectoFinal
{
    public class Juego
    {
        public Texture2D texturaFondo;

        public Nave jugador = new Nave();

        State gameState = State.Menu;
        public Parallax fondo = new Parallax();

        //Niveles
        public Nivel[] Niveles = new Nivel[4];
        public Texture2D[] textNiveles = new Texture2D[4];
        public Texture2D logo;
        //Botones
        public Texture2D[] botones = new Texture2D[10];
        public Vector2[] posiciones = new Vector2[3];

        public bool[][] arrepcion = new bool[3][];


        public Tienda chopin = new Tienda();
        public Rectangle TiendaRect;
        public Texture2D[] TiendaText = new Texture2D[2];

        public Rectangle AtrasRect;
        public Texture2D[] AtrasText = new Texture2D[2];
        public Vector2 TiendaPos;

        public Texture2D naves_selec;

        public Texture2D texturaOpciones, textSelectShip;

        public short seleccion = 1;
        public int fps = 0;
        public int maxfps = 100;
        
        public Texture2D textGameOver, textInstruc, textPasado;

        public bool finDeJuego = false;
        //Este es el enum para los estados del juego
        public enum State
        {
            Menu,
            Playing,
            Gameover,
            Highscores,
            Niveles,
            Tienda,
            Instrucciones,
            PasandoNivel,
            SelectShip
        }

        public Juego()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            logo = Content.Load<Texture2D>("logo");
            naves_selec = Content.Load<Texture2D>(@"seleccion");
            chopin.LoadContent(Content);
            Niveles[0] = new Nivel(0.2f, 1000, 1000, 1000, 30);
            Niveles[1] = new Nivel(0.3f, 0.6f, 1000, 1000, 50);
            Niveles[2] = new Nivel(0.4f, 0.7f, 0.6f, 1000, 90);
            Niveles[3] = new Nivel(0.5f, 0.8f, 0.5f, 0.4f, 200);

            posiciones[0] = new Vector2(120, 100);
            posiciones[1] = new Vector2(120, 425);
            posiciones[2] = new Vector2(800, 425);

            TiendaPos = new Vector2(900, 30);

            texturaFondo = Content.Load<Texture2D>("wallpaper");
            texturaOpciones = Content.Load<Texture2D>("wallpaper2");
            textGameOver = Content.Load<Texture2D>("gameover");
            textInstruc = Content.Load<Texture2D>("Instrucciones");
            textPasado = Content.Load<Texture2D>("pasado");
            textSelectShip = Content.Load<Texture2D>("FondoNaves");
            for (int i = 0; i < 4; i++)
            {
                textNiveles[i] = Content.Load<Texture2D>("Fondos Niveles\\FondoMenu" + (1 + i).ToString());
            }
            for (int i = 0; i < 10; i++)
            {
                botones[i] = Content.Load<Texture2D>("Buttons\\boton" + (1 + i).ToString());
            }
            for (int i = 0; i < 2; i++)
            {
                TiendaText[i] = Content.Load<Texture2D>(@"Tienda" + (1 + i).ToString());
                AtrasText[i] = Content.Load<Texture2D>(@"botonAtras" + (1 + i).ToString());

            }

            fondo.loadContent(Content);

            jugador.LoadContent(Content);
            foreach (Nivel nivele in Niveles)
            {
                nivele.loadContent(Content);
            }
        }

        public void Update(GameTime gameTime, ContentManager content)
        {

            MouseState mouse = Mouse.GetState();
            Point mousePos = new Point(mouse.X, mouse.Y);
            fondo.update(gameTime);
            KeyboardState kb = Keyboard.GetState();

            switch (gameState)
            {
                case State.Menu:
                    {
                        fps += gameTime.ElapsedGameTime.Milliseconds;
                        if (fps >= 70)
                        {
                            fps = 0;
                            if (kb.IsKeyDown(Keys.Right) && seleccion < 5)
                                seleccion++;
                            if (kb.IsKeyDown(Keys.Left) && seleccion > 1)
                                seleccion--;

                            if (kb.IsKeyDown(Keys.Enter))
                            {
                                if (seleccion == 1)
                                    gameState = State.Niveles;
                                if (seleccion == 2)
                                {

                                    using (FileStream fs = (new FileStream(@"save", FileMode.OpenOrCreate)))
                                    {
                                        if (fs.Length != 0)
                                        {
                                            XmlSerializer xmlser = new XmlSerializer(typeof(Scores));
                                            jugador.score = (Scores)xmlser.Deserialize(fs);
                                        }
                                    }

                                    using (FileStream fs = new FileStream(@"save2", FileMode.OpenOrCreate))
                                    {
                                        if (fs.Length != 0)
                                        {
                                            XmlSerializer xmlser = new XmlSerializer(typeof(bool[][]));

                                            jugador.habilidades = (bool[][])xmlser.Deserialize(fs);
                                            jugador.SuperBalas1 = jugador.habilidades[0][0];
                                            jugador.SuperBalas2 = jugador.habilidades[1][0];
                                            jugador.SuperBalas3 = jugador.habilidades[2][0];

                                            jugador.SuperEscudo1 = jugador.habilidades[0][1];
                                            jugador.SuperEscudo2 = jugador.habilidades[1][1];
                                            jugador.SuperEscudo3 = jugador.habilidades[2][1];

                                            jugador.DirBalas1 = jugador.habilidades[0][2];
                                            jugador.DirBalas2 = jugador.habilidades[1][2];
                                            jugador.DirBalas3 = jugador.habilidades[2][2];
                                        }
                                    }
                                    gameState = State.Niveles;
                                    seleccion = 0;
                                }
                                if (seleccion == 3)
                                {
                                    gameState = State.Instrucciones;
                                    seleccion = 0;
                                }
                                if (seleccion == 4)
                                {
                                    using (FileStream fs = new FileStream(@"save", FileMode.Create))
                                    {
                                        XmlSerializer xmlser = new XmlSerializer(typeof(Scores));
                                        xmlser.Serialize(fs, jugador.score);
                                    }
                                    using (FileStream fs = new FileStream(@"save2", FileMode.Create))
                                    {
                                        XmlSerializer xmlser = new XmlSerializer(typeof(bool[][]));
                                        xmlser.Serialize(fs, jugador.habilidades);
                                    }
                                    finDeJuego = true;
                                }
                                if (seleccion == 5)
                                {
                                    gameState = State.SelectShip;
                                    seleccion = 2;
                                }

                                MediaPlayer.Play(jugador.sn.bgMusic);
                                fps = 0;
                               
                            }
                        }

                        break;
                    }
                case State.SelectShip:
                    {
                        fps += gameTime.ElapsedGameTime.Milliseconds;
                        if (fps >= maxfps)
                        {
                            fps = 0;
                            if (kb.IsKeyDown(Keys.Right) && seleccion < 3)
                                seleccion++;
                            if (kb.IsKeyDown(Keys.Left) && seleccion > 1)
                                seleccion--;
                            if (kb.IsKeyDown(Keys.Enter))
                            {
                                if (seleccion == 1)
                                {
                                    jugador.texturaNave = content.Load<Texture2D>("nave");
                                    gameState = State.Menu;
                                }
                                if (seleccion == 2)
                                {
                                    jugador.texturaNave = content.Load<Texture2D>("ship2");
                                    gameState = State.Menu;
                                }
                                if (seleccion == 3)
                                {
                                    jugador.texturaNave = content.Load<Texture2D>("ship3");
                                    gameState = State.Menu;
                                }
                            }
                        }
                        
                        break;
                    }
                case State.Niveles:
                    {
                        fps += gameTime.ElapsedGameTime.Milliseconds;
                        if (fps >= maxfps)
                        {
                            fps = 0;

                            if (kb.IsKeyDown(Keys.Right) && seleccion < 4)
                                seleccion++;
                            if (kb.IsKeyDown(Keys.Left) && seleccion > -1)
                                seleccion--;

                            if (seleccion >= jugador.score.Licencia + 1)
                            {
                                seleccion--;
                            }
                            if (kb.IsKeyDown(Keys.Enter))
                            {
                                MediaPlayer.Play(jugador.sn.bgMusic);
                                if (seleccion == jugador.score.Licencia)
                                {
                                    gameState = State.Tienda;
                                    seleccion = 1;
                                    fps = 0;
                                    break;
                                }
                                if (seleccion == -1)
                                {
                                    gameState = State.Menu;
                                    fps = 0;
                                    break;
                                }
                                if (!Niveles[seleccion].level_passed)
                                    gameState = State.Playing;
                            }
                        }

                        break;
                    }
                case State.Playing:
                    {

                        jugador.Update(gameTime);
                        Niveles[seleccion].Update(gameTime, jugador);

                        if (Niveles[seleccion].level_passed)
                        {

                            gameState = State.PasandoNivel;
                            Niveles[seleccion].level_passed=false;

                        }
                        if (Niveles[seleccion].Murio)
                            gameState = State.Gameover;

                        if (kb.IsKeyDown(Keys.Back))
                        {
                            gameState = State.Niveles;
                            //fps = 0;
                        }
                        // }

                        break;
                    }
                case State.Tienda:
                    {
                        chopin.Update(gameTime, jugador);
                        if (chopin.salir)
                        {
                            gameState = State.Niveles;
                            chopin.salir = false;
                        }


                        break;
                    }
                case State.PasandoNivel:
                    {
                        int fps2=0;
                        fps2 += gameTime.ElapsedGameTime.Seconds;
                        if (fps2 >= 4||kb.IsKeyDown(Keys.Enter))
                        {
                            fps2 = 0;
                            gameState = State.Niveles;
                        }

                        break;
                    }
                case State.Gameover:
                    {
                        if (kb.IsKeyDown(Keys.Enter))
                        {
                            finDeJuego = true;
                        }
                        break;
                    }
                case State.Instrucciones:
                    {
                        fps += gameTime.ElapsedGameTime.Milliseconds;
                        if (fps >= maxfps)
                        {
                            fps = 0;
                            if (kb.IsKeyDown(Keys.Back) || kb.IsKeyDown(Keys.Enter))
                            {
                                gameState = State.Menu;
                            }
                        }
                        break;
                    }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            fondo.Draw(spriteBatch);
            switch (gameState)
            {
                case State.Menu:
                    {
                        spriteBatch.Draw(texturaFondo, new Vector2(0), Color.White);
                        spriteBatch.Draw(logo, new Vector2(350, 150), Color.White);

                        if (seleccion == 1)
                            spriteBatch.Draw(botones[1], new Vector2(posiciones[0].X - 35, posiciones[0].Y - 35), Color.White);
                        else
                            spriteBatch.Draw(botones[0], posiciones[0], Color.White);
                        if (seleccion == 2)
                            spriteBatch.Draw(botones[3], new Vector2(posiciones[1].X - 35, posiciones[1].Y - 35), Color.White);
                        else
                            spriteBatch.Draw(botones[2], posiciones[1], Color.White);
                        //instrucciones------------------------------------------------
                        if (seleccion == 3)
                            spriteBatch.Draw(botones[7], new Vector2(800-35, 100-35), Color.White);
                        else
                            spriteBatch.Draw(botones[6], new Vector2(800, 100), Color.White);

                        if (seleccion == 4)
                            spriteBatch.Draw(botones[5], new Vector2(posiciones[2].X - 35, posiciones[2].Y - 35), Color.White);
                        else
                            spriteBatch.Draw(botones[4], posiciones[2], Color.White);

                        if (seleccion == 5)
                            spriteBatch.Draw(botones[9], new Vector2(500-35, 500-35), Color.White);
                        else
                            spriteBatch.Draw(botones[8], new Vector2(500, 500), Color.White);
                        break;
                    }
                    case State.SelectShip:
                    {

                        spriteBatch.Draw(textSelectShip, new Vector2(130,85), new Rectangle(0, 0, textSelectShip.Width, textSelectShip.Height),
                           Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0.5f);
                        if (seleccion == 1)
                        {
                            spriteBatch.Draw(naves_selec, new Vector2(300-30,234),Color.White);
                        }
                        if (seleccion == 2)
                        {
                            spriteBatch.Draw(naves_selec, new Vector2(520-30,234), Color.White);
                        }
                        if (seleccion == 3)
                        {
                            spriteBatch.Draw(naves_selec, new Vector2(740-30,234),Color.White);
                        }
                        break;
                    }
                case State.PasandoNivel:
                    {
                        jugador.DrawScore(spriteBatch);
                        spriteBatch.Draw(textPasado,new Vector2(300,200),Color.White );
                        break;
                    }
                case State.Niveles:
                    {
                        if (seleccion >= 0 && seleccion <= 5 && seleccion != jugador.score.Licencia)
                            spriteBatch.Draw(textNiveles[seleccion], new Vector2(0), Color.White);
                        else
                            spriteBatch.Draw(texturaOpciones, new Vector2(0), Color.White);
                        //-----------------------------------------------------------------------------------------------------------
                        if (seleccion == jugador.score.Licencia)
                            spriteBatch.Draw(TiendaText[1], TiendaPos, Color.White);
                        else
                            spriteBatch.Draw(TiendaText[0], TiendaPos, Color.White);
                        if (seleccion == -1)
                            spriteBatch.Draw(AtrasText[1], new Vector2(TiendaPos.X - 800, TiendaPos.Y), Color.White);
                        else
                            spriteBatch.Draw(AtrasText[0], new Vector2(TiendaPos.X - 800, TiendaPos.Y), Color.White);


                        break;
                    }
                case State.Playing:
                    {

                        jugador.Draw(spriteBatch);
                        Niveles[seleccion].Draw(spriteBatch);
                        break;
                    }
                case State.Gameover:
                    {
                        spriteBatch.Draw(textGameOver, new Vector2(270, 170), Color.White);

                        break;
                    }
                case State.Tienda:
                    {
                        chopin.Draw(spriteBatch);
                        break;
                    }
                case State.Instrucciones:
                    {
                        spriteBatch.Draw(textInstruc, new Vector2(0), Color.White);
                        break;
                    }

            }
        }

    }


}
