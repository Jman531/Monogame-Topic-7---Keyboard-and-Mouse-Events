using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_Topic_7___Keyboard_and_Mouse_Events
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D pacDownTexture, pacUpTexture, pacLeftTexture, pacRightTexture, pacSleepTexture;
        Rectangle pacLocation;

        SpriteFont instructionFont;

        KeyboardState keyboardState, oldKeyboardState;

        MouseState mouseState;

        Random generator = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 600);
            pacLocation = new Rectangle(10, 10, 75, 75);

            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pacDownTexture = Content.Load<Texture2D>("PacDown");
            pacUpTexture = Content.Load<Texture2D>("PacUp");
            pacLeftTexture = Content.Load<Texture2D>("PacLeft");
            pacRightTexture = Content.Load<Texture2D>("PacRight");
            pacSleepTexture = Content.Load<Texture2D>("PacSleep");
            instructionFont = Content.Load<SpriteFont>("Instructions");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                pacLocation.Y -= 2;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                pacLocation.Y += 2;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                pacLocation.X -= 2;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                pacLocation.X += 2;
            }

            if (oldKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                pacLocation.X = generator.Next(0, 726);
                pacLocation.Y = generator.Next(0, 526);
            }

            if (pacLocation.Left >= window.Width)
            {
                pacLocation.X = -74;
            }
            if (pacLocation.Right <= 0)
            {
                pacLocation.X = window.Width;
            }
            if (pacLocation.Top >= window.Height)
            {
                pacLocation.Y = -74;
            }
            if (pacLocation.Bottom <= 0)
            {
                pacLocation.Y = window.Height;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                pacLocation.X = mouseState.X - 37;
                pacLocation.Y = mouseState.Y - 37;
            }

            oldKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                _spriteBatch.Draw(pacUpTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                _spriteBatch.Draw(pacDownTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                _spriteBatch.Draw(pacLeftTexture, pacLocation, Color.White);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                _spriteBatch.Draw(pacRightTexture, pacLocation, Color.White);
            }
            else
            {
                _spriteBatch.Draw(pacSleepTexture, pacLocation, Color.White);
            }

            _spriteBatch.DrawString(instructionFont, "Use WASD to move", new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
