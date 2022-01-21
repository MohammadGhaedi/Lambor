using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambor.ViewModels.Api
{
    public class UpdateOrderViewModel
    {
        public long Id { get; set; }
        public string CostumerName { get; set; }
        public string CostumerPhone { get; set; }
        public string CostumerAddress { get; set; }
        public string Description { get; set; }
    }
}
