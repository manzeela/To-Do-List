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
    public partial class EditTask : Form
    {
       
        int id;
        public Task task;
       
        public EditTask()
        {
            InitializeComponent();
            priorityBox.Items.Add("LOW");
            priorityBox.Items.Add("MEDIUM");
            priorityBox.Items.Add("HIGH");
            dueDatePicker.MinDate = DateTime.Now;
            bindData();
        }

        public void bindData()
        {
            task = DataAccess.selectedTask();
            nameBox.Text = task.Name;
            descBox.Text = task.Description;
            priorityBox.SelectedItem = task.Priority;
            isCompletedCheck.Checked = task.IsCompleted;
            dueDatePicker.Value = task.DueDate;
            id = task.Id;
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            db.updateTask(id, nameBox.Text, descBox.Text, dueDatePicker.Value.Date, priorityBox.SelectedItem.ToString(), isCompletedCheck.Checked);
            MessageBox.Show("Task Successfully Deleted!!" +
            "Press Load Task to View Updated Tasks");
        }


    }
}
