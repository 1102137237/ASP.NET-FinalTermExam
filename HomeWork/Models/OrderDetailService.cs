using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HomeWork.Models
{
    public class OrderDetailService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Models.OrderDetails> GetOrderDetailById(string OrderID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ProductID,UnitPrice,Qty FROM Sales.OrderDetails WHERE OrderID=@OrderID";

            using (SqlConnection conn = new SqlConnection(this.GetconnectionStrings()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();

            }
            return this.MapOrderDataToList(dt);
        }

        private List<Models.OrderDetails> MapOrderDataToList(DataTable orderData)
        {
            List<Models.OrderDetails> result = new List<Models.OrderDetails>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.OrderDetails()
                {
                    ProductID = (int)row["ProductID"],
                    UnitPrice = row["UnitPrice"].ToString(),
                    Qty = row["Qty"].ToString(),
                });
            }
            return result;
        }
    }
}