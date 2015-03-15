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
			stateStack.Peek().Update(time);
		}

		/// <summary>
		/// Draws the current state.
		/// </summary>
		static public void Draw(GameTime time,SpriteBatch spriteBatch)
		{
			stateStack.Peek().Draw(time,spriteBatch);
		}
    }
}
