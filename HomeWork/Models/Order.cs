using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace HomeWork.Models
{
    public class Order
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public Order()
        {
            var ods = new List<Models.OrderDetails>();
            ods.Add(new OrderDetails() { ProductID = 77 });
            this.OrderDetails = ods;

        }
        /// <summary>
        /// 訂單編號
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 顧客編號
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// 顧客名稱
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 員工編號
        /// </summary>
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// 訂單日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 需要日期
        /// </summary>
        public DateTime? RequiredDate { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary>
        public DateTime? ShippedDate { get; set; }

        /// <summary>
        /// 出貨編號
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// 出貨說明
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// 出貨地址
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// 出貨城市
        /// </summary>
        public string ShipCity { get; set; }

        /// <summary>
        /// 出貨地區
        /// </summary>
        public string ShipRegion { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string ShipPostalCode { get; set; }

        /// <summary>
        /// 出貨國家
        /// </summary>
        public string ShipCountry { get; set; }

        /// <summary>
        /// 訂單明細
        /// </summary>
        public List<OrderDetails> OrderDetails { get; set; }
    }
}