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
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductInfoVM>>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public GetAllProductHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<IEnumerable<ProductInfoVM>>Handle(GetAllProductQuery request,CancellationToken cancellationToken)
        {
            var data = await _dataAccessProvider.GetAllProducts();

            var results = data.Select(der => new ProductInfoVM
            {
                BidEndDate = der.BidEndDate,
                StartingPrice = der.StartingPrice,
                ShortDescription = der.ShortDescription,
                SellerId = der.SellerId,
                ProductName = der.ProductName,
                ProductId = der.ProductId,
                IsDeleted = der.IsDeleted,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                Category = der.Category,

            });

            return results;
        }
    }
}
