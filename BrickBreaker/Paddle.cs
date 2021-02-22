using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreaker
{
    class Paddle
    {
        Texture2D _tex;
        Color _tint;
        Vector2 _pos;
        Vector2 _size;
        public int XSpeed;
        public int direction;

        public Rectangle hitBox => new Rectangle((int)_pos.X, (int)_pos.Y, (int)_size.X, (int)_size.Y);

        public Paddle(Texture2D tex, Color tint, Vector2 pos, Vector2 size, int xSpeed)
        {
            _tex = tex;
            _tint = tint;
            _pos = pos;
            _size = size;
            XSpeed = xSpeed;
            direction = 0;
        }

        public void Update(GameTime gameTime1, Rectangle gameBounds)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = 1;
                _pos.X += XSpeed;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = -1;
                _pos.X -= XSpeed;
            }
            else
            {
                direction = 0;
                if (gameBounds.Contains(Mouse.GetState().Position))
                {
                    _pos.X = Mouse.GetState().X - _size.X / 2;
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tex, hitBox, _tint);
        }
    }
}
