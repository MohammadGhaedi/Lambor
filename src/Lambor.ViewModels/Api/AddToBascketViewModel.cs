using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Api
{
    public class AddToBasketViewModel
    {
        public int? Count { get; set; }
        public int ProductId { get; set; }

    }
}
