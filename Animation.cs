using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CannonGame
{
    public class Animation : Sprite
    {

        private Texture2D _texture;
        public List<Sprite> normalAnimation;
        public Sprite Sprite;
        int frames = 0;
        float waitTime = 1;
        int count = 16;
        bool timerOn = false, timerTwo = false;

        public Animation(Texture2D texture, Sprite? sprite) : base(texture, null)
        {
            _texture = texture;
            Height = texture.Height;
            frames = (int)texture.Width / (int)Height;
            Sprite = sprite;
            InitAnimation();
            
        }

        //method to slice up the file into usable sprites
        private void InitAnimation()
        {
            normalAnimation = new List<Sprite>();            
            for (int i = 0; i < frames; i++)
            {
                if (Sprite != null)
                    normalAnimation.Add
                    (
                        new Sprite(_texture, null)
                        {
                            Position = new Vector2(i * 64, 0),

                        }
                    );
            }
        }

        // method to iterate through each sprite
        public void Animate(float animationSpeed, SpriteBatch spriteBatch, GameTime gameTime)
        {
            timerOn = true;


            if (!timerOn)
            {
                timerOn = true;
            }

            if (timerOn)
            {
                waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                timerTwo = true;
                Cycle(spriteBatch);
  
                if (waitTime <= 0)
                {
                    waitTime = 1;
                    timerOn = false;
                }
            }

        }

        private void Cycle(SpriteBatch spriteBatch)
        {
            if (timerTwo)
            {
                
                foreach (Sprite sp in normalAnimation)
                {   count--;
                    if (count % 2 == 0)
                    {
                        sp.Position = Sprite.Position - new Vector2(1100, 0);
                        sp.Draw(spriteBatch);
                    }
                    if (count == 0)
                    timerTwo = false;
                }
                
                
            }
        }

        //

    }
}
