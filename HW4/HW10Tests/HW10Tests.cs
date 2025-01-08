// <copyright file="HW10Tests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW10Tests
{
    /// <summary>
    /// This is a class to test the features for HW10.
    /// </summary>
    public class HW10Tests
    {
        /// <summary>
        /// So that we can instantiate a dummy spreadsheet.
        /// </summary>
        private Spreadsheet testSheet;

        /// <summary>
        /// Usually empty.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.testSheet = new Spreadsheet(5, 5);
        }

        /// <summary>
        /// Our exception case to test for a bad reference.
        /// Testing for invalid references and how to handle them.
        /// </summary>
        [Test]
        public void TestBadReference()
        {
            var a1 = this.testSheet.GetCell(0, 0);
            var b1 = this.testSheet.GetCell(0, 1);
            this.HelpTestSetCellText(b1, "10");
            this.HelpTestSetCellText(a1, "=(B1+10)+Cell*27");

            Assert.That(a1.ValueInCell.ToString(), Is.EqualTo("ERROR: Invalid Variable(s) - ExtractCellVariables()")); // !(badreference) caught
        }

        /// <summary>
        /// Our exception case to test for a self reference.
        /// Testing for self references and how to handle them.
        /// </summary>
        [Test]
        public void TestSelfReference()
        {
            var a1 = this.testSheet.GetCell(0, 0);
            this.HelpTestSetCellText(a1, "=A1+10");

            Assert.That(a1.ValueInCell.ToString(), Is.EqualTo("ERROR: Self-reference detected - A1 is referencing itself - UpdateCell()")); // !(selfreference) caught
        }

        /// <summary>
        /// Our exception case to test for a circular reference.
        /// Test for circular references and how to handle them.
        /// </summary>
        [Test]
        public void TestCircularReference()
        {
            var a1 = this.testSheet.GetCell(0, 0);
            var b1 = this.testSheet.GetCell(0, 1);
            var c1 = this.testSheet.GetCell(0, 2);
            var d1 = this.testSheet.GetCell(0, 3);
            this.HelpTestSetCellText(a1, "=B1"); // A1 reference to B1
            this.HelpTestSetCellText(b1, "=C1"); // B1 reference to C1
            this.HelpTestSetCellText(c1, "=D1"); // C1 reference to D1
            this.HelpTestSetCellText(d1, "=A1"); // D1 reference to A1

            Assert.That(d1.ValueInCell.ToString(), Is.EqualTo("ERROR: Circular reference detected - UpdateCell()")); // !(circularreference) caught
        }

        /// <summary>
        /// Our edge case to test a formula with an undefined variable.
        /// Empty cell in a formula can be considered to be a boundary test case.
        /// </summary>
        [Test]
        public void EdgeCase()
        {
            var a1 = this.testSheet.GetCell(0, 0);
            this.HelpTestSetCellText(a1, "=B1+100"); // B1 is a valid cell but is undefined (value = 0)

            Assert.That(a1.ValueInCell.ToString(), Is.EqualTo("100"));
        }

        /// <summary>
        /// Our normal case to test for dependency update propagation.
        /// </summary>
        [Test]
        public void NormalCase()
        {
            var a1 = this.testSheet.GetCell(0, 0);
            var b1 = this.testSheet.GetCell(0, 1);
            var c1 = this.testSheet.GetCell(0, 2);
            this.HelpTestSetCellText(a1, "=B1"); // A1 reference to B1
            this.HelpTestSetCellText(b1, "=C1"); // B1 reference to C1
            this.HelpTestSetCellText(c1, "=500"); // C1 is 500

            Assert.That(c1.ValueInCell.ToString(), Is.EqualTo("500"));
            Assert.That(a1.ValueInCell.ToString(), Is.EqualTo("500"));
            Assert.That(b1.ValueInCell.ToString(), Is.EqualTo("500"));
        }

        /// <summary>
        /// Test aux method. STOLEN FROM MY HW7TESTS
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