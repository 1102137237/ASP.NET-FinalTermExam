using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class OrderService
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public List<Models.Order> GetOrderByCondtioin(Models.OrderSearch arg)
        {

            DataTable dt = new DataTable();
            string sql = @"select A.OrderID,B.CompanyName,A.OrderDate,A.ShippedDate from Sales.Orders A join Sales.Customers B on A.CustomerID=B.CustomerID where (A.OrderID Like @OrderID Or @OrderID='') And 
						  (A.Orderdate=@Orderdate Or @Orderdate='') And (A.EmployeeID=@EmployeeID or @EmployeeID='') And (B.Companyname Like '%'+@CustomerName+'%' Or @CustomerName='' ) And (A.ShipperID Like @ShipperID Or @ShipperID='' )
                          And (A.ShippedDate=@ShippedDate Or @ShippedDate='') And (A.RequiredDate=@RequiredDate Or @RequiredDate='')";


            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", arg.OrderID == null ? string.Empty : arg.OrderID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", arg.OrderDate == null ? string.Empty : arg.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", arg.CustomerName == null ? string.Empty : arg.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", arg.ShipperID == null ? string.Empty : arg.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID == null ? string.Empty : arg.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", arg.ShippedDate == null ? string.Empty : arg.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", arg.RequiredDate == null ? string.Empty : arg.RequiredDate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            return this.MapOrderDataToList(dt);
        }


        public Models.Order GetOrderById(string OrderID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT O.OrderID,O.CustomerID,C.CompanyName as CustomerName,
                           E.EmployeeID,E.FirstName+E.LastName as EmployeeName,O.OrderDate,
                           O.RequiredDate,O.ShippedDate,O.ShipperID,S.CompanyName,O.Freight,
                           O.ShipName,O.ShipAddress,O.ShipCity,O.ShipRegion,O.ShipPostalCode,O.ShipCountry 
                           FROM Sales.Orders AS O JOIN Sales.Customers AS C ON O.CustomerID=C.CustomerID
                           JOIN HR.Employees AS E ON O.EmployeeID=E.EmployeeID
                           JOIN Sales.Shippers AS S ON O.ShipperID=S.ShipperID
                           Where OrderID=@OrderID";


            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDataToList2(dt).FirstOrDefault();
        }

        private List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    OrderID = (int)row["OrderID"],
                    CustomerName = row["CompanyName"].ToString(),
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    /*CustomerID = (int)row["CustomerID"],
                    EmployeeID = (int)row["EmployeeID"],
                    //OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    //ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperID = (int)row["ShipperID"],
                    Freight = (double)row["Freight"],
                    ShipName = row["ShipName"].ToString(),
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),*/

                });
            }
            return result;
        }

        private List<Models.Order> MapOrderDataToList2(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    OrderID = (int)row["OrderID"],
                    CustomerID = (int)row["CustomerID"],
                    CustomerName = row["CustomerName"].ToString(),
                    EmployeeID = (int)row["EmployeeID"],
                    EmployeeName = row["EmployeeName"].ToString(),
                    CompanyName = row["CompanyName"].ToString(),
                    OrderDate = row["OrderDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["OrderDate"],
                    RequiredDate = row["RequiredDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequiredDate"],
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperID = (int)row["ShipperID"],
                    Freight = (decimal)row["Freight"],
                    ShipName = row["ShipName"].ToString(),
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),

                });
            }
            return result;
        }

        /// <summary>
		/// 新增訂單
		/// </summary>
		/// <param name="Order"></param>
		/// <returns>訂單編號</returns>
		public string InsertOrder(Models.Order Order)
        {
            string sql = @" Insert INTO Sales.Orders
						 (
							CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipperID,Freight,
							ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry
						)
						VALUES
						(
							@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipperID,@Freight,
							@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry
			            )
                        Select SCOPE_IDENTITY()
						";
            string sql2 = @" Insert INTO Sales.OrderDetails
                             (
                                OrderID,ProductID,UnitPrice,Qty
                             )
                             VALUES
                             (
                                @OrderID,@ProductID,@UnitPrice,@Qty
                             )
                           ";
            string OrderID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                
                cmd.Parameters.Add(new SqlParameter("@CustomerID", Order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", Order.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", Order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", Order.RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", Order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", Order.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@Freight", Order.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipName", Order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", Order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", Order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", Order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", Order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", Order.ShipCountry));


                OrderID = cmd.ExecuteScalar().ToString();

                for (int i = 0; i < Order.OrderDetails.Count; i++)
                {
                    cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    cmd2.Parameters.Add(new SqlParameter("@ProductID", Order.OrderDetails[i].ProductID));
                    cmd2.Parameters.Add(new SqlParameter("@UnitPrice", Order.OrderDetails[i].UnitPrice));
                    cmd2.Parameters.Add(new SqlParameter("@Qty", Order.OrderDetails[i].Qty));
                    //cmd2.Parameters.Add(new SqlParameter("@Discount", Order.OrderDetails[i].Discount == null ? string.Empty : Order.OrderDetails[i].Discount));
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();
            }
            
            return OrderID;
        }

        public void DeleteOrderByID(string OrderID)
        {
            try
            {
                string sql = "Delete FROM Sales.OrderDetails Where OrderID=@OrderID Delete FROM Sales.Orders Where OrderID=@OrderID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}