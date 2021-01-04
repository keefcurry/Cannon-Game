using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

namespace CannonGame
{
    public class Overlay : Sprite
    {

        private int x = 512, y = 650; 
        public Overlay(Texture2D texture) : base(texture, null)
        {

        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawShipStatus(spriteBatch);
            FillShipStatus(spriteBatch);
            DrawScore(spriteBatch);
            DrawAmmo(spriteBatch);
            //base.Draw(spriteBatch);
        }


        private void DrawShipStatus(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.BlackBar, new Rectangle(x-4, y-2, Game1.BlackBar.Width * 2 + 10, Game1.BlackBar.Height+4), Color.White);
        }

        public void FillShipStatus(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < Globals.ShipsStatus; i++)
            {
                spriteBatch.Draw(Game1.BarShade, new Rectangle(x + i * 2 , y, Game1.BarShade.Width, Game1.BlackBar.Height), Color.White);
            }
        }

        public void DrawScore(SpriteBatch spriteBatch)
        {
            string s = "SCORE   " + Globals.Score.ToString() + "";
            spriteBatch.DrawString(Game1.spriteFont, s, new Vector2(Globals.viewportRectangle.Width - 150, y), Color.White);
        }

        public void DrawAmmo(SpriteBatch spriteBatch)
        {
            string s = "" + Cannon.hitboxes.Count;
            Rectangle r = new Rectangle(Globals.viewportRectangle.Width - 250, y - ((int)Globals.Scale/2), (int)Globals.Scale, (int)Globals.Scale);
            spriteBatch.Draw(Game1.Bullet, r, Color.White);
            spriteBatch.DrawString(Game1.spriteFont, s, new Vector2(r.X+70, r.Y+70), Color.White);
        }
    }
}
