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
        private List<Button> buttons;
        private List<Button> members;

        public InventoryMenu(SpriteFont font, Texture2D sprite, Game1 game, Viewport port)
            : base(game, port)
        {
            this.font = font;
            this.texture = sprite;
            base.StateCamera.InputEnabled = false;
            buttons = new List<Button>();
            members = new List<Button>();
        }

        public override void Update(GameTime time)
        {
            mState = Mouse.GetState();
            StateManager.KState = Keyboard.GetState();

            if (mState.LeftButton == ButtonState.Pressed)
            {
                Point p = mState.Position;
                if (members.Count == 0)
                {
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        Button button = buttons[i];
                        if (button.Position.Contains(p))
                        {
                            for (int j = 0; j < Party.Count; j++)
                            {
                                Button b = new Button(Party.PartyList[j].Name, font, texture, new Rectangle(p.X + (50 * j), p.Y + (30 * j), 50, 30), true);
                                members.Add(b);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < members.Count; i++)
                    {
                        Button button = members[i];
                        if (button.Position.Contains(p))
                        {
                            Point p1 = new Point(members[0].Position.X, members[0].Position.Y);
                            for (int j = 0; j < buttons.Count; j++)
                            {
                                if (buttons[j].Position.Contains(p1))
                                {
                                    Party.PartyList[i].EquipItem = Inventory.Items[j];
                                    break;
                                }
                            }
                            members.Clear();
                            break;
                        }
                    }
                }
            }

            base.Update(time);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Inventory:", new Vector2(350, 0), Color.Brown);
            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                Button button = new Button(Inventory.Items[i].Name, font, texture, new Rectangle(0, 30 * (i + 1), 800, 30), true);
                buttons.Add(button);
            }
        }
    }
}
