using AspNetCoreMultipleProject.Commands;

using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using DomainModel;
using DomainModel.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Handler
{
    public class UpdateBidHandler : IRequestHandler<UpdateBidCommand,Unit>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public UpdateBidHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<Unit> Handle(UpdateBidCommand request,CancellationToken cancellationToken)
        {
            var data = await _dataAccessProvider.GetAllBuyer();
            var results = data.Select(der => new BuyerInfoVM
            {
                Address = der.Address,
                BidAmount = der.BidAmount,
                City = der.City,
                CreatedDate = System.DateTime.Now,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                ProductId = der.ProductId,
                State = der.State,
                BuyerId = der.BuyerId
            });

           
            var buyerExists = results.Where(a => a.ProductId == request.productId && a.Email == request.buyerEmailId).SingleOrDefault();

            var dataproduct = await _dataAccessProvider.GetAllProducts();

            var resultsproduct = dataproduct.Select(derprod => new ProductInfoVM
            {
                BidEndDate = derprod.BidEndDate,
                StartingPrice = derprod.StartingPrice,
                ShortDescription = derprod.ShortDescription,
                SellerId = derprod.SellerId,
                ProductName = derprod.ProductName,
                ProductId = derprod.ProductId,
                IsDeleted = derprod.IsDeleted,
                DetailedDescription = derprod.DetailedDescription,
                CreatedDate = derprod.CreatedDate,
                Category = derprod.Category,

            });

           

            var productInfo = resultsproduct.Where(a => a.ProductId == request.productId).SingleOrDefault();
            if (productInfo == null)
            {
                throw new Exception("Product is not exists.");
            }
            if (buyerExists == null)
            {
                throw new Exception("This buyer is not exists.");
            }
            if (productInfo != null && productInfo.StartingPrice > request.newBidAmt)
            {
                throw new Exception("Bid Amount is not less then the product starting price.");
            }
            if (productInfo != null && productInfo.BidEndDate < System.DateTime.Now)
            {
                throw new Exception("Bid end date is expired.");
            }


            if (buyerExists != null)
            {
                await _dataAccessProvider.UpdateBid(request.productId, request.buyerEmailId, request.newBidAmt);

            }
            return Unit.Value;
        }
    }
}
