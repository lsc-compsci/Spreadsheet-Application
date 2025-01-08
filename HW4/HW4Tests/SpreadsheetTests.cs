// <copyright file="SpreadsheetTests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW4Tests
{
    /// <summary>
    /// Class for the tests.
    /// </summary>
    public class SpreadsheetTests
    {
        /// <summary>
        /// Creating an instance of the spreadsheet class.
        /// </summary>
        private Spreadsheet test;

        /// <summary>
        /// Setup initializes a 9x9 spreadsheet (supposedly, we won't know until we test for it).
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.test = new Spreadsheet(9, 9);
        }

        /// <summary>
        /// Tests for proper initialization of the spreadsheet.
        /// </summary>
        [Test]
        public void TestSpreadsheetInit()
        {
            var testRow = this.test.RowCount;
            var testCol = this.test.ColumnCount;

            Assert.That(testRow, Is.EqualTo(9));
            Assert.That(testCol, Is.EqualTo(9));
        }

        /// <summary>
        /// Tests for valid text being properly set in the cell.
        /// </summary>
        [Test]
        public void TestValidTextInCell()
        {
            this.test.GetCell(5, 5).TextInCell = "Test passed.";

            var testText = this.test.GetCell(5, 5).TextInCell;

            Assert.That(testText, Is.EqualTo("Test passed."));
        }

        /// <summary>
        /// Tests for valid value being properly set in the cell.
        /// Remark: Value setter is protected, this is also testing that accessor are working accordingly.
        /// </summary>
        [Test]
        public void TestInalidValueInCell()
        {
            this.test.SetValue(this.test.GetCell(5, 5), "Test passed");

            var testValue = this.test.GetCell(5, 5).ValueInCell;

            Assert.That(testValue, Is.EqualTo("Test passed"));
        }
    }
}