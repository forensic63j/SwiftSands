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
            Item item = null;
            if (StateManager.KState.IsKeyDown(Keys.Escape) && StateManager.KPrevious.IsKeyUp(Keys.Escape))
            {
                StateManager.CloseState();
            }

            if (StateManager.KState.IsKeyDown(Keys.P) && StateManager.KPrevious.IsKeyUp(Keys.P))
            {
                StateManager.CloseState();
                StateManager.OpenState(StateGame.PartyMenu);
            }

            if (StateManager.KState.IsKeyDown(Keys.T) && StateManager.KPrevious.IsKeyUp(Keys.T))
            {
                StateManager.CloseState();
                StateManager.OpenState(StateGame.TaskMenu);
            }

            if (StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
            {
                Point p = StateManager.MState.Position;
                if (members.Count > 0)
                {
                    Point p2 = new Point(members[0].Position.X, members[0].Position.Y);
                    for (int x = 0; x < buttons.Count; x++)
                    {
                        if (buttons[x].Position.Contains(p2))
                        {
                            String s = buttons[x].Name;
                            item = Inventory.FindItem(s);
                            break;
                        }
                    }
                    for (int i = 0; i < members.Count; i++)
                    {
                        if (members[i].Position.Contains(p))
                        {
                            String s = Inventory.EquippedPlayer(item);
                            if (s != "None")
                                Party.FindPlayer(s).EquipItem = Character.DefaultItem;
                            Party.PartyList[i].EquipItem = item;
                            members.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    for (int a = 0; a < buttons.Count; a++)
                    {
                        if (buttons[a].Position.Contains(p))
                        {
                            for (int b = 0; b < Party.PartyList.Count; b++)
                            {
                                Button button = new Button(Party.PartyList[b].Name, font, texture, new Rectangle(p.X, p.Y + (30 * b), 70, 30), true);
                                members.Add(button);
                            }
                        }
                    }
                }
                
            }

            if (StateManager.MState.RightButton == ButtonState.Pressed && StateManager.MPrevious.RightButton == ButtonState.Released)
            {
                members.Clear();          
            }

            base.Update(time);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Inventory:", new Vector2(350, 0), Color.Brown);
            for (int i = 0; i < Inventory.Items.Count; i++)
            {
                Item temp = Inventory.Items[i];
                String s = Inventory.EquippedPlayer(temp);
                Button button = new Button(temp.Name, font, texture, new Rectangle(0, 30 * (i + 1), 800, 30), true);
                spriteBatch.Draw(texture, button.Position, Color.White);
                String damOrHeal = "";
                if (temp.Type == ItemType.HealingSpell)
                    damOrHeal = "Heal: " + temp.Healing;
                else if (temp.Type == ItemType.ManaRecovery)
                    damOrHeal = "Mana: " + temp.Healing;
                else if (temp.Type != ItemType.Evidence)
                    damOrHeal = "Damage: " + temp.Damage;
                spriteBatch.DrawString(font, button.Name + " " + damOrHeal + ", Range: " + temp.Range + ", Equipped: " + s, 
                    new Vector2(200.0f, (float)button.Position.Y), Color.Black);
                buttons.Add(button);
            }
            if (members.Count > 0)
            {
                for (int x = 0; x < members.Count; x++)
                {
                    spriteBatch.Draw(texture, members[x].Position, Color.White);
                    spriteBatch.DrawString(font, members[x].Name, new Vector2(members[x].Position.X, members[x].Position.Y), Color.Black);
                }
            }
        }

        /// <summary>
        /// Returns the states class name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Inventory";
        }
    }
}
