// <copyright file="ICommands.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This is a commands interface that will be inherit.
    /// </summary>
    public interface ICommands
    {
        /// <summary>
        /// For Redo.
        /// </summary>
        void Execute();

        /// <summary>
        /// For Undo.
        /// </summary>
        void UnExecute();
    }
}
