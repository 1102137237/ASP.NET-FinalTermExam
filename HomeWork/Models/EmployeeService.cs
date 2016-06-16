using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HomeWork.Models
{
    public class EmployeeService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Models.Employee> GetEmployeeById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT EmployeeID,FirstName+LastName as LastName FROM HR.Employees";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapOrderDataToList(result);
        }

        private List<Models.Employee> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Employee> result = new List<Models.Employee>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.Employee()
                {
                    LastName = row["LastName"].ToString(),
                    EmployeeID = (int)row["EmployeeID"]
                });
            }
            return result;
        }
    }
}
