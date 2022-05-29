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
    public class GetAllSellerHandler : IRequestHandler<GetAllSellerQuery, IEnumerable<SellerInfoVM>>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public GetAllSellerHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<IEnumerable<SellerInfoVM>>Handle(GetAllSellerQuery request,CancellationToken cancellationToken)
        {
            var data = await _dataAccessProvider.GetAllSeller();

            var results = data.Select(der => new SellerInfoVM
            {
                Address = der.Address,
                State = der.State,
                SellerId = der.SellerId,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
            });

            return results;
        }
    }
}
