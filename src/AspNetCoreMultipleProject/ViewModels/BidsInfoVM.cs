using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.ViewModels
{
    public class BidsInfoVM
    {
        public ProductInfoVM productInfoVM { get; set; }

        public List<BuyerInfoVM> buyerInfoVM { get; set; }
    }
}
