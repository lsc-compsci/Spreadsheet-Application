// <copyright file="UndefinedVariableException.cs" company="Sher Chhi Ly">
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
    /// A class for a custom-exception.
    /// </summary>
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UndefinedVariableException"/> class.
        /// A constructor for the exception.
        /// </summary>
        /// <param name="message"> exception message.</param>
        public UndefinedVariableException(string message)
            : base(message)
        {
        }
    }
}
