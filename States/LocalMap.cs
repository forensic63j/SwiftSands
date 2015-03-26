//Clayton Scavone

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
  
    class LocalMap : State
    {
        Map map;
        MouseState mState;
        Texture2D buttonSprite;

        Vector2 mouse;

        KeyboardState oldState;
        public LocalMap(Game1 game, Viewport port) : base(game, port) { }

        public override void OnEnter()
        {
            map = LoadManager.LoadMap("desert.txt");
            buttonSprite = LoadManager.LoadSprite("GUI\\button-sprite.png");
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnDestroy()
        {
            base.OnExit();
        }

        public override void Update(GameTime time)
        {
            StateManager.KState = Keyboard.GetState();
            if (StateManager.KState.IsKeyDown(Keys.Escape))
            {
                if (!StateManager.KPrevious.IsKeyDown(Keys.Escape))
                {
                    StateManager.OpenState(StateGame.Pause);
                }
            }
            mState = Mouse.GetState();
            mouse = new Vector2(mState.X, mState.Y);
            mouse = Vector2.Transform(mouse, Matrix.Transpose(StateCamera.Transform));
            base.Update(time);
        }

        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
            map.Draw(time, spriteBatch);
            base.DrawWorld(time, spriteBatch);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(buttonSprite, new Rectangle((int)mouse.X, (int)mouse.Y, 5, 5), Color.Black);
            base.DrawScreen(time, spriteBatch);
        }
    }
}
