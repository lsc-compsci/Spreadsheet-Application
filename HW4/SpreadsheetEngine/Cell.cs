// <copyright file="Cell.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// An abstract base class.
    /// It represents one cell in the worksheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged // INotifyPropertyChanged interface implemented
    {
        /// <summary>
        /// Event field.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// used in "Text" property.
        /// </summary>
        private string textInCell = string.Empty;

        /// <summary>
        /// used in "Value" property.
        /// </summary>
        private string valueInCell = string.Empty;

        /// <summary>
        /// int value row index.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// int value column index.
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Default cell background color- white.
        /// This is given.
        /// </summary>
        private uint bgColor = 0xFFFFFFFF;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Set in the constructor but returned through the get.
        /// </summary>
        /// <param name="rowIndex"> row index. </param>
        /// <param name="columnIndex"> column index. </param>
        public Cell(int rowIndex, int columnIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        /// <summary>
        /// Gets list of Cells that are referencing other cells.
        /// </summary>
        public List<Cell> Dependents { get; private set; } = new List<Cell>();

        /// <summary>
        /// Gets list of Cells that are being referenced (needed to check for circular reference).
        /// </summary>
        public List<Cell> ReferencedCells { get; private set; } = new List<Cell>();

        /// <summary>
        /// Gets index for row, read-only propery.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets index for column, read-only property.
        /// </summary>
        public int ColumnIndex // read-only property
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets represents the actual text that’s typed into the cell.
        /// Reference used: lecture w5.2 p25.
        /// </summary>
        public string TextInCell // "Text" Property
        {
            get
            {
                return this.textInCell; // returns the protected field
            }

            set
            {
                if (this.textInCell == value) // ignore exact same string
                {
                    return;
                }
                else
                {
                    this.textInCell = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("TextInCell"));
                }
            }
        }

        /// <summary>
        /// Gets or sets represents the evaluated value of the cell. Contains the evaluation of the formula that is typed into the cell.
        /// It will just be the Text propertyif the text doesn't start with the '=' character.
        /// </summary>
        public string ValueInCell // "Value" Property
        {
            get
            {
                return this.valueInCell;
            }

            protected internal set
            {
                if (this.valueInCell == value)
                {
                    return;
                }
                else
                {
                    this.valueInCell = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("ValueInCell"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a uint property for the background color of a cell.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor == value)
                {
                    return;
                }
                else
                {
                    this.bgColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("ColorInCell"));
                }
            }
        }

        /// <summary>
        /// Clears dependencies for the current cell.
        /// When a formula changes, its dependencies may change as well.
        /// </summary>
        public void ClearDependencies()
        {
            foreach (var dependentCell in this.Dependents)
            {
                dependentCell.Dependents.Remove(this);
            }

            this.ReferencedCells.Clear();
        }

        /// <summary>
        /// Adds dependent cells to a dependency list.
        /// Makes it possible to track cell depency and propagate necessary changes.
        /// </summary>
        /// <param name="cellThatDepends"> Cell class type.</param>
        public void AddDependency(Cell cellThatDepends)
        {
            if (!this.Dependents.Contains(cellThatDepends))
            {
                this.Dependents.Add(cellThatDepends);
            }
        }

        /// <summary>
        /// Adds cells that are being referenced to a referenced cells list.
        /// Referenced cells would have record which cells depend on it.
        /// </summary>
        /// <param name="referencedCell"> Cell class type.</param>
        public void AddReferenceCell(Cell referencedCell)
        {
            if (!this.ReferencedCells.Contains(referencedCell))
            {
                this.ReferencedCells.Add(referencedCell);
                referencedCell.AddDependency(this); // ensures that referencedCell is aware of current cell as dependent.
            }
        }
    }
}
