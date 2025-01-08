// <copyright file="HW9Tests.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using SpreadsheetEngine;

namespace HW9Tests
{
    /// <summary>
    /// This is a class to test methods for HW9.
    /// </summary>
    public class HW9Tests
    {
        /// <summary>
        /// dummy sheet for our tests.
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
        /// Normal test case 1
        /// Saving a file
        /// (file could have formulas, text, and highlights).
        /// </summary>
        [Test]
        public void NormalTest1()
        {
            var testCell1 = this.testSheet.GetCell(0, 0); // A1
            testCell1.TextInCell = "Hello";
            testCell1.BGColor = 0xFFFF8080;
            var testCell2 = this.testSheet.GetCell(0, 1); // A2
            testCell2.TextInCell = "World";
            testCell2.BGColor = 0xFFFF8080;

            string file = "NormalTest1.xml";

            try
            {
                using (FileStream input = new FileStream(file, FileMode.Create))
                {
                    this.testSheet.SaveSpreadsheetFile(input);
                }
            }
            finally
            {
                if (File.Exists(file))
                {
                    Assert.Pass(file); // cool its been saved
                }
                else
                {
                    Assert.Fail("File was not saved properly");
                }

                File.Delete(file); // clean up
            }
        }

        /// <summary>
        /// Normal test case 2
        /// Loading a file
        /// (file could have formulas, text, and highlights).
        /// </summary>
        [Test]
        public void NormalTest2()
        {
            // a dummy file needs to exist first
            var testCell1 = this.testSheet.GetCell(0, 0); // A1
            testCell1.BGColor = 0xFFFF8080;
            this.HelpTestSetCellText(testCell1, "=5*5");
            var testCell2 = this.testSheet.GetCell(0, 1); // A2
            this.HelpTestSetCellText(testCell2, "=A1");

            string file = "NormalTest2.xml";

            try
            {
                using (FileStream input = new FileStream(file, FileMode.Create))
                {
                    this.testSheet.SaveSpreadsheetFile(input);
                }

                var anotherSheet = new Spreadsheet(5, 5);
                using (FileStream output = new FileStream(file, FileMode.Open))
                {
                    anotherSheet.LoadSpreadsheetFile(output);
                }

                Assert.That(anotherSheet.GetCell(0, 0).ValueInCell.ToString(), Is.EqualTo("25"));
                Assert.That(anotherSheet.GetCell(0, 1).ValueInCell.ToString(), Is.EqualTo("25"));
            }
            finally
            {
                if (File.Exists(file))
                {
                    Assert.Pass(file);
                    File.Delete(file); // clean up
                }
            }
        }

        /// <summary>
        /// Boundary test case
        /// Saving and Loading an empty file.
        /// </summary>
        [Test]
        public void EdgeTest()
        {
            string file = "EdgeTest.xml";

            try
            {
                using (FileStream input = new FileStream(file, FileMode.Create))
                {
                    this.testSheet.SaveSpreadsheetFile(input);
                }

                var anotherSheet = new Spreadsheet(5, 5);
                using (FileStream output = new FileStream(file, FileMode.Open))
                {
                    anotherSheet.LoadSpreadsheetFile(output);
                }

                // making sure each cell's value is null (empty field)
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Assert.IsNull(anotherSheet.GetCell(i, j).ValueInCell);
                    }
                }
            }
            finally
            {
                if (File.Exists(file))
                {
                    Assert.Pass(file);
                    File.Delete(file); // clean up
                }
            }
        }

        /// <summary>
        /// Exception test case
        /// Loading a file that does not exist.
        /// </summary>
        [Test]
        public void ExceptionTest()
        {
            string file = "where.xml"; // this file doesn't exist

            var anotherSheet = new Spreadsheet(5, 5);
            Assert.Throws<FileNotFoundException>(() =>
            {
                using (FileStream output = new FileStream(file, FileMode.Open))
                {
                    anotherSheet.LoadSpreadsheetFile(output);
                }
            });
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