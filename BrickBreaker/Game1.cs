using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace BrickBreaker
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Brick> _bricks;
        Ball _ball;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            _bricks = new List<Brick>();
            _ball = new Ball(CreatePixel(GraphicsDevice), Color.Blue, new Vector2(300, 250), new Vector2(20, 20));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 17; i++)
            {
                _bricks.Add(new Brick(2, CreatePixel(GraphicsDevice), Color.Red, new Vector2(i * 50, 0), new Vector2(50, 50)));
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(Brick brick in _bricks)
            {
                if(brick.Hp > 0 && brick.hitBox.Intersects(_ball.hitBox))
                {
                    brick.Hp--;
                    _ball.YSpeed *= -1;
                }
            }

            _ball.Update(gameTime, GraphicsDevice.Viewport.Bounds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            foreach(Brick brick in _bricks)
            {
                brick.Draw(_spriteBatch);
            }

            _ball.Draw(_spriteBatch);

            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private Texture2D CreatePixel(GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, 1, 1);

            texture.SetData(new Color[] { Color.White });

            return texture;
        }


        // IF it hits in the middlle of two bricks the speed is changed 2 times so ends up normals and both bricks get hit twice!!
    }
}
