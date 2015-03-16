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
        public LocalMap(Game game, Viewport port) : base(game, port) { }

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
			base.Update(time);
        }

        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
            base.Draw(time, spriteBatch);
        }
    }
}
