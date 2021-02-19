
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreaker
{
    class Brick
    {
        Texture2D _tex;
        Color _tint;
        Vector2 _pos;
        Vector2 _size;

        public Rectangle hitBox => new Rectangle((int)_pos.X, (int)_pos.Y, (int)_size.X, (int)_size.Y);
        public int Hp;

        public Brick(int hp, Texture2D tex, Color tint, Vector2 pos, Vector2 size)
        {
            Hp = hp;
            _tex = tex;
            _tint = tint;
            _pos = pos;
            _size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Hp > 0)
            {
                spriteBatch.Draw(_tex, hitBox, _tint);
            }
        }

    }
}
