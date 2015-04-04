﻿//Clayton Scavone

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
    static class StateManager
    {
        static Dictionary<string, State> allStates;
        static public Stack<State> stateStack;

        static private MouseState mState;
        static private MouseState mPrevious;

        static private Vector2 mousePosition;

        static private KeyboardState kState;
        static private KeyboardState kPrevious;

        static public Stack<State> StateStack{
           get{return stateStack;}
        }
        static public State CurrentState{
           get{return stateStack.Peek();}
        }
        /// <summary>
        /// Gets the current mouse state.
        /// </summary>
        static public MouseState MState
        {
            get { return mState; }
        }

        static public Vector2 MousePosition
        {
            get { return mousePosition; }
            private set { mousePosition = value; }
        }

        static public Vector2 WorldMousePosition
        {
            get
            {
                Vector2 mouse = MousePosition;
                mouse = Vector2.Transform(mouse, CurrentState.StateCamera.InverseTransform);
                return mouse;
            }
        }

        /// <summary>
        /// Gets the last mouse state.
        /// </summary>
        static public MouseState MPrevious
        {
            get { return mPrevious; }
        }
        /// <summary>
        /// Gets new key state
        /// </summary>
        static public KeyboardState KState
        {
            get { return kState; }
            set { kState = value; }
        }

        /// <summary>
        /// Gets the last key state.
        /// </summary>
        static public KeyboardState KPrevious
        {
            get { return kPrevious; }
            set { kPrevious = value; }
        }

        static StateManager()
        {
            allStates = new Dictionary<string, State>();
            stateStack = new Stack<State>();
        }
        
        /// <summary>
        /// Switches state, calls exit and enter
        /// </summary>
        /// <param name="newState"></param>
        static public void SwitchState(State newState)
        {
            if (stateStack.Count >= 1)
            {
                stateStack.Peek().OnDestroy();
                stateStack.Clear();
            }
            stateStack.Push(newState);
            stateStack.Peek().OnEnter();
        }

        static public Vector2 UnConvertPosition(Vector2 position, Camera camera)
        {
            Vector2 newPosition = position;
            newPosition = Vector2.Transform(newPosition, camera.InverseTransform);
            LocalMap localmap = CurrentState as LocalMap;
            int x = (int)(Math.Floor((newPosition.X) * localmap.Map.TileWidth));
            int y = (int)(Math.Floor((newPosition.Y) * localmap.Map.TileHeight));
            return new Vector2(x, y);
        }

        /// <summary>
        /// Translates a vector's coodinates into tile coodinates.
        /// </summary>
        /// <param name="position">The vector coodinates in terms of the world.</param>
        /// <returns>The mouse coodunates in terms of the tile system.</returns>
        static public Vector2 ConvertPosition(Vector2 position, Camera camera)
        {
            Vector2 newPosition = position;
            newPosition = Vector2.Transform(newPosition, camera.InverseTransform);
            LocalMap localmap = CurrentState as LocalMap;
            int x = (int)(Math.Floor((newPosition.X) / localmap.Map.TileWidth));
            int y = (int)(Math.Floor((newPosition.Y) / localmap.Map.TileHeight));
            return new Vector2(x, y);
        }

        /// <summary>
        /// Translates a rectangle's coodinates into tile coodinates.
        /// </summary>
        /// <param name="position">The rectangle coodinates in terms of the world.</param>
        /// <returns>The mouse coodunates in terms of the tile system.</returns>
        static public Rectangle ConvertPosition(Rectangle position, Camera camera)
        {
            Vector2 newPosition = new Vector2(position.X, position.Y);
            newPosition = Vector2.Transform(new Vector2(position.X, position.Y), camera.InverseTransform);
            LocalMap localmap = CurrentState as LocalMap;
            int x = (int)(Math.Floor((newPosition.X / localmap.Map.TileWidth)));
            int y = (int)(Math.Floor((newPosition.Y / localmap.Map.TileHeight)));
            int width = localmap.Map.TileWidth;
            int height = localmap.Map.TileHeight;
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Opens new state ontop of stack without closing previous state
        /// </summary>
        /// <param name="newState"></param>
        static public void OpenState(State newState)
        {
            stateStack.Push(newState);
            stateStack.Peek().OnEnter();
        }
        /// <summary>
        /// Closes state ontop of stateStack
        /// </summary>
        static public void CloseState()
        {
            if (stateStack.Count > 1)
            {
                stateStack.Peek().OnExit();
                stateStack.Pop();                
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

		/// <summary>
		/// Updates current state.
		/// </summary>
		static public void Update(GameTime time)
		{
            mPrevious = mState;
            mState = Mouse.GetState();
            MousePosition = new Vector2(mState.X, mState.Y);
            MousePosition = Vector2.Transform(MousePosition, Matrix.Transpose(CurrentState.StateCamera.Transform));
            KState = Keyboard.GetState();
            CurrentState.StateCamera.Update();
			stateStack.Peek().Update(time);
		}

		/// <summary>
		/// Draws the current state.
		/// </summary>
		static public void Draw(GameTime time,SpriteBatch spriteBatch)
		{
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, CurrentState.StateCamera.Transform);
            stateStack.Peek().DrawWorld(time, spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            stateStack.Peek().DrawScreen(time, spriteBatch);
            spriteBatch.End();
		}
    }
}
