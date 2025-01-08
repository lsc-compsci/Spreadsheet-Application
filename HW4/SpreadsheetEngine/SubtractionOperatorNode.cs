﻿// <copyright file="SubtractionOperatorNode.cs" company="Sher Chhi Ly">
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
    /// This is the class for the Subtraction operator.
    /// </summary>
    internal class SubtractionOperatorNode : OperatorNode
    {
        public SubtractionOperatorNode()
            : base('-', OpPrecedence.Subtract)
        {
            this.L = null;
            this.R = null;
        }
    }
}
