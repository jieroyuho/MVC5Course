﻿using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [LocalDebugOnly]
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class MBCController : BaseController
    {
        [ShareData]
        // GET: MBC
        public ActionResult Index()
        {
            ViewData["Temp1"] = "暫存資料 Temp1";

            var b = new ClientLoginViewModel()
            {
                FirstName = "Will",
                LastName = "Huang"
            };

            ViewData["Temp2"] = b;

            ViewBag.Temp3 = b;

            return View();
        }
        [ShareData]
        public ActionResult MyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyForm(ClientLoginViewModel c) // MyForm(FormCollection form) 弱型別
        {
            if (ModelState.IsValid)
            {
                TempData["MyFormData"] = c;
                return RedirectToAction("MyFormResult");
            }
            return View();
        }

        public ActionResult MyFormResult()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            var data = db.Products.OrderBy(p => p.ProductId).Take(10);
            return View(data);
        }

        public ActionResult BatchUpdate(ProductBatchUpdateViewModel[] items)
        {
            /*
             * 預設輸出的欄位名稱格式：item.ProductId
             * 要改成以下欄位格式：
             * items[0].ProductId
             * items[1].ProductId
             */
            if (ModelState.IsValid) //拿掉會影響ModelBind的狀態，Model Bind不會丟例外，只會影響State
            {
                foreach (var item in items)
                {
                    var product = db.Products.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }

                db.SaveChanges();

                return RedirectToAction("ProductList");
            }

            return View();
        }

        public ActionResult MyError()
        {
            throw new InvalidOperationException("ERROR");
            return View();
        }


    }
}