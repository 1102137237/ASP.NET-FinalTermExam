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

        public List<Models.Employee> GetVal()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT DISTINCT A.Title,(A.Title+'-'+B.CodeVal) as Val From HR.Employees A join dbo.CodeTable B on A.Title=B.CodeId Where (B.CodeType like 'TITLE')";

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
                    Title = (int)row["Title"],
                    Val = row["Val"].ToString(),
                });
            }
            return result;
        }
    }
}
