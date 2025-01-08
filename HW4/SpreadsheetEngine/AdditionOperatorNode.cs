// <copyright file="AdditionOperatorNode.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This is the class for the Addition operator.
    /// </summary>
    internal class AdditionOperatorNode : OperatorNode
    {
        public AdditionOperatorNode()
            : base('+', OpPrecedence.Add)
        {
            this.L = null;
            this.R = null;
        }
    }
}
