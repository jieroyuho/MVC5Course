using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientLoginViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大輸入只能{1}個字元")]
        [DisplayName("名")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大輸入只能{1}個字元")]
        [DisplayName("中間名")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} 最大輸入只能{1}個字元")]
        [DisplayName("姓")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("[MF]", ErrorMessage = "{0}只能輸入M或F")]
        [DisplayName("性別")]
        public string Gender { get; set; }
       

    }

}