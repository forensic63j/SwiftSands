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
    class TaskMenu : State
    {
        private SpriteFont font;
        private Texture2D texture;
        private List<Button> tasks;

        public TaskMenu(SpriteFont font, Texture2D texture, Game1 game, Viewport port)
            : base(game, port)
        {
            this.font = font;
            this.texture = texture;
            tasks = new List<Button>();
        }

        public override void Update(GameTime time)
        {
            if (StateManager.KState.IsKeyDown(Keys.Escape) && StateManager.KPrevious.IsKeyUp(Keys.Escape))
            {
                StateManager.CloseState();
            }

            if (StateManager.KState.IsKeyDown(Keys.P) && StateManager.KPrevious.IsKeyUp(Keys.P))
            {
                StateManager.CloseState();
                StateManager.OpenState(StateGame.PartyMenu);
            }

            if (StateManager.KState.IsKeyDown(Keys.I) && StateManager.KPrevious.IsKeyUp(Keys.I))
            {
                StateManager.CloseState();
                StateManager.OpenState(StateGame.InventoryMenu);
            }

            base.Update(time);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Task Menu", new Vector2(350, 0), Color.Brown);
            for (int i = 0; i < TaskManager.Count; i++)
            {
                Button button = new Button(TaskManager.Tasks[i].Description, font, texture, new Rectangle(0, 30 * (i + 1), 800, 30), true);
                spriteBatch.Draw(texture, button.Position, Color.White);
                spriteBatch.DrawString(font, button.Name + ", Damage: " + Inventory.Items[i].Damage + ", Range: " + Inventory.Items[i].Range,
                    new Vector2(200.0f, (float)button.Position.Y), Color.Black);
                tasks.Add(button);
            }
        }
    }
}
