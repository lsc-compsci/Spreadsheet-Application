// <copyright file="ColorChangeCommand.cs" company="Sher Chhi Ly">
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
    /// Our class for background color change commands.
    /// Feature added to handle groups of cells in logic layer.
    /// </summary>
    public class ColorChangeCommand : ICommands
    {
        /// <summary>
        /// So we know where color change took place.
        /// List of cells.
        /// </summary>
        private List<Cell> cells;

        /// <summary>
        /// Collection of old background color associated with each cell.
        /// </summary>
        private Dictionary<Cell, uint> oldBGs;

        /// <summary>
        /// The new background color.
        /// </summary>
        private uint newBG;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangeCommand"/> class.
        /// The constructor for our color change command.
        /// After adding the fields and I pretty much hit 'tab' and this was auto-generated.
        /// Pretty neat.
        /// </summary>
        /// <param name="cells"> cell.</param>
        /// <param name="newBG"> new background color.</param>
        public ColorChangeCommand(List<Cell> cells, uint newBG)
        {
            if (cells == null || cells.Count == 0)
            {
                throw new ArgumentNullException();
            }

            this.cells = cells;
            this.oldBGs = cells.ToDictionary(cell => cell, cell => cell.BGColor); // storing cell's with its og color
            this.newBG = newBG;
        }

        /// <summary>
        /// interface method re-implemented.
        /// REDO.
        /// </summary>
        public void Execute()
        {
            foreach (var cell in this.cells)
            {
                cell.BGColor = this.newBG;
            }
        }

        /// <summary>
        /// interface method re-implemented.
        /// UNDO.
        /// </summary>
        public void UnExecute()
        {
            foreach (var cell in this.cells)
            {
                cell.BGColor = this.oldBGs[cell];
            }
        }
    }
}
