﻿using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        //FabricsEntities db = new FabricsEntities(); //在Class下不能使用var
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();
            var data = db.Products.Where(p => p.ProductName.Contains("White"));
            return View(data);
        }
        public ActionResult Create()
        {
            var product = new Product()
            {
                ProductName = "White Dog",
                Active = true,
                Price = 200,
                Stock = 10
            };

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            // db.OrderLine.Where(p => p.ProductId == id)
            // product.OrderLine

            // 錯誤示範 (以下範例不要抄)
            /*
            foreach (var item in product.OrderLine.ToList())
            {
                db.OrderLine.Remove(item);
                db.SaveChanges();
            }
            */

            db.OrderLines.RemoveRange(product.OrderLines); // remove 關聯

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        public ActionResult Update(int id)
        {
            var product = db.Products.Find(id);
            product.ProductName += "!";

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityErrors in ex.EntityValidationErrors)
                {
                    foreach (var vError in entityErrors.ValidationErrors)
                    {
                        throw new DbEntityValidationException(vError.PropertyName + "發生錯誤" + vError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Update20()
        {
            var str = "%White%";
            db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price*1.2 WHERE ProductName LIKE @p0", str);
            //db.Database.ExecuteSqlCommand("UPDATE dbo.Product SET Price=Price*1.2 WHERE ProductName LIKE @p0", str);
            //var db = new FabricsEntities();
            //var data = db.Products.Where(p => p.ProductName.Contains("White"));
            //foreach (var item in data)
            //{
            //    if (item.Price.HasValue)
            //        item.Price = item.Price.Value * 1.2m;
            //}
            //db.SaveChanges(); 

            return RedirectToAction("Index");
        }
        public ActionResult ClientContribution()
        {
            var data = db.vw_ClientContribution.Take(10);
            return View(data);
        }
        public ActionResult ClientContribution2(string keyword = "Mary")
        {
            var data = db.Database.SqlQuery<ClientContributionViewModel>(@"
            	SELECT
                c.ClientId,
		        c.FirstName,
		        c.LastName,
		        (SELECT SUM(o.OrderTotal) 
		            FROM [dbo].[Order] o 
		            WHERE o.ClientId = c.ClientId) as OrderTotal
	            FROM 
		        [dbo].[Client] as c
                WHERE c.FirstName LIKE @p0", "%" + keyword + "%");

            return View(data);
        }
        public ActionResult ClientContribution3(string keyword = "Mary")
        {
            var data = db.usp_GetClientContribution(keyword);
            return View(data);
        }



    }
}