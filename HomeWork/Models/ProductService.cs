using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace HomeWork.Models
{
    public class ProductService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }
        public List<Models.Product> GetProductById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT * FROM Production.Products";

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
        private List<Models.Product> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Product> result = new List<Models.Product>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.Product()
                {
                    ProductName = row["ProductName"].ToString(),
                    ProductID = (int)row["ProductID"]
                });
            }
            return result;
        }

        public List<Models.Product> GetPrice()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT UnitPrice FROM Production.Products";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                sqlAdapter.SelectCommand = new SqlCommand(sql, conn);
                sqlAdapter.Fill(result);
                conn.Close();

            }
            return this.MapOrderDataToList2(result);
        }
        private List<Models.Product> MapOrderDataToList2(DataTable orderData)
        {
            List<Models.Product> result = new List<Models.Product>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.Product()
                {
                    UnitPrice = (decimal)row["UnitPrice"]
                });
            }
            return result;
        }
    }
}