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
    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductInfoVM>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public AddProductHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<ProductInfoVM> Handle(AddProductCommand request,CancellationToken cancellationToken)
        {
            var productRecord = new ProductInfo
            {
                BidEndDate = request.ProductInfoVM.BidEndDate,
                Category = request.ProductInfoVM.Category,
                DetailedDescription = request.ProductInfoVM.DetailedDescription,
                CreatedDate = System.DateTime.UtcNow,
                IsDeleted = false,
                ProductName = request.ProductInfoVM.ProductName,
                SellerId = request.ProductInfoVM.SellerId,
                ShortDescription = request.ProductInfoVM.ShortDescription,
                StartingPrice = request.ProductInfoVM.StartingPrice,
            };



            var der = await _dataAccessProvider.AddProduct(productRecord);

            var result = new ProductInfoVM
            {
                BidEndDate = der.BidEndDate,
                Category = der.Category,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                IsDeleted = der.IsDeleted,
                ProductName = der.ProductName,
                SellerId = der.SellerId,
                ShortDescription = der.ShortDescription,
                StartingPrice = der.StartingPrice,
                ProductId = der.ProductId,
            };

            return result;
        }
    }
}
