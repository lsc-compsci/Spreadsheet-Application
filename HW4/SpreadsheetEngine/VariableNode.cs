// <copyright file="VariableNode.cs" company="Sher Chhi Ly">
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
    /// This is the child class VariableNode inheriting from BaseNode.
    /// </summary>
    public class VariableNode : BaseNode
    {
        /// <summary>
        /// Gets or sets getter and setter for Name.
        /// </summary>
        public string? Name { get; set; }
    }
}
