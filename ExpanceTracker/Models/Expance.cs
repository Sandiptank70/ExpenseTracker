//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpenseTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expance
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string ExpName { get; set; }
        public int ExpAmt { get; set; }
        public string ExpDescription { get; set; }
        public System.DateTime ExpDate { get; set; }
    
        public virtual Category Category1 { get; set; }
    }
}