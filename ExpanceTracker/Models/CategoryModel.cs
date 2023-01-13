using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Is Required Field")]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }
        [Required(ErrorMessage = "Category Amount Is Required Field")]
        [Display(Name = "Amount")]
        [Range(1, 1000000000000, ErrorMessage = "Amount Must be Positive")]
        public int CatAmount { get; set; }
        public Nullable<int> CatAvalibleAmt { get; set; }
    }
}