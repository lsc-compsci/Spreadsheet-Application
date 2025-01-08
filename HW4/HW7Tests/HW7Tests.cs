// <copyright file="HW7Tests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using SpreadsheetEngine;

namespace HW7Tests
{
    /// <summary>
    /// Dummy form1 to test spreadsheet and cell behavior (post hw5 and hw6).
    /// </summary>
    [TestFixture]
    public class HW7Tests
    {
        /// <summary>
        /// spreadsheet to test the operations.
        /// </summary>
        private Spreadsheet testSheet;

        /// <summary>
        /// Set up usually empty. Created a small testing spreadsheet.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.testSheet = new Spreadsheet(5, 5);
        }

        /// <summary>
        /// Multiple cell value assignment and formula evaluation.
        /// </summary>
        [Test]
        public void NormalTestCase1()
        {
            var cellA1 = this.testSheet.GetCell(0, 0); // A1
            this.HelpTestSetCellText(cellA1, "=50"); // assigning A1 =50;

            var cellB1 = this.testSheet.GetCell(0, 1); // B1
            this.HelpTestSetCellText(cellB1, "=10"); // assigning B1 =10;

            var cellC1 = this.testSheet.GetCell(0, 2); // C1
            this.HelpTestSetCellText(cellC1, "=((A1+B1)*2)/5"); // assigning C1 with a formula to be evaluated

            Assert.That(cellC1.ValueInCell.ToString(), Is.EqualTo("24"));
        }

        /// <summary>
        /// Assigning formula to a cell and evaluating it.
        /// </summary>
        [Test]
        public void NormalTestCase2()
        {
            var cellE5 = this.testSheet.GetCell(4, 4); // E5
            this.HelpTestSetCellText(cellE5, "=(((5/2)*2)-1)"); // assigning E5 a formula

            Assert.That(cellE5.ValueInCell.ToString(), Is.EqualTo("4"));
        }

        /// <summary>
        /// Basic constant assignment to a cell.
        /// </summary>
        [Test]
        public void EdgeTestCase1()
        {
            var cellA1 = this.testSheet.GetCell(0, 0); // A1
            this.HelpTestSetCellText(cellA1, "=150"); // assigning A1 to a constant

            Assert.That(cellA1.ValueInCell.ToString(), Is.EqualTo("150"));
        }

        /// <summary>
        /// Variable assigned to a defined variable.
        /// </summary>
        [Test]
        public void EdgeTestCase2()
        {
            var cellC4 = this.testSheet.GetCell(3, 2); // C4
            this.HelpTestSetCellText(cellC4, "=10"); // assigning C4 to a constant

            var cellC3 = this.testSheet.GetCell(2, 2); // C3
            this.HelpTestSetCellText(cellC3, "=C4"); // assigning C3 to C4

            Assert.That(cellC3.ValueInCell.ToString(), Is.EqualTo("10"));
        }

        /// <summary>
        /// VARIABLE UNDEFINED EXCEPTION.
        /// </summary>
        [Test]
        public void ExceptionalTestCase1()
        {
            var cellD2 = this.testSheet.GetCell(1, 3); // D2;
            this.HelpTestSetCellText(cellD2, "=A1"); // A1 is undefined

            Assert.That(cellD2.ValueInCell.ToString(), Is.EqualTo("ERROR: A1 is not defined - GetCellRefValue()"));
        }

        /// <summary>
        /// CONSECUTIVE OPERATOR EXCEPTION.
        /// </summary>
        [Test]
        public void ExceptionalTestCase2()
        {
            var cellD2 = this.testSheet.GetCell(1, 3); // D2;
            this.HelpTestSetCellText(cellD2, "=5+*5"); // what is '+*'

            Assert.That(cellD2.ValueInCell.ToString(), Is.EqualTo("ERROR: Consecutive Operation Symbols - Compile()"));
        }

        /// <summary>
        /// INVALID FORMULA EXCEPTION.
        /// </summary>
        [Test]
        public void ExceptionalTestCase3()
        {
            var cellD2 = this.testSheet.GetCell(1, 3); // D2;
            this.HelpTestSetCellText(cellD2, "=HELLOWORLD"); // is an expression but not a formula

            Assert.That(cellD2.ValueInCell.ToString(), Is.EqualTo("ERROR: Invalid Variable - ExtractCellRef()"));
        }

        /// <summary>
        /// Test aux method.
        /// A way to bypass and set the text in the cell without pain.
        /// </summary>
        /// <param name="cell"> Cell class type.</param>
        /// <param name="text"> text to be evaluated.</param>
        private void HelpTestSetCellText(Cell cell, string text)
        {
            cell.TextInCell = text;
            this.testSheet.UpdateCell(cell, new System.ComponentModel.PropertyChangedEventArgs("TextInCell"));
        }
    }
}