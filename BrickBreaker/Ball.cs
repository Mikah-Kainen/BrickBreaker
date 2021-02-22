using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreaker
{
    class Ball
    {
        Texture2D _tex;
        Color _tint;
        Vector2 _pos;
        Vector2 _size;
        public int XSpeed;
        public int YSpeed;

        public Rectangle hitBox => new Rectangle((int)_pos.X, (int)_pos.Y, (int)_size.X, (int)_size.Y);

        public Ball(Texture2D tex, Color tint, Vector2 pos, Vector2 size)
        {
            _tex = tex;
            _tint = tint;
            _pos = pos;
            _size = size;
            XSpeed = 4;
            YSpeed = -12;
        }

        public bool Update(GameTime gameTime, Rectangle screen)
        {
            if (_pos.X + _size.X > screen.Right)
            {
                XSpeed = -1 * Math.Abs(XSpeed);
            }
            else if(_pos.X < screen.Left)
            {
                XSpeed = Math.Abs(XSpeed);
            }

            if (_pos.Y + _size.Y > screen.Bottom)
            {
                Console.WriteLine("You Lost");
                //YSpeed = -1 * Math.Abs(YSpeed);
                return true;
            }
            else if (_pos.Y < screen.Top)
            {
                YSpeed = Math.Abs(YSpeed);
            }

            _pos.X += XSpeed;
            _pos.Y += YSpeed;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tex, hitBox, _tint);
        }
    }
}
