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
        private Task selectedTask;

        public TaskMenu(SpriteFont font, Texture2D texture, Game1 game, Viewport port)
            : base(game, port)
        {
            this.font = font;
            this.texture = texture;
            tasks = new List<Button>();
            selectedTask = null;
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

            if (StateManager.MState.LeftButton == ButtonState.Pressed && StateManager.MPrevious.LeftButton == ButtonState.Released)
            {
                Point p = StateManager.MState.Position;
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].Position.Contains(p))
                    {
                        selectedTask = TaskManager.FindTask(tasks[i].Name);
                        if (selectedTask.Completed)
                        {
                            selectedTask.EndTask();
                            tasks.Remove(tasks[i]);
                            selectedTask = null;
                        }
                        break;
                    }
                    selectedTask = null;
                }
            }

            base.Update(time);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Task Menu", new Vector2(350, 0), Color.Brown);
            for (int i = 0; i < TaskManager.Count; i++)
            {
                Task t = TaskManager.Tasks[i];
                Button button = new Button(t.Description, font, texture, new Rectangle(0, 75 * (i + 1) - 30, 800, 75), true);
                Color color = Color.White;
                if (selectedTask != null && t == selectedTask)
                    color = Color.Bisque;
                spriteBatch.Draw(texture, button.Position, color);
                spriteBatch.DrawString(font, t.Description + ", Type: " + t.Type + "\nTarget: " + t.Target + ", Exp Reward: "
                    + t.ExpReward + "\nCompleted: " + t.Completed + ", Redoable: " + t.Redo,
                    new Vector2(200.0f, (float)button.Position.Y), Color.Black);
                tasks.Add(button);
            }
        }

        /// <summary>
        /// Returns the states class name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Tasks";
        }
    }
}