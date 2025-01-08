// <copyright file="MultiplyOperatorNode.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This is the class for the Multiplication operator.
    /// </summary>
    internal class MultiplyOperatorNode : OperatorNode
    {
        public MultiplyOperatorNode()
            : base('*', OpPrecedence.Multiply)
        {
            this.L = null;
            this.R = null;
        }
    }
}
