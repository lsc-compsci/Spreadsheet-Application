// <copyright file="SpreadsheetCell.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// We needed to find a way to bridge an abstract class with outside world without publicly declaring parent class.
    /// I would say this is an auxiliary method.
    /// </summary>
    /// <param name="nRows"> takes in updated rows. </param>
    /// <param name="nCols"> takes in updated columns. </param>
    public class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// SpreadsheetCell contructor (refactored).
        /// </summary>
        /// <param name="nRows"> # of rows.</param>
        /// <param name="nCol"> # of columns.</param>
        public SpreadsheetCell(int nRows, int nCol)
            : base(nRows, nCol)
        {
        }
    }
}