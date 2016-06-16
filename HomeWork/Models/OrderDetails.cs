using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class OrderDetails
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 產品編號
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public string UnitPrice { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public string Qty { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public string Discount { get; set; }
    }
}