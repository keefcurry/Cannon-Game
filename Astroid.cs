using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CannonGame
{
    public class Astroid : Sprite
    {
        Random random = new Random();
        private Texture2D _texture;
        private bool animate = false;
        private Vector2 contactPosition;
        private Animation animation;
        private GameTime _gameTime;

        public Astroid(Texture2D texture) : base(texture,null)
        {
            _texture = texture;
            SetRandomYPos();
            Position.X = 1280 - texture.Width;
            DifficultyLevelAdjust();
            SetVelocity();
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Rotation = random.Next(0, 3);
            animation = new Animation(Game1.AstroidSheet, this);
            
        }
        private void RotataionAnimation()
        {
            if (Speed <= 3)
                Rotation -= .05f;
            else
                Rotation += .03f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _gameTime = gameTime;
            RotataionAnimation();
            CheckBounds();
            Move();
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        // Set the astroid to respawn after it reaches the other side
        private void CheckBounds()
        {
            if (!Globals.viewportRectangle.Contains(Position))
            {
                ResetAstroid();
            }
        }

        public void ResetAstroid()
        {
            Position.X = Globals.viewportRectangle.Width;
            Position.X = 1280 - _texture.Width;
            SetRandomYPos();
            DifficultyLevelAdjust();
            SetVelocity();
        }

        private void SetRandomYPos()
        {
            Position.Y = random.Next(5, 500);
        }
        
        private void Move()
        {
            Position -= Velocity;

        }

        private void SetVelocity()
        {
            Velocity = new Vector2(Speed, 0);
        }

        public override void HitBase(Base _base)
        {
            if (Rectangle.Intersects(_base.Rectangle))
            {
                animate = true;
                contactPosition = Position;
                ResetAstroid();
                Hit = true;
            }

            base.HitBase(_base);
        }

        public override void CallAnimation(float animationSpeed, SpriteBatch spriteBatch)
        {
            if (animate)
            {
                animation.Animate(animationSpeed, spriteBatch, _gameTime);
                animate = false;
            }
        }

        public void DifficultyLevelAdjust()
        {
            if (Globals.Score >= 175)
                Speed = random.Next(10, 13);
            if (Globals.Score >= 165)
                Speed = random.Next(9, 12);
            if (Globals.Score >= 150)
                Speed = random.Next(8, 11);
            if (Globals.Score >= 135)
                Speed = random.Next(7, 10);
            else if (Globals.Score >= 115)
                Speed = random.Next(6, 9);
            else if (Globals.Score >= 90)
                Speed = random.Next(5, 8);
            else if (Globals.Score >= 75)
                Speed = random.Next(4, 7);
            else if (Globals.Score >= 45)
                Speed = random.Next(3, 6);
            else if(Globals.Score >= 0)
                Speed = random.Next(2, 5);
        }

    }
}