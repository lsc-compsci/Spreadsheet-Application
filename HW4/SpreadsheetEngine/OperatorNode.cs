// <copyright file="OperatorNode.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This is the child class OperatorNode inheriting for BaseNode.
    /// </summary>
    public abstract class OperatorNode : BaseNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// This is the default constructor for OperatorNode.
        /// </summary>
        /// <param name="c">char symbol parameter.</param>
        /// <param name="precedence"> OpPrecedence parameter.</param>
        public OperatorNode(char c, OpPrecedence precedence)
        {
            this.Operator = c;
            this.OperatorPrecedence = precedence;
            this.L = null;
            this.R = null;
        }

        /// <summary>
        /// Defines the precedence of each operation. 1 = low precedence, 2 = high precedence.
        /// </summary>
        public enum OpPrecedence // following BIDMAS or PEMDAS
        {
            Add = 1,
            Subtract = 1,
            Multiply = 2,
            Divide = 2,
        }

        /// <summary>
        /// Gets or sets Operator.
        /// </summary>
        public char Operator { get; set; }

        /// <summary>
        /// Gets or sets Operator Precedence.
        /// </summary>
        public OpPrecedence OperatorPrecedence { get; set; }

        /// <summary>
        /// Gets or sets Left node.
        /// </summary>
        public BaseNode? L { get; set; }

        /// <summary>
        /// Gets or sets Right node.
        /// </summary>
        public BaseNode? R { get; set; }
    }
}
