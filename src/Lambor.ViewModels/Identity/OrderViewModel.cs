
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lambor.Entities;
using Lambor.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lambor.ViewModels.Identity
{
    public class OrderViewModel
    {
        [HiddenInput]
        public long Id { get; set; }
        
        [Display(Name = "نام مشتری")]
        public string CostumerName { get; set; }
        [Display(Name = "تلفن مشتری")]
        public string CostumerPhone { get; set; }
        [Display(Name = "آدرس مشتری")]
        public string CostumerAddress { get; set; }
        [Display(Name = "شرح سفارش")]
        public string Description { get; set; }
        [Display(Name = "تاریخ سفارش")]
        public DateTime OrderDateTime { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public OrderStatus OrderStatus { get; set; }
    }

    
}
