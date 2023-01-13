using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models
{
    public class ExpanceLimit
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> AvalibleAmt { get; set; }
        [Required(ErrorMessage ="This Field Is Required")]
        [Range(1, 1000000000000, ErrorMessage = "Amount Must be Positive")]
        [Display(Name = "Expense Limit")]
        public int ExpLimit { get; set; }
    }
}