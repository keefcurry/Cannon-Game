using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CannonGame
{
    public class AstroidGame
    {

        public int Score;
        public Cannon Cannon;
        public static List<Sprite> _astriods;
        public Base _base;
        public Overlay _overlay;
        public Animation astroidAnimation;
        private GraphicsDeviceManager graphics;
        bool isPause = false;
        bool isPauseKeyDownHandled = false;


        public AstroidGame(GraphicsDeviceManager _graphics)
        {
            Cannon = new Cannon(Game1.Cannon)
            {
                Position = new Vector2(80, 660),
                Input = new Input
                {
                    RotateLeft = Keys.A,
                    RotateRight = Keys.D,
                    Shoot = Keys.Space,
                    Pause = Keys.E
                },
                Rotation = 0.0f,
                Color = Color.White,
                Speed = 2
            };
            _astriods = new List<Sprite>()
            {
                new Astroid(Game1.Astroid),
                new Astroid(Game1.Astroid),
                new Astroid(Game1.Astroid)
            };

            _base = new Base(Game1.Base)
            {
                Width = 144,
                Height = 496,
                Origin = new Vector2(0,0),
                Position = new Vector2(0,112) //144, 624

            };

            _overlay = new Overlay(Game1.BlackBar) 
            {

            };

            astroidAnimation = new Animation(Game1.AstroidSheet, null);

            graphics = _graphics;
        }



        public virtual void Update(GameTime gameTime)
        {
            CheckForPause(Cannon.ReturnPause);
            //CheckForPause();
            if(!isPause)
            {
                if (Globals.ShipsStatus >= 1)
                    _base.Update(gameTime, _astriods);
                Cannon.Update(gameTime, _astriods);
                foreach (Sprite astroid in _astriods)
                {
                    astroid.Update(gameTime, _astriods);
                    astroid.HitBase(_base);
                }
            }

            Debug.WriteLine(isPause);


        }

        /// <summary>
        /// Draw in layers 
        ///     1 Player
        ///     2 Items/Tiles
        ///     3 Background
        ///     4 Overlay
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Game1.Background, new Rectangle(0, 0,graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            if(Globals.ShipsStatus >= 1)
                _base.Draw(spriteBatch);
            Cannon.Draw(spriteBatch);
            foreach (Sprite astroid in _astriods)
            {
                astroid.Draw(spriteBatch);
                astroid.CallAnimation(2,spriteBatch);
            }

            _overlay.Draw(spriteBatch);
            if (isPause)
                DrawPauseScreen(spriteBatch);         
        }

        public void DrawPauseScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.Pause, new Rectangle(0, 0, 1280, 720), Color.White);
        }

        private void CheckForPause(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key))
            {
                if (!isPauseKeyDownHandled)
                {
                    isPause = !isPause;
                    isPauseKeyDownHandled = true;
                }
            }
            else
            {
                isPauseKeyDownHandled = false;
            }

        }
    }
}
