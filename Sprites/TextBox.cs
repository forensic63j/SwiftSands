using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
    class TextBox : Sprite
    {
        private SpriteFont font;

        public TextBox(Texture2D texture, Rectangle position, bool isActive, String name, SpriteFont font) : base(texture, position, isActive, name)
        {
            this.font = font;
        }

        public SpriteFont Font
        {
            get
            {
                return font;
            }
        }

        public void Update()
        {
            if (IsActive)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.GetPressedKeys().Length > 0)
                {
                    IsActive = false;
                }
            }
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (IsActive)
            {
                batch.Draw(this.Texture, this.Position, Color.White);
                batch.DrawString(font, this.Name, new Vector2(this.Position.X + 5, this.Position.Y + 2), Color.Black);
            }
        }
    }
}
