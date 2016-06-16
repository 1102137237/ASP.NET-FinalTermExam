using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HomeWork.Models
{
    public class CustomerService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Models.Customer> GetCustomerById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT CustomerID,CompanyName FROM Sales.Customers";

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

        private List<Models.Customer> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Customer> result = new List<Models.Customer>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.Customer()
                {
                    CompanyName = row["CompanyName"].ToString(),
                    CustomerID = (int)row["CustomerID"]
                });
            }
            return result;
        }
    }
}