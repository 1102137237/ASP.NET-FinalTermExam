using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class Product
    {
        /// <summary>
        /// 產品編號
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 供應商編號
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// 種類編號
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 停產
        /// </summary>
        public int Discontiuned { get; set; }
    }
}