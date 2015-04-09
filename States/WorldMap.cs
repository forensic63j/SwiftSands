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
    class WorldMap : State
    {
        Map map;
         public WorldMap(Game1 game, Viewport port) : base(game, port) { }

         public Map Map
         {
             get { return map; }
         }

        public override void OnEnter()
        {
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
			if(Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				StateManager.OpenState(StateGame.Pause);
			}
			base.Update(time);
        }

        public override void DrawWorld(GameTime time, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here          
            base.Draw(time, spriteBatch);
        }

        public override void DrawScreen(GameTime time, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here          
            base.Draw(time, spriteBatch);
        }
    }
}
