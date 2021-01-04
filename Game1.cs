using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CannonGame
{
    public class Game1 : Game
    {
        public static Texture2D Cannon, Astroid, Bullet, Background, HitBox, Base, BlackBar, Bar, BarShade, Glow, bbBullet, bbGlow, AstroidSheet;
        public static SpriteFont spriteFont;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AstroidGame astroidGame;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here
            spriteFont = Content.Load<SpriteFont>("newFont");
            AstroidSheet = Content.Load<Texture2D>("Astroid-sheet");
            Cannon = Content.Load<Texture2D>("Cannon");
            Astroid = Content.Load<Texture2D>("Astroid");
            Bullet = Content.Load<Texture2D>("Bullet");
            HitBox = Content.Load<Texture2D>("Hitbox");
            Base = Content.Load<Texture2D>("Base");
            Bar = Content.Load<Texture2D>("Bar");
            BlackBar = Content.Load<Texture2D>("BlackBar");
            BarShade = Content.Load<Texture2D>("BarShade");
            Background = Content.Load<Texture2D>("Background");
            Glow = Content.Load<Texture2D>("Blur");
            bbBullet = Content.Load<Texture2D>("bbBullet");
            bbGlow = Content.Load<Texture2D>("bbGlow");

            astroidGame = new AstroidGame(_graphics);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            astroidGame.Update(gameTime);
            Debug.WriteLine(Globals.ShipsStatus);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            astroidGame.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
