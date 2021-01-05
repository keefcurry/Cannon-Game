using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CannonGame
{
    public class SplashScreen
    {
        private bool start = false;
        private bool pause = false;
        private Texture2D backgroundSplash;
        private Texture2D playButton;
        private Rectangle playButtonRect = new Rectangle(468, 440, 398, 109);
        private MouseState mouseState;
        public Input input;

        public SplashScreen()
        {
            backgroundSplash = Game1.Splash;
            playButton = Game1.PlayButton;

        }

        public bool Play()
        {
            return start;
        }

        public bool Pause()
        {
            return pause;
        }

        public virtual void Update(GameTime gameTime)
        {
            MousePos();
        }

        public void MousePos()
        {
            mouseState = Mouse.GetState();
            if (playButtonRect.Contains(mouseState.X, mouseState.Y) && ButtonState.Pressed == mouseState.LeftButton)
                start = true;
        }

        public void DrawSplash(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundSplash, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(playButton, playButtonRect, Color.White);
        }


    }
}
