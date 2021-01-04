using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CannonGame
{
    public class Base : Sprite
    {
        private Texture2D _texture;
        float waitTime = 1;
        bool timerOn = false,timerTwo = false, UpDown = false;
        int count = 5;

        public Base(Texture2D texture) : base(texture,null)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (Sprite sprite in sprites)
                CheckForHit(sprite, gameTime);
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void CheckForHit(Sprite sprite, GameTime gameTime)
        {

            if (sprite.Hit == true || timerOn)
            {
                if(!timerOn)
                {
                    Globals.ShipsStatus -= 1.2f + sprite.Speed;
                    sprite.Hit = false;
                    
                    timerOn = true;
                }
                
                if(timerOn)
                {
                    waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Debug.WriteLine("---- " + (float)gameTime.ElapsedGameTime.TotalMilliseconds);
                    Color = new Color(255,0,0);
                    timerTwo = true;

                    ShakeEffect();
                    
                    if(waitTime <= 0)
                    {
                        waitTime = 1;
                        timerOn = false;
                    }
                }
            }
            else
                Color = Color.White;

        }

        private void ShakeEffect()
        {
            if(timerTwo)
            {
                count--;
                if(count % 2 == 0)
                {
                    if (!UpDown)
                    {
                        Position.Y += 10;
                        UpDown = true;
                    }
                    else
                    {
                        Position.Y -= 10;
                        UpDown = false;
                    }
                }

                if (count == 0)
                    timerTwo = false;
            }

        }

    }
}
