using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuraksApp
{
    public partial class EditCommands : Form
    {
        private Panel buttonPanel = new Panel();
        public EditCommands()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.myCommandObjects = new List<CommandObject>();

            for (int i = 0; i < dgvCommands.RowCount; i++)
            {
                if (dgvCommands.Rows[i].Cells[0].Value != null && dgvCommands.Rows[i].Cells[1].Value != null)
                {
                    if (dgvCommands.Rows[i].Cells[0].Value.ToString() != "" && dgvCommands.Rows[i].Cells[1].Value.ToString() != "")
                    {
                        Globals.myCommandObjects.Add(new CommandObject(dgvCommands.Rows[i].Cells[0].Value.ToString(), dgvCommands.Rows[i].Cells[1].Value.ToString()));
                    }
                }
            }

            Program.WriteCommands();
            this.Close();
            Application.Restart();
        }

        private void EditCommands_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            PopulateDataGridView();
        }


        private void SetupDataGridView()
        {
            this.Controls.Add(dgvCommands);

            dgvCommands.ColumnCount = 2;

            dgvCommands.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgvCommands.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCommands.ColumnHeadersDefaultCellStyle.Font = new Font(dgvCommands.Font, FontStyle.Bold);

            dgvCommands.Name = "dgvCommands";
            dgvCommands.Size = new Size(700, 250);
            dgvCommands.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvCommands.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvCommands.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvCommands.GridColor = Color.Black;
            dgvCommands.RowHeadersVisible = false;

            dgvCommands.Columns[0].Name = "Name";
            dgvCommands.Columns[0].Width = 150;
            dgvCommands.Columns[1].Name = "Command";
            dgvCommands.Columns[1].Width = 600;

            dgvCommands.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCommands.MultiSelect = true;
            dgvCommands.Dock = DockStyle.Fill;

        }
        private void PopulateDataGridView()
        {
            foreach (CommandObject cObject in Globals.myCommandObjects)
            {
                string[] tempRow = new string[2];
                tempRow[0] = cObject.Name;
                tempRow[1] = cObject.Command;
                dgvCommands.Rows.Add(tempRow);
            }
            dgvCommands.Columns[0].DisplayIndex = 0;
            dgvCommands.Columns[1].DisplayIndex = 1;
        }
    }
}
