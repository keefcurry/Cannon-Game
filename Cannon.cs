using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CannonGame
{
    public class Cannon : Sprite
    {
        public int Lives; // 3 lives

        public static List<Sprite> hitboxes;
        float previousStatus = Globals.ShipsStatus;
        int previousScore, ScoreMultiplier = 0;
        bool AddOne = false;
        int ammoScore = 0;
        float bulletWidth = 6.4f * 8;
        float bulletHeight = 6.4f * 7;
        public Keys ReturnPause { get; set; }
        public Cannon(Texture2D texture) : base(texture,null)
        {
            texture = Game1.Cannon;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //if(Bullet!=null)
            // Bullet.Update(gameTime, sprites);
            hitBoxInit();
            IncreaseAmmo();
            CannonControls();

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //ShootBullet();
            //Bullet.Draw(spriteBatch);
            AimBox(spriteBatch);
            //Debug.WriteLine(hitbox.Rotation + " : " + Rotation);

            base.Draw(spriteBatch);

        }

        private void hitBoxInit()
        {
            if (hitboxes == null)
                hitboxes = new List<Sprite>
                {
                    new Sprite(Game1.bbBullet,Game1.bbGlow)
                    {
                        Position = new Vector2(Position.X, Position.Y),
                        Alive = false,
                        GlowColor = Color.Red,
                        Width = bulletWidth,
                        Height = bulletHeight
                    },
                    new Sprite(Game1.bbBullet,Game1.bbGlow)
                    {
                        Position = new Vector2(Position.X, Position.Y),
                        Alive = false,
                        GlowColor = Color.Red,
                        Width = bulletWidth,
                        Height = bulletHeight
                    },
                    new Sprite(Game1.bbBullet,Game1.bbGlow)
                    {
                        Position = new Vector2(Position.X, Position.Y),
                        Alive = false,
                        GlowColor = Color.Red,
                        Width = bulletWidth,
                        Height = bulletHeight
                    },

                };
        }

        private void AimBox(SpriteBatch spriteBatch)
        {
            if (hitboxes != null)
                foreach (Sprite hitbox in hitboxes)
                    hitbox.Draw(spriteBatch);
        }



        private void rotateBox()
        {
            if (hitboxes != null)
            {
                foreach (Sprite hitbox in hitboxes)
                    if (!hitbox.Alive)
                    {
                        hitbox.Alive = true;
                        hitbox.Position = Position;
                        hitbox.Rotation = Rotation;
                        hitbox.Velocity = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)) * 10.0f;
                        return;
                    }
            }
        }

        private void shootBox()
        {
            foreach (Sprite hitbox in hitboxes)
                if (hitbox.Alive)
                {
                    hitbox.Position += hitbox.Velocity;
                    if (!Globals.viewportRectangle.Contains((int)hitbox.Position.X, (int)hitbox.Position.Y))
                    {
                        hitbox.Alive = false;
                        hitbox.Position = Position;
                    }
                    foreach(Astroid astroid in AstroidGame._astriods)
                    {
                        if (astroid.Rectangle.Intersects(hitbox.Rectangle))
                        {
                            astroid.ResetAstroid();
                            hitbox.Alive = false;
                            hitbox.Position = Position;
                            Globals.Score += 1;
                            ammoScore++;
                        }
                    }
                }


        }

        private void CannonControls()
        {

            if (Keyboard.GetState().IsKeyDown(Input.RotateLeft))
                Rotation -= 0.05f;
            if (Keyboard.GetState().IsKeyDown(Input.RotateRight))
                Rotation += 0.05f;
            if (Keyboard.GetState().IsKeyDown(Input.Pause))
                ReturnPause = Keys.E;
            Input.GetState();
            if (Input.HasBeenPressed(Keys.Space))
            {
                rotateBox();
                Debug.WriteLine("Shoot");
                shootBox();
            }
            shootBox();

            Rotation = MathHelper.Clamp(Rotation, -MathHelper.PiOver2, 0);
        }

        private void IncreaseAmmo()
        {
            while (ammoScore >= 45)
            {
                if (hitboxes != null)
                    hitboxes.Add
                    (
                        new Sprite(Game1.bbBullet, Game1.bbGlow)
                        {
                            Position = new Vector2(Position.X, Position.Y),
                            Alive = false,
                            GlowColor = Color.Red,
                            Width = bulletWidth,
                            Height = bulletHeight
                        }
                    );
                ammoScore -= 45;
            }
        }
    }


}
