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
    class Camera
    {
        float zoom;
        Matrix transform;
        Vector2 position;
        float rotation;
        Viewport viewport;
        KeyboardState keyState;

        bool inputEnabled;

		/// <summary>
		/// Gets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return position; }
		}

		public Matrix Transform{
            get{ return transform; }
        }

        public bool InputEnabled
        {
            get { return inputEnabled; }
            set { inputEnabled = value; }
        }
        public void Input()
        {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
            {
                position.X += 2F;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                position.X -= 2F;
            } 
            if (keyState.IsKeyDown(Keys.S))
            {
                position.Y -= 2F;
            } 
            if (keyState.IsKeyDown(Keys.W))
            {
                position.Y += 2F;
            }
        }

        public void Update()
        {
            if (inputEnabled)
            {
                Input();
            }
            MathHelper.Clamp(zoom, 0.01f, 10.0f);
            rotation = MathHelper.WrapAngle(rotation);
            transform = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(position.X, position.Y, 0);        
        }

        public Camera(Viewport viewport)
        {
            inputEnabled = true;
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
            this.viewport = viewport;
        }

    }
}
