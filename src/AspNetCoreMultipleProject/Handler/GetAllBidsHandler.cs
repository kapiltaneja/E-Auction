using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using DomainModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Handler
{
    public class GetAllBidsHandler : IRequestHandler<GetAllBidsQuery, BidsInfoVM>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public GetAllBidsHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<BidsInfoVM> Handle(GetAllBidsQuery request,CancellationToken cancellationToken)
        {
            BidsInfoVM result = new BidsInfoVM();
            ProductInfoVM productInfoVM = new ProductInfoVM();
            List<BuyerInfoVM> buyerInfoVM = new List<BuyerInfoVM>();
            var productInfo = await _dataAccessProvider.GetProductById(request.productId);
            var buyerInfo = await _dataAccessProvider.GetAllBidsByProductId(request.productId);

            if (productInfo != null)
            {
                productInfoVM.BidEndDate = productInfo.BidEndDate;
                productInfoVM.Category = productInfo.Category;
                productInfoVM.CreatedDate = productInfo.CreatedDate;
                productInfoVM.DetailedDescription = productInfo.DetailedDescription;
                productInfoVM.IsDeleted = productInfo.IsDeleted;
                productInfoVM.ProductId = productInfo.ProductId;
                productInfoVM.ProductName = productInfo.ProductName;
                productInfoVM.SellerId = productInfo.SellerId;
                productInfoVM.ShortDescription = productInfo.ShortDescription;
                productInfoVM.StartingPrice = productInfo.StartingPrice;

            }

            if (buyerInfo != null && buyerInfo.Count > 0)
            {
                foreach (var item in buyerInfo)
                {
                    BuyerInfoVM buyer = new BuyerInfoVM();
                    buyer.Address = item.Address;
                    buyer.BidAmount = item.BidAmount;
                    buyer.BuyerId = item.BuyerId;
                    buyer.City = item.City;
                    buyer.CreatedDate = item.CreatedDate;
                    buyer.Email = item.Email;
                    buyer.FirstName = item.FirstName;
                    buyer.LastName = item.LastName;
                    buyer.Phone = item.Phone;
                    buyer.PinCode = item.PinCode;
                    buyer.ProductId = item.ProductId;
                    buyer.State = item.State;
                    buyerInfoVM.Add(buyer);
                }
            }

            result.buyerInfoVM = buyerInfoVM;
            result.productInfoVM = productInfoVM;
            return result;
        }
    }
}
