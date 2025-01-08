// <copyright file="TextChangeCommand.cs" company="Sher Chhi Ly">
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
    /// Our class for text change commands.
    /// </summary>
    public class TextChangeCommand : ICommands
    {
        /// <summary>
        /// Where the change is located.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// What is the old text.
        /// </summary>
        private string oldText;

        /// <summary>
        /// What is the new text.
        /// </summary>
        private string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangeCommand"/> class.
        /// The constructor for our text change command.
        /// After adding the fields and I pretty much hit 'tab' and this was auto-generated.
        /// Pretty neat.
        /// </summary>
        /// <param name="cell"> cell.</param>
        /// <param name="oldText"> old text.</param>
        /// <param name="newText"> new text.</param>
        public TextChangeCommand(Cell cell, string oldText, string newText)
        {
            if (cell == null)
            {
                throw new ArgumentNullException();
            }

            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }

        /// <summary>
        /// interface method re-implemented.
        /// REDO.
        /// </summary>
        public void Execute()
        {
            this.cell.TextInCell = this.newText;
            if (!this.newText.StartsWith("="))
            {
                this.cell.ClearDependencies();
            }
        }

        /// <summary>
        /// interface method re-implemented.
        /// UNDO.
        /// </summary>
        public void UnExecute()
        {
            this.cell.TextInCell = this.oldText;
            if (!this.newText.StartsWith("="))
            {
                this.cell.ClearDependencies();
            }
        }
    }
}
