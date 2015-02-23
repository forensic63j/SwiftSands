﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SwiftSands.Managers
{
    class LoadManager
    {
        #region fields
        DirectoryInfo saveInfo;
        #endregion

        #region method
		/// <summary>
		/// Creates a load manager.
		/// </summary>
        public LoadManager()
        {
            saveInfo = Directory.CreateDirectory("Data//SaveFiles"); 
        }

        /// <summary>
        /// Loads content.
        /// </summary>
        public void LoadContent()
        {

        }

        /// <summary>
        /// Loads a savefile.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        public void LoadSavefile(String filename)
        {
            try
            {
				using(Stream inStream = File.OpenRead(filename))
				{
					using(BinaryReader input = new BinaryReader(inStream))
					{

					}
				}
			} catch(Exception e)
			{

			}
        }
        #endregion
    }
}
