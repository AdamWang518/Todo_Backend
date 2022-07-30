using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Todo_Backend.Models;

namespace Todo_Backend.Controllers
{
    public class TodoController : ApiController
    {
        String connectionString = "Persist Security Info=False;Integrated Security=true;  Initial Catalog = Todo_List; Server=localhost";
        [HttpPost]
        [Route("setTodoList")]
        public Response setTodoList(TodoModel todoModel)
        {
            
            String sqlcmd = $@"INSERT INTO todo_table (date, text, time,comment)
                        VALUES ('{todoModel.date}', '{todoModel.text}', '{todoModel.time}','{todoModel.comment}')";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlcmd, sqlConnection);
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
            return new Response();
        }
        [HttpGet]
        [Route("getTodoList")]
        public Response getTodoList()
        {
            List<TodoModel> todoModel = new List<TodoModel>(); 
            String sqlcmd = "SELECT * FROM todo_table";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlcmd, sqlConnection);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                //dataReader ["欄位名稱"].ToString()    資料庫的資料
                TodoModel model = new TodoModel();
                model.id = (Guid)dr["id"];
                model.date = (String)dr["date"];
                model.comment = (String)dr["comment"];
                model.time = (String)dr["time"];
                model.text = (String)dr["text"];
                todoModel.Add(model);
            }
            sqlConnection.Close();
            return new Response(true, todoModel);
        }
        [HttpPost]
        [Route("updateTodoList")]
        public Response updateTodoList(TodoModel todoModel)
        {
            String sqlcmd = $@"UPDATE todo_table SET date='{todoModel.date}', text='{todoModel.text}', time='{todoModel.time}',comment='{todoModel.comment}' WHERE id = '{todoModel.id}'; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlcmd, sqlConnection);
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
            return new Response();
        }
        [HttpGet]
        [Route("deleteTodoList")]
        public Response deleteTodoList(Guid id)
        {
            String sqlcmd = $@"DELETE FROM todo_table WHERE id='{id}'; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlcmd, sqlConnection);
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
            return new Response();
        }
    }
}
