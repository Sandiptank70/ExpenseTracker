using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ExpenseTracker.Models
{
    public class ExpanceModel
    {
        
        public int Id { get; set; }
        [Required]
        public int Category { get; set; }
        [Display(Name = "Expance Name")]
        [Required]
        public string ExpName { get; set; }
        [Display(Name = "Expance Amount")]
        [Required]
        public int ExpAmt { get; set; }
        [Display(Name = "Expance Description")]
        [Required]
        public string ExpDescription { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Expance Date")]
        [Required]
        public System.DateTime ExpDate { get; set; }

        public string CatName { get; set; }
    }
}