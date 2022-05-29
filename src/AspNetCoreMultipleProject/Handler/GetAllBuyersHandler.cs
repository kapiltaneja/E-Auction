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
    public class GetAllBuyersHandler : IRequestHandler<GetAllBuyersQuery, IEnumerable<BuyerInfoVM>>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public GetAllBuyersHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<IEnumerable<BuyerInfoVM>>Handle(GetAllBuyersQuery request,CancellationToken cancellationToken)
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

            return results;
        }
    }
}
