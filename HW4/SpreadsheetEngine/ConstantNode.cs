// <copyright file="ConstantNode.cs" company="Sher Chhi Ly">
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
    /// This is the child class ConstantNode that inherits from BaseNode.
    /// </summary>
    public class ConstantNode : BaseNode
    {
        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public double Value { get; set; }
    }
}