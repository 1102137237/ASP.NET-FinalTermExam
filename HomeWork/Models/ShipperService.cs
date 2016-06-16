using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class ShipperService
    {
        private string GetconnectionStrings()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Models.Shipper> GetShipperById()
        {
            DataTable result = new DataTable();
            string sql = @"SELECT ShipperID,CompanyName FROM Sales.Shippers";

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

        private List<Models.Shipper> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Shipper> result = new List<Models.Shipper>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Models.Shipper()
                {
                    CompanyName = row["CompanyName"].ToString(),
                    ShipperID = (int)row["ShipperID"]
                });
            }
            return result;
        }
    }
}