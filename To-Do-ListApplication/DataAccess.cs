using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_ListApplication
{
    public class DataAccess
    {
        private static Task currentTask;
        public List<Task> LoadData()
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                var output = connection.Query<Task>("Tasks_GetAll").ToList();

                return output;
            }
        }

        public void InsertData(string name, string description, DateTime dueDate, string priority, bool isComplete)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                Task task = new Task{ Name = name, Description = description, DueDate = dueDate, Priority = priority, IsCompleted = isComplete};
                connection.Execute("Tasks_AddTask @Name, @Description, @Priority, @DueDate,  @IsCompleted", task);
            }
        }

        public List<Task> searchData(string name)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                var output = connection.Query<Task>("Tasks_SearchTask @Name",new { Name = name}).ToList();
                return output;
            }
        }

        public void updateTask(int id, string name, string description, DateTime date, string priority, bool completed)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                Task task = new Task {Id=id,  Name = name, Description = description, Priority = priority, DueDate = date, IsCompleted = completed };
                connection.Query<Task>("Tasks_UpdateTask @Id, @Name, @Description, @Priority, @DueDate, @IsCompleted", task);
            }
        }

        public void deleteData(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                connection.Execute("Tasks_deleteTask @Id", new { Id = id });
            }
        }
       

   
        public static void setSelectedTask(Task task)
        {
            currentTask = task;
        }
        public static Task selectedTask()
        {
            
            return currentTask;

        }

        public void clearData()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.conString("ToDoList")))
            {
                connection.Execute("Tasks_ClearTask");
            }
        }
    }
}
