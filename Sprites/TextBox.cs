using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
    class TextBox
    {
        private SpriteFont font;
        private bool isActive;
        private String text;
        private Rectangle position;
        private Texture2D texture;
        private SpriteBatch spriteBatch;

        #region Singleton
        private static TextBox _instance;

        public static TextBox Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TextBox();
                return _instance;
            }
        }

        private TextBox()
        {
            
        }
        #endregion

        #region Properties
        public SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }

        public bool IsActive
        {
            get 
            {
                return isActive; 
            }
            set
            {
                isActive = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return spriteBatch;
            }
            set
            {
                spriteBatch = value;
            }
        }
        #endregion

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

        public void Draw()
        {
            if (IsActive)
            {
                spriteBatch.Draw(this.Texture, this.Position, Color.White);
                spriteBatch.DrawString(font, this.Text, new Vector2(this.Position.X + 15, this.Position.Y + 2), Color.Black);
            }
        }
    }
}
