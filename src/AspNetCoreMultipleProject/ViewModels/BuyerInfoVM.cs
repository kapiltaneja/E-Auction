using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.ViewModels
{
    public class BuyerInfoVM
    {
        public int BuyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int ProductId { get; set; }

        public double BidAmount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
