//Clayton Scavone and Brian Sandon

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
    class OptionsMenu : State
	{
		#region fields
		private Button volume;
		private Button resolution;
		private Button back;
        private SpriteFont font;

		private MouseState mState;
		private MouseState mPrevious;
		#endregion

		#region properties
		/// <summary>
		/// Gets the resume button.
		/// </summary>
		public Button Volume
		{
			get { return volume; }
		}

		/// <summary>
		/// Gets the save button.
		/// </summary>
		public Button Resolution
		{
			get { return resolution; }
		}

		/// <summary>
		/// Gets the options button.
		/// </summary>
		public Button Back
		{
			get { return back; }
		}
		#endregion

		/// <summary>
		/// Instatiates the Options Menu
		/// </summary>
		public OptionsMenu(SpriteFont font,Texture2D sprite, Game game,Viewport port)
			: base(game,port) 
		{
			this.font = font;

			int buttonWidth = 120;
			int centering = (port.Width - buttonWidth) / 2;
			volume = new Button("Volume",font,sprite,new Rectangle(centering,20,buttonWidth,30),true,true);
			resolution = new Button("Resolution",font,sprite,new Rectangle(centering,60,buttonWidth,30),true,true);
			back = new Button("Back",font,sprite,new Rectangle(centering,100,buttonWidth,30),true,true);
			
		}

		#region methods
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
			mPrevious = mState;
			mState = Mouse.GetState();

			volume.Update(mState,mPrevious);
			resolution.Update(mState,mPrevious);
			back.Update(mState,mPrevious);

			base.Update(time);
        }

        public override void Draw(GameTime time, SpriteBatch spriteBatch)
        {
			spriteBatch.DrawString(font,"Options:",new Vector2(350,0),Color.Brown);
			
			volume.Draw(spriteBatch);
			resolution.Draw(spriteBatch);
			back.Draw(spriteBatch);
			
			base.Draw(time, spriteBatch);
		}
		#endregion
	}
}
