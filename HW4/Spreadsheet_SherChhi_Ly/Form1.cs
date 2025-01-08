// <copyright file="Form1.cs" company="Sher Chhi Ly">
// Copyright (c) Sher Chhi Ly. All rights reserved.
// </copyright>

using System.ComponentModel;
using SpreadsheetEngine;

namespace Spreadsheet_SherChhi_Ly
{
    /// <summary>
    /// This is a partial class for my UI.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// a Spreadsheet object as a member variable to main UI or ViewModel class – Form1.
        /// </summary>
        private Spreadsheet formSpreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Initializes the components for WinForms.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.ResetDataGridView();
            this.formSpreadsheet = new Spreadsheet(50, 26);
            this.formSpreadsheet.CellPropertyChanged += UpdateSpreadsheet;
            this.dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;

            this.InitializeDataGrid();
        }

        /// <summary>
        /// Purpose: will programmatically create columns A to Z and rows.
        /// </summary>
        public void InitializeDataGrid() // was for step 1
        {
            // initialize columns A - Z.
            for (int i = 65; i < 91; i++)
            {
                char convert = (char)i;
                this.dataGridView1.Columns.Add(convert.ToString(), convert.ToString());
            }

            // initialize rows 1 - 50. *modified using ascii numbers to fix zero-based index offset*
            for (int j = 0; j < 50; j++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }
        }

        /// <summary>
        /// Purpose: clears the grid of the spreadsheet.
        /// Taken from the MDSN documentation: DataGridViewColumnCollection.Clear Method.
        /// </summary>
        private void ResetDataGridView()
        {
            this.dataGridView1.CancelEdit();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.DataSource = null;
        }

        /// <summary>
        /// Form1 method to update the spreadsheet.
        /// Same logic as the update method in Cell.cs.
        /// </summary>
        /// <param name="sender"> object parameter. </param>
        /// <param name="e"> information about the property changed. </param>
        private void UpdateSpreadsheet(object sender, PropertyChangedEventArgs e)
        {
            Cell updateCell = (Cell)sender; // explicit cast to sender to use Cell properties

            // gets row and column of updated cell
            int updateRow = updateCell.RowIndex;
            int updateColumn = updateCell.ColumnIndex;

            var datagridCell = this.dataGridView1.Rows[updateRow].Cells[updateColumn];

            // responsibility: refreshes value displayed as text
            if (e.PropertyName == "ValueInCell") // checks if cell's value is updated
            {
                if (updateCell != null)
                {
                    datagridCell.Value = updateCell.ValueInCell; // updates corresponding cell
                }
            }

            // responsibility: refreshes cell background color
            if (e.PropertyName == "ColorInCell") // checks if the cell's background color changed
            {
                datagridCell.Style.BackColor = Color.FromArgb(
                 (int)(updateCell.BGColor >> 24) & 0xFF,  // Alpha
                 (int)(updateCell.BGColor >> 16) & 0xFF,  // Red
                 (int)(updateCell.BGColor >> 8) & 0xFF,   // Green
                 (int)(updateCell.BGColor & 0xFF));       // Blue
            }
        }

        /// <summary>
        /// Returns bool for if stacks for redo and undo is empty.
        /// </summary>
        private void UpdateUndoRedoUI()
        {
            // Set the enabled state based on the availability of undo/redo actions
            this.undoToolStripMenuItem.Enabled = this.formSpreadsheet.IsUnDoable();
            this.redoToolStripMenuItem.Enabled = this.formSpreadsheet.IsReDoable();
        }

        /// <summary>
        /// Required for demo.
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "321 Spreadsheet - Ly";

            this.button1.Text = "Perform Demo";
        }

        /// <summary>
        /// Demo for step 7.
        /// </summary>
        /// <param name="sender"> object parameter. </param>
        /// <param name="e"> event e parameter. </param>
        private void Button1_Click_1(object sender, EventArgs e) // REDUNDANT IN LATER ASSIGNMENTS
        {
            Random random = new Random();

            // a string in random cells
            for (int i = 0; i < 49; i++)
            {
                int someRow = random.Next(0, 50);
                int someCol = random.Next(0, 26);

                Cell demoCell1 = this.formSpreadsheet.GetCell(someRow, someCol);
                demoCell1.TextInCell = "NIGHTMARE";
            }

            // a string in every cell in Column B
            for (int j = 0; j < 50; j++)
            {
                Cell demoCell2 = this.formSpreadsheet.GetCell(j, 1);
                demoCell2.TextInCell = $"B{j + 1}";
            }

            // every cell in Column B to be duplicated in Column A
            for (int k = 0; k < 50; k++)
            {
                Cell demoCell3 = this.formSpreadsheet.GetCell(k, 0); // column index is at 'A' now
                demoCell3.TextInCell = $"B{k + 1}";
            }
        }

        /// <summary>
        /// Click begin to edit updater.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cell editCell = this.formSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = editCell.TextInCell;
        }

        /// <summary>
        /// Click to finish edit updater.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cell editCell = this.formSpreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
            string oldText = editCell.TextInCell;
            string? newText = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); // feature to consider: entering manually entering empty strings
            editCell.TextInCell = newText;
            TextChangeCommand textCommand = new TextChangeCommand(editCell, oldText, newText);
            this.formSpreadsheet.AddUndo(textCommand);

            this.UpdateUndoRedoUI();
        }

        /// <summary>
        /// Click to open ColorDialog and change the background color.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog1 = new ColorDialog())
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color chosenColor = colorDialog1.Color;

                    // convert the color to a uint for BGColor (ARGB format)
                    uint newColor = (uint)((chosenColor.A << 24) | (chosenColor.R << 16) |
                                          (chosenColor.G << 8) | chosenColor.B);

                    // list of cells in UI layer
                    List<Cell> selectedCells = new List<Cell>();

                    // update the BGColor for each selected cell in the DataGridView
                    foreach (DataGridViewCell datagridCell in this.dataGridView1.SelectedCells)
                    {
                        // find the corresponding SpreadsheetCell based on row and column
                        int row = datagridCell.RowIndex;
                        int column = datagridCell.ColumnIndex;
                        Cell cell = this.formSpreadsheet.GetCell(row, column);

                        selectedCells.Add(cell); // adds selected cell to our list
                    }

                    // instantiates the color command before each cell.BGColor is changed to properly preserve bg color
                    ColorChangeCommand colorCommand = new ColorChangeCommand(selectedCells, newColor);
                    this.formSpreadsheet.AddUndo(colorCommand);

                    foreach (var cell in selectedCells) // fires UpdateCell() to handle the color changes
                    {
                        cell.BGColor = newColor;
                    }

                    this.UpdateUndoRedoUI();
                }
            }
        }

        /// <summary>
        /// Click to Undo.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formSpreadsheet.Undo();
            this.UpdateUndoRedoUI();
        }

        /// <summary>
        /// Click to Redo.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.formSpreadsheet.Redo();
            this.UpdateUndoRedoUI();
        }

        /// <summary>
        /// Click to Save Spreadsheet file.
        /// </summary>
        /// <param name="sender">object type.</param>
        /// <param name="e"> e.</param>
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "XML File (*.xml)|*.xml"; // file type
                saveFileDialog1.DefaultExt = "xml";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream inputStream = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                        {
                            this.formSpreadsheet.SaveSpreadsheetFile(inputStream);
                        }

                        // not necessary but for style points
                        MessageBox.Show("File saved successfully.", "SAVED ^_^", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File saved unsuccessfully.", "ERROR >:(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Click to Load Spreadsheet file.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "XML File (*.xml)|*.xml"; // file type

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream outputStream = new FileStream(openFileDialog1.FileName, FileMode.Open))
                        {
                            this.formSpreadsheet.LoadSpreadsheetFile(outputStream);
                        }

                        // style points hell ye
                        MessageBox.Show("File loaded successfully.", "LOADED B-)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Where's the file?", "ERROR O.o", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// A basic feature I added to clear the entire spreadsheet manually. (not very robust)
        /// Does not work with undo/redo yet.
        /// But it is here for convenience sake.
        /// </summary>
        /// <param name="sender"> object type.</param>
        /// <param name="e"> e.</param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 49; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    this.formSpreadsheet.GetCell(i, j).TextInCell = string.Empty;
                    this.formSpreadsheet.GetCell(i, j).BGColor = 0xFFFFFFFF;
                }
            }
        }
    }
}