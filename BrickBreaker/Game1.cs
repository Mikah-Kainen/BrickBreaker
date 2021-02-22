using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BrickBreaker
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _brickSizeX;
        private int _brickIncrimentX;
        private int _brickSizeY;
        private int _brickIncrimentY;
        private int _yScreen;

        List<Brick> _bricks;
        Ball _ball;
        Paddle _paddle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            _yScreen = GraphicsDevice.Viewport.Bounds.Height / 2;

            _brickIncrimentX = 15;
            _brickIncrimentY = 15;
   
            _brickSizeX = GraphicsDevice.Viewport.Bounds.Width / _brickIncrimentX;
            _brickSizeY = _yScreen / _brickIncrimentY;

            _bricks = new List<Brick>();
            _ball = new Ball(CreatePixel(GraphicsDevice), Color.Blue, new Vector2(0, GraphicsDevice.Viewport.Height * 2 / 3), new Vector2(GraphicsDevice.Viewport.Width / 50, GraphicsDevice.Viewport.Width / 50));
            _paddle = new Paddle(CreatePixel(GraphicsDevice), Color.DarkGray, new Vector2(0, GraphicsDevice.Viewport.Bounds.Height - 50), new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 50), 10);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int x = 0; x < _brickIncrimentY; x ++)
            {
                for (int i = 0; i < _brickIncrimentX; i++)
                {
                    _bricks.Add(new Brick(CreatePixel(GraphicsDevice), Color.Red, new Vector2(i * _brickSizeX, x * _brickSizeY), new Vector2(_brickSizeX, _brickSizeY), 1));
                }
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            bool didChange = false;
            foreach(Brick brick in _bricks)
            {
                if(brick.Hp > 0 && brick.hitBox.Intersects(_ball.hitBox))
                {
                    brick.Hp--;
                    if (!didChange)
                    {
                        _ball.YSpeed *= -1;
                        //_ball.XSpeed *= -1;
                        didChange = true;
                    }
                    //else
                    //{
                    //    _ball.XSpeed *= -1;
                    //}
                }
            }

            if(_ball.hitBox.Intersects(_paddle.hitBox))
            {
                _ball.YSpeed *= -1;
                if(_paddle.direction > 0)
                {
                    _ball.XSpeed = Math.Abs(_ball.XSpeed);
                }
                else if(_paddle.direction < 0)
                {
                    _ball.XSpeed = -1 * Math.Abs(_ball.XSpeed);
                }
            }


            // TODO: Add your update logic here
            _ball.Update(gameTime, GraphicsDevice.Viewport.Bounds);
            _paddle.Update(gameTime);
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
            _paddle.Draw(_spriteBatch);
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
