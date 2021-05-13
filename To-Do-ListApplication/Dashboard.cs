using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_ListApplication
{
    public partial class Dashboard : Form
    {

        List<Task> TaskList = new List<Task>();
        int id;
        public Dashboard()
        {
            InitializeComponent();
            DataAccess db = new DataAccess();
            TaskList = db.LoadData();
            updateBinding();
        }

        private void updateBinding()
        {
            taskDisplayGrid.DataSource = TaskList;
            foreach (DataGridViewColumn column in taskDisplayGrid.Columns)
            {
                column.ReadOnly = true;
            }
            taskDisplayGrid.Columns["Id"].Visible = false;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            TaskList = db.LoadData();
            updateBinding();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            new AddTask().Show();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            TaskList = db.searchData(textBox1.Text);
            updateBinding();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            DataAccess.setSelectedTask(new Task
            {
                Id = (int)taskDisplayGrid.CurrentRow.Cells[0].Value,
                Name = (string)taskDisplayGrid.CurrentRow.Cells[1].Value,
                Description = (string)taskDisplayGrid.CurrentRow.Cells[2].Value,
                DueDate = (DateTime)taskDisplayGrid.CurrentRow.Cells[3].Value,
                Priority = (string)taskDisplayGrid.CurrentRow.Cells[4].Value,
                IsCompleted = (Boolean)taskDisplayGrid.CurrentRow.Cells[5].Value
            });
            new EditTask().Show();
            
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (confirmDeletion())
            {
                DataAccess db = new DataAccess();
                id = (int)taskDisplayGrid.CurrentRow.Cells[0].Value;
                db.deleteData(id);
                MessageBox.Show("Task Successfully Deleted!!" +
                    "Press Load Task to View Updated Tasks");
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if(confirmDeletion())
            {
                DataAccess da = new DataAccess();
                da.clearData();
                MessageBox.Show("All Tasks Deleted!!" +
                  "Press Load Task to View changes");
            }
            
        }

        private bool confirmDeletion()
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                return true;
            else
                return false;
        }
    }
}
