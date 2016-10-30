using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductBatchUpdateViewModel //: IValidatableObject
        //在ViewModle下的驗證只會發生在Model Binding時，本範例只會在DBSaving時發生，所以可以拿掉此驗證方法
        //Model Binding不會有例外。只會丟ModelState
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.Stock < 100 && this.Price > 20)
        //    {
        //        yield return new ValidationResult("庫存與商品金額的條件錯誤", new string[] { "Price" });
        //    }
        //}
    }
}