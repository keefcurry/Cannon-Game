using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CannonGame
{
    public class Sprite
    {

        protected Texture2D _texture;
        protected Texture2D _glow;

        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;
        public float gravity;
        public Input Input;
        public Color Color;
        public Color GlowColor;
        public float Rotation = 0;
        public Vector2 Origin;
        public bool Hit = false;

        public float Width = 0, Height = 0;

        public Rectangle Rectangle
        {
            get
            {
                if (Width == 0)
                    return new Rectangle((int)Position.X, (int)Position.Y, (int)Globals.Scale, (int)Globals.Scale);
                else
                    return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }

        public bool Alive { get; internal set; }

        public Sprite(Texture2D texture, Texture2D? glow)
        {
            _glow = glow;
            _texture = texture;
            Color = Color.White;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void HitBase(Base _base) { }

        public virtual void CallAnimation(float animationSpeed, SpriteBatch spriteBatch) { }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }
        private void DrawGlow(SpriteBatch spriteBatch)
        {
            if (_glow != null)
                spriteBatch.Draw(_glow, Rectangle, null, new Color(255,0,0,0.7f), Rotation, Origin, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {   //Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth
            spriteBatch.Draw(_texture, Rectangle, null, Color,Rotation, Origin, SpriteEffects.None, 0 );
            if (_glow != null)
                DrawGlow(spriteBatch);
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion

    }
}