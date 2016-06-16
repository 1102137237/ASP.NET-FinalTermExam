using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class Shipper
    {
        /// <summary>
        /// 出貨編號
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 電話號碼
        /// </summary>
        public string Phone { get; set; }
    }
}