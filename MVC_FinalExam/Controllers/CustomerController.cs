using MVC_FinalExam.ModelsAdd;
using MVC_FinalExam.ModelsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_FinalExam.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService customerService = new CustomerService();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }


        #region 查詢的部分
        public ActionResult QueryCustomer()
        {
            DropDown_Title();
            return View();
        }

        [HttpPost]
        public JsonResult QueryCustomer(CustomerModel CusModel)
        {
            return this.Json(customerService.GetQueryResult(CusModel));
        }
        #endregion

        #region 刪除的部分
        [HttpPost]
        public void QueryPage_Delete(int CustomerID)
        {
            customerService.DeleteQueryResult(CustomerID);
        }
        #endregion

        #region 新增的部分
        public ActionResult InsertCustomer()
        {
            DropDown_Title();
            return View();
        }

        [HttpPost]
        public void InsertCustomer(CustomerModel CusModel)
        {
            customerService.SaveCustomer(CusModel);
        }
        #endregion

        #region 修改的部分
        public ActionResult UpdateCustomer(int CustomerID)
        {
            DropDown_Title();
            ViewBag.UpdateCustomerID = CustomerID;
            return View();
        }

        [HttpPost]
        public JsonResult UpdateCustomer_getData(int CustomerID)
        {
            return this.Json(customerService.GetUpdateData(CustomerID));
        }


        #endregion

        #region MyViewBag
        public void DropDown_Title()
        {
            ViewBag.CustomerTitle = GetSelectListItem(customerService.GetCustomreTitle().ToArray());
        }


        public List<SelectListItem> GetSelectListItem(String[] listStr)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            string value = "";
            string[] MySplit = { };
            for (int i = -1; i < listStr.Length; i++)
            {
                if (i != -1)
                {
                    MySplit = listStr[i].Split('_');
                    value = MySplit[0];
                }
                else
                {
                    value = "0";
                }

                string text = (i == -1) ? text = string.Empty : text = MySplit[1];

                result.Add(new SelectListItem
                {
                    Value = value,
                    Text = text
                });
            }

            return result;
        }
        #endregion



    }
}