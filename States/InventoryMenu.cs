using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SwiftSands
{
    class InventoryMenu : State
    {
        private SpriteFont font;
        Texture2D texture;
        private MouseState mState;

        public InventoryMenu(SpriteFont font, Texture2D sprite, Game1 game, Viewport port)
            : base(game, port)
        {
            this.font = font;
            this.texture = sprite;
            base.StateCamera.InputEnabled = false;
        }

        public override void Update(GameTime time)
        {
            mState = Mouse.GetState();
            StateManager.KState = Keyboard.GetState();

            base.Update(time);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Inventory:", new Vector2(350, 0), Color.Brown);
            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                Button button = new Button(Inventory.Items[i].Name, font, texture, new Rectangle(0, 30 * (i + 1), 800, 30), true);
            }
        }
    }
}
