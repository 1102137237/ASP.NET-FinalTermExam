using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HomeWork.Models;

namespace HomeWork.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        

        public ActionResult Index(Models.OrderSearch arg)
        {
            Models.EmployeeService EmployeeService = new Models.EmployeeService();

            List<Models.Employee> result = EmployeeService.GetEmployeeById();
            List<SelectListItem> EmployeeData = new List<SelectListItem>();
            EmployeeData.Add(new SelectListItem()
            {
                Text = "",
                Value = "",
            });
            ViewData["EmpData"] = EmployeeData;
            foreach (var item in result)
            {
                EmployeeData.Add(new SelectListItem()
                {
                    Text = item.LastName,
                    Value = item.EmployeeID.ToString(),
                });
                ViewData["EmpData"]= EmployeeData;
            }

            
            Models.ShipperService ShipperService = new Models.ShipperService();

            List<Models.Shipper> result2 = ShipperService.GetShipperById();
            List<SelectListItem> ShipperData = new List<SelectListItem>();
            ShipperData.Add(new SelectListItem()
            {
                Value = "",
                Text = ""
            });
            ViewData["ShipperData"] = ShipperData;
            foreach (var item in result2)
            {
                ShipperData.Add(new SelectListItem()
                {
                    Value = item.ShipperID.ToString(),
                    Text = item.CompanyName
                });
                ViewData["ShipperData"] = ShipperData;
            }

           
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.SearchResult = orderService.GetOrderByCondtioin(arg);
            return View();
        }
        
        public ActionResult InsertOrder()
        {
            Models.EmployeeService EmployeeService = new Models.EmployeeService();

            List<Models.Employee> result = EmployeeService.GetEmployeeById();
            List<SelectListItem> EmployeeData = new List<SelectListItem>();
           
            foreach (var item in result)
            {
                EmployeeData.Add(new SelectListItem()
                {
                    Text = item.LastName,
                    Value = item.EmployeeID.ToString(),
                });
                ViewData["EmpData"] = EmployeeData;
            }

            Models.CustomerService CustomerService = new Models.CustomerService();

            List<Models.Customer> result2 = CustomerService.GetCustomerById();
            List<SelectListItem> CustomerData = new List<SelectListItem>();

            foreach (var item in result2)
            {
                CustomerData.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.CustomerID.ToString(),
                });
                ViewData["CustomerData"] = CustomerData;
            }

            Models.ShipperService ShipperService = new Models.ShipperService();

            List<Models.Shipper> result3 = ShipperService.GetShipperById();
            List<SelectListItem> ShipperData = new List<SelectListItem>();
            
            foreach (var item in result3)
            {
                ShipperData.Add(new SelectListItem()
                {
                    Value = item.ShipperID.ToString(),
                    Text = item.CompanyName
                });
                ViewData["ShipperData"] = ShipperData;
            }

            Models.ProductService ProductService = new Models.ProductService();

            List<Models.Product> result4 = ProductService.GetProductById();
            List<SelectListItem> ProductData = new List<SelectListItem>();

            foreach (var item in result4)
            {
                ProductData.Add(new SelectListItem()
                {
                    Value = item.ProductID.ToString(),
                    Text = item.ProductName
                });
                ViewData["ProductData"] = ProductData;
            }
            

            List<Models.Product> result5 = ProductService.GetPrice();
            List<SelectListItem> PriceData = new List<SelectListItem>();

            ViewBag.PriceData = PriceData;
            foreach (var item in result5)
            {
                PriceData.Add(new SelectListItem()
                {
                    Value = item.UnitPrice.ToString()
                });
                ViewBag.PriceData = PriceData;
            }
            

            return View(new Models.Order());
        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult InsertOrder(Models.Order Order)
        {
                OrderService OrderService = new OrderService();
                OrderService.InsertOrder(Order);
                return RedirectToAction("Index");
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteOrder(string OrderID)
        {
            try
            {
                OrderService OrderService = new OrderService();
                OrderService.DeleteOrderByID(OrderID);
                return this.Json(true);
            }
            catch (Exception)
            {
                return this.Json(false);
            }


        }

        public ActionResult UpdateOrder(string OrderID)
        {
            Models.OrderService OrderService = new Models.OrderService();
            ViewBag.OrderData = OrderService.GetOrderById(OrderID);

            Models.OrderDetailService OrderDetailService = new Models.OrderDetailService();
            List<Models.OrderDetails> OrderDetailData = OrderDetailService.GetOrderDetailById(OrderID);
            ViewBag.OrderDetailData = OrderDetailData;

            Models.EmployeeService EmployeeService = new Models.EmployeeService();

            List<Models.Employee> result = EmployeeService.GetEmployeeById();
            List<SelectListItem> EmployeeData = new List<SelectListItem>();

            foreach (var item in result)
            {
                EmployeeData.Add(new SelectListItem()
                {
                    Text = item.LastName,
                    Value = item.EmployeeID.ToString(),
                    Selected = item.EmployeeID.Equals(ViewBag.OrderData.EmployeeID),
                });
                ViewData["EmpData"] = EmployeeData;
            }

            Models.CustomerService CustomerService = new Models.CustomerService();

            List<Models.Customer> result2 = CustomerService.GetCustomerById();
            List<SelectListItem> CustomerData = new List<SelectListItem>();

            foreach (var item in result2)
            {
                CustomerData.Add(new SelectListItem()
                {
                    Text = item.CompanyName,
                    Value = item.CustomerID.ToString(),
                    Selected = item.CustomerID.Equals(ViewBag.OrderData.CustomerID),
                });
                ViewData["CustomerData"] = CustomerData;
            }

            Models.ShipperService ShipperService = new Models.ShipperService();

            List<Models.Shipper> result3 = ShipperService.GetShipperById();
            List<SelectListItem> ShipperData = new List<SelectListItem>();

            foreach (var item in result3)
            {
                ShipperData.Add(new SelectListItem()
                {
                    Value = item.ShipperID.ToString(),
                    Text = item.CompanyName,
                    Selected = item.ShipperID.Equals(ViewBag.OrderData.ShipperID),
                });
                ViewData["ShipperData"] = ShipperData;
            }

            Models.ProductService ProductService = new Models.ProductService();

            List<Models.Product> result4 = ProductService.GetProductById();
            List<SelectListItem> ProductData = new List<SelectListItem>();
            List<List<SelectListItem>> getProductList = new List<List<SelectListItem>>();

            

            for(int i=0;i< OrderDetailData.Count; i++) { 
                foreach (var item in result4)
                {
                    ProductData.Add(new SelectListItem()
                    {
                        Value = item.ProductID.ToString(),
                        Text = item.ProductName,
                        Selected = item.ProductID.Equals(ViewBag.OrderDetailData[i].ProductID)
                    });
                }
                getProductList.Add(new List<SelectListItem>(ProductData));
                ProductData.Clear();
            }

            ViewBag.ProductData = getProductList;

            List<Models.Product> result5 = ProductService.GetPrice();
            List<SelectListItem> PriceData = new List<SelectListItem>();

            foreach (var item in result5)
            {
                PriceData.Add(new SelectListItem()
                {
                    Value = item.UnitPrice.ToString()
                });
                ViewBag.PriceData = PriceData;
            }


            DateTime orderdate = Convert.ToDateTime(ViewBag.OrderData.OrderDate);
            ViewBag.OrderDate = (orderdate.ToString("yyyy-MM-dd"));
            DateTime requireddate = Convert.ToDateTime(ViewBag.OrderData.RequiredDate);
            ViewBag.RequiredDate = (requireddate.ToString("yyyy-MM-dd"));
            DateTime shippeddate = Convert.ToDateTime(ViewBag.OrderData.ShippedDate);
            ViewBag.ShippedDate = (shippeddate.ToString("yyyy-MM-dd"));

            //ViewBag.SearchResult = OrderService.GetOrderDetailByID(OrderID);

            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOrder(Models.Order Order)
        {
            Models.OrderService OrderService = new OrderService();
            OrderService.DeleteOrderDetial(Order);
            OrderService.UpdateOrder(Order);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder2(string OrderID)
        {
            OrderService OrderService = new OrderService();
            OrderService.DeleteOrderByID(OrderID);
            return RedirectToAction("Index");
        }
    }
}