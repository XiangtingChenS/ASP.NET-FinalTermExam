using MVC_FinalExam.Models;
using MVC_FinalExam.ModelsAdd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_FinalExam.ModelsService
{
    public class CustomerService
    {
        
        MyDB db = new MyDB();

        /// <summary>
        /// 取得員工職稱
        /// </summary>
        /// <returns></returns>
        public List<string> GetCustomreTitle()
        {
            List<string> result = new List<string>();
            result.Add("0001_Owner");
            result.Add("0002_Order Administrator");
            result.Add("0003_Sales Representative");
            result.Add("0004_Sales Agent");
            //var data = db.Customers
            //         .Select(x => new
            //         {
            //             x.ContactTitle
            //         }).ToList();

            //CodeTable未有索引鍵，因而無法匯入!!
            //因時間不夠，故先寫死

            //int index = 0;
            //foreach (var item in data)
            //{
            //    index++;
            //    if (item.ContactTitle == "0001")
            //    {
            //        result.Add(index+"_"+"Owner");
            //    }
            //    else if (item.ContactTitle == "0002")
            //    {
            //        result.Add(index + "_" + "Order Administrator");
            //    }
            //    else if (item.ContactTitle == "0003")
            //    {
            //        result.Add(index + "_" + "Sales Representative");
            //    }
            //    else if (item.ContactTitle == "0004")
            //    {
            //        result.Add(index + "_" + "Sales Agent");
            //    }
            //}
            return result;
        }


        /// <summary>
        /// 取得查詢結果
        /// </summary>
        /// <returns></returns>
        public List<CustomerModel> GetQueryResult(CustomerModel cusModel)
        {
            List<CustomerModel> result = new List<CustomerModel>();
            var data = db.Customers
                    .Select(x => new
                    {
                        x.CustomerID,
                        x.CompanyName,
                        x.ContactName,
                        x.ContactTitle
                    })
                    .Where(x =>
                            (cusModel.CustomerID == 0 || x.CustomerID.ToString().Contains(cusModel.CustomerID.ToString())) &&
                            (cusModel.CompanyName == null || x.CompanyName.ToString().Contains(cusModel.CompanyName.ToString())) &&
                            (cusModel.ContactName == null || x.ContactName.ToString().Contains(cusModel.ContactName.ToString())) &&
                            (cusModel.ContactTitle == "0"|| x.ContactTitle == cusModel.ContactTitle)
                     )
                    .OrderBy(x => x.CustomerID)
                    .ToList();


            //CodeTable未有索引鍵，因而無法匯入!!
            //因時間不夠，故先寫死
            //Assistant 0003  ???????
            string[] ptr = { "0001", "0002", "0003", "0004", "Assistant 0003" };
            string[] codeVal = { "Owner" , "Order Administrator" , "Sales Representative", "Sales Agent" , "" };

            foreach (var item in data)
            {
               
                CustomerModel CustomerModel = new CustomerModel()
                {
                    CustomerID = item.CustomerID,
                    CompanyName = item.CompanyName,
                    ContactName = item.ContactName,
                    ContactTitle = item.ContactTitle+"-"+codeVal[ptr.ToList().IndexOf(item.ContactTitle)]
                };
                result.Add(CustomerModel);
            }

            return result;
        }



        /// <summary>
        /// 刪除一筆客戶
        /// </summary>
        /// <param name="id"></param>
        public void DeleteQueryResult(int id)
        {
            var del_Cust = db.Customers
                           .Where(x => x.CustomerID == id)
                           .ToList();
            db.Customers.RemoveRange(del_Cust);

            db.SaveChanges();
        }

        /// <summary>
        /// 儲存新增的客戶資料
        /// </summary>
        /// <param name="cusModel"></param>
        public void SaveCustomer(CustomerModel cusModel)
        {
            Customers customer = new Customers()
            {

                CompanyName = cusModel.CompanyName,
                ContactName = cusModel.ContactName,
                ContactTitle = cusModel.ContactTitle,
                CreationDate = Convert.ToDateTime(cusModel.CreationDate),
                Address = cusModel.Address,
                Region = cusModel.Region,
                City = cusModel.City,
                PostalCode = cusModel.PostalCode,
                Country = cusModel.Country,
                Phone = cusModel.Phone
            };

            db.Customers.Add(customer);
            db.SaveChanges();

        }



        /// <summary>
        /// 取得要修改的客戶資料
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public CustomerModel GetUpdateData(int CustomerID)
        {
            var data = db.Customers
                       .Where(x => x.CustomerID == CustomerID)
                       .First();

            CustomerModel cu = new CustomerModel()
            {
                CustomerID = data.CustomerID,
                CompanyName = data.CompanyName,
                ContactName = data.ContactName,
                ContactTitle = data.ContactTitle,
                CreationDate = data.CreationDate.ToShortDateString(),
                Address = data.Address,
                Region = data.Region,
                City = data.City,
                PostalCode = data.PostalCode,
                Country = data.Country,
                Phone = data.Phone,
                Fax = data.Fax
            };

            return cu;
        }
    }
}