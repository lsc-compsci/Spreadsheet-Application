// <copyright file="HW8Tests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW8Tests
{
    /// <summary>
    /// To test features in HW8.
    /// </summary>
    public class HW8Tests
    {
        /// <summary>
        /// Usually empty.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Normal test case.
        /// The undoing and redoing of color changes for a group of cells.
        /// </summary>
        [Test]
        public void NormalTest1()
        {
            // selecting 3 cells
            var cell1 = new SpreadsheetCell(0, 0); // A1
            var cell2 = new SpreadsheetCell(0, 1); // B1
            var cell3 = new SpreadsheetCell(0, 2); // C1

            uint oldColor = 0xFFFFFFFF; // white; default color
            uint newColor = 0xFF0000FF; // blue

            var cells = new List<Cell> { cell1, cell2, cell3 };

            var command = new ColorChangeCommand(cells, newColor);

            command.Execute();
            Assert.That(cell1.BGColor, Is.EqualTo(newColor));
            Assert.That(cell2.BGColor, Is.EqualTo(newColor));
            Assert.That(cell3.BGColor, Is.EqualTo(newColor)); // should now be blue

            // undo
            command.UnExecute();
            Assert.That(cell1.BGColor, Is.EqualTo(oldColor));
            Assert.That(cell2.BGColor, Is.EqualTo(oldColor));
            Assert.That(cell3.BGColor, Is.EqualTo(oldColor)); // should revert to white

            // redo
            command.Execute();
            Assert.That(cell1.BGColor, Is.EqualTo(newColor));
            Assert.That(cell2.BGColor, Is.EqualTo(newColor));
            Assert.That(cell3.BGColor, Is.EqualTo(newColor)); // should be blue again
        }

        /// <summary>
        /// Normal test case.
        /// The undoing and redoing of text changes.
        /// </summary>
        [Test]
        public void NormalTest2()
        {
            var cell = new SpreadsheetCell(0, 0); // A1
            string oldString = "old";
            string newString = "new";
            var command = new TextChangeCommand(cell, oldString, newString);

            command.Execute();
            Assert.That(cell.TextInCell, Is.EqualTo(newString)); // new

            command.UnExecute();
            Assert.That(cell.TextInCell, Is.EqualTo(oldString)); // old

            command.Execute();
            Assert.That(cell.TextInCell, Is.EqualTo(newString)); // new
        }

        /// <summary>
        /// Boundary test case.
        /// Checks if undo stack is false when the stack is empty.
        /// </summary>
        [Test]
        public void EdgeTest()
        {
            var test2 = new Spreadsheet(5, 5);
            Assert.DoesNotThrow(() => test2.Undo()); // undoing on an empty stack should not throw an exception.
            Assert.IsFalse(test2.IsUnDoable()); // should be false when the undo stack is empty.
        }

        /// <summary>
        /// Exceptional test case.
        /// Color change but null is passed in as the call parameter.
        /// </summary>
        [Test]
        public void ExceptionTest()
        {
            // default color is white
            uint newColor = 0xFF0000FF; // blue
            Assert.Throws<ArgumentNullException>(() => new ColorChangeCommand(null, newColor));
        }
    }
}