using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class Employee
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// 名子
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        public int Title { get; set; }

        /// <summary>
        /// 稱呼
        /// </summary>
        public string TitleOfCourtesy { get; set; }
        
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 雇用日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 郵政編碼
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 電話號碼
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 管理者編號
        /// </summary>
        public int ManagerID { get; set; }

        public string Name { get; set; }

        public string Val { get; set; }

        public string Gender { get; set; }

        public string MonthlyPayment { get; set; }

        public string YearlyPayment { get; set; }
    }
}