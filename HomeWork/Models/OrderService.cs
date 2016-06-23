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
        public List<Models.Employee> GetEmploueeByCondtioin(Models.Employee arg)
        {

            DataTable dt = new DataTable();
            string sql = @"select A.EmployeeID,(A.FirstName+A.LastName) AS Name,(A.Title+'-'+B.CodeVal) as Val,A.HireDate,A.Gender from HR.Employees A join dbo.CodeTable B on A.Title=B.CodeId Where (B.CodeType like 'TITLE') And (A.EmployeeID=@EmployeeID or @EmployeeID='') And 
                           ((A.FirstName+A.LastName) Like '%'+@Name+'%' Or @Name='') And ((A.Title+'-'+B.CodeVal) like @Val Or @Val='') And (A.HireDate=@HireDate Or @HireDate='') Order by EmployeeID";


            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmployeeID == null ? string.Empty : arg.EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@Name", arg.Name == null ? string.Empty : arg.Name));
                cmd.Parameters.Add(new SqlParameter("@Val", arg.Val == null ? string.Empty : arg.Val));
                cmd.Parameters.Add(new SqlParameter("@HireDate", arg.HireDate == null ? string.Empty : arg.HireDate));
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

        private List<Models.Employee> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Employee> result = new List<Employee>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Employee()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    Name = row["Name"].ToString(),
                    Val = row["Val"].ToString(),
                    Gender = row["Gender"].ToString(),
                    HireDate = row["HireDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["HireDate"],
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

        public string UpdateOrder(Models.Order Order)
        {
            string sql = @" Update Sales.Orders
                            SET CustomerID=@CustomerID,EmployeeID=@EmployeeID,OrderDate=@OrderDate,RequiredDate=@RequiredDate,ShippedDate=@ShippedDate,ShipperID=@ShipperID,
                            Freight=@Freight,ShipCountry=@ShipCountry,ShipCity=@ShipCity,ShipRegion=@ShipRegion,ShipPostalCode=@ShipPostalCode,ShipAddress=@ShipAddress,ShipName=@ShipName
                            Where OrderID=@OrderID
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

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd.Parameters.Add(new SqlParameter("@OrderID", Order.OrderID));
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

                cmd.ExecuteNonQuery();

                for (int i = 0; i < Order.OrderDetails.Count; i++)
                {
                    cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.Add(new SqlParameter("@OrderID", Order.OrderID));
                    cmd2.Parameters.Add(new SqlParameter("@ProductID", Order.OrderDetails[i].ProductID));
                    cmd2.Parameters.Add(new SqlParameter("@UnitPrice", Order.OrderDetails[i].UnitPrice));
                    cmd2.Parameters.Add(new SqlParameter("@Qty", Order.OrderDetails[i].Qty));
                    //cmd2.Parameters.Add(new SqlParameter("@Discount", Order.OrderDetails[i].Discount == null ? string.Empty : Order.OrderDetails[i].Discount));
                    cmd2.ExecuteNonQuery();
                }

                conn.Close();
            }

            return "0";
        }

        public void DeleteEmployeeByID(string EmployeeID)
        {
            try
            {
                string sql = "Delete FROM HR.Employees Where EmployeeID=@EmployeeID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOrderDetial(Models.Order Order)
        {
            string sql = "Delete FROM Sales.OrderDetails Where OrderID=@OrderID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderID", Order.OrderID));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}