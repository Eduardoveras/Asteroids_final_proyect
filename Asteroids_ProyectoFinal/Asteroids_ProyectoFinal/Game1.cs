using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Asteroids_ProyectoFinal
{
    public class Game1 : Game
    {


        //*******************************************************
        //Juego desarrollado por Eduardo Veras & Ernesto Rodrigues
        //parael proyecto final de la materia de programacion 
        //aplicada de la Pontificia Univeridad catolica Madre y Maestra
        //*********************************************************



        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //Asteroids juego

        public Juego menu = new Juego();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 720;
            Window.Title = "...............Asteroids...............";
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Aqui cargamos el contenido de los objetos que utilizaremos
            //al igual que las texturas para los fondos
            menu.LoadContent(Content);
        }



        protected override void Update(GameTime gameTime)
        {

            menu.Update(gameTime,Content);
            
            if (menu.finDeJuego)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            menu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
