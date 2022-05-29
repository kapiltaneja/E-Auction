using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.ViewModels
{
    public class ProductInfoVM
    {
        public int ProductId { get; set; }
        public int SellerId { get; set; }

        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }

        public double StartingPrice { get; set; }

        public DateTime BidEndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
