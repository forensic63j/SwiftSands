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
        Matrix inverseTransform;
        Vector2 position;
        float rotation;
        Viewport viewport;
        KeyboardState keyState;
        float cameraSpeed = 4f;
        float rightCameraBound;
        float leftCameraBound;
        float topCameraBound;
        float bottomCameraBound;

        bool inputEnabled;

        public float RightCameraBound
        {
            get { return rightCameraBound; }
            set { rightCameraBound = value; }
        }

        public float BottomCameraBound
        {
            get { return bottomCameraBound; }
            set { bottomCameraBound = value; }
        }

        public float TopCameraBound
        {
            get { return topCameraBound; }
            set { topCameraBound = value; }
        }
        public float LeftCameraBound
        {
            get { return leftCameraBound; }
            set { leftCameraBound = value; }
        }


		/// <summary>
		/// Gets the position.
		/// </summary>
		public Vector2 Position
		{
			get { return position; }
            set { position = value; }
		}

		public Matrix Transform{
            get{ return transform; }
        }

        public Matrix InverseTransform
        {
            get { return Matrix.Invert(transform); }
        }

        public bool InputEnabled
        {
            get { return inputEnabled; }
            set { inputEnabled = value; }
        }
        public void Input()
        {
            keyState = StateManager.KState;
            if (keyState.IsKeyDown(Keys.A))
            {
                if (position.X >= LeftCameraBound)
                {
                    position.X += cameraSpeed;
                }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                if (position.X <= RightCameraBound)
                {
                    position.X -= cameraSpeed;
                }
            } 
            if (keyState.IsKeyDown(Keys.S))
            {
                if (position.Y <= BottomCameraBound)
                {
                    position.Y -= cameraSpeed;
                }
            } 
            if (keyState.IsKeyDown(Keys.W))
            {
                if (position.Y >= TopCameraBound)
                {
                    position.Y += cameraSpeed;
                }
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
