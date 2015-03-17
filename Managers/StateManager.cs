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
    static class StateManager
    {
        static Dictionary<string, State> allStates;
        static public Stack<State> stateStack;

        static Texture2D buttonSprite;

        static private MouseState mState;
        static private MouseState mPrevious;

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

        /// <summary>
        /// Gets the last mouse state.
        /// </summary>
        static public MouseState MPrevious
        {
            get { return mPrevious; }
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
