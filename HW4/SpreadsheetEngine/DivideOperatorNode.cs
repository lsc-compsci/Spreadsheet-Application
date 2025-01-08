// <copyright file="DivideOperatorNode.cs" company="Sher Chhi Ly">
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
    /// This is the class for the Division operator.
    /// </summary>
    internal class DivideOperatorNode : OperatorNode
    {
        public DivideOperatorNode()
            : base('/', OpPrecedence.Divide)
        {
            this.L = null;
            this.R = null;
        }
    }
}
