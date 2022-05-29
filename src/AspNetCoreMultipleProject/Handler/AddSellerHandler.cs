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
    public class AddSellerHandler : IRequestHandler<AddSellerCommand, SellerInfoVM>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public AddSellerHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<SellerInfoVM> Handle(AddSellerCommand request,CancellationToken cancellationToken)
        {
            var sellerRecord = new SellerInfo
            {
                Address = request.SellerInfoVM.Address,
                City = request.SellerInfoVM.City,
                Email = request.SellerInfoVM.Email,
                FirstName = request.SellerInfoVM.FirstName,
                LastName = request.SellerInfoVM.LastName,
                Phone = request.SellerInfoVM.Phone,
                PinCode = request.SellerInfoVM.PinCode,
                State = request.SellerInfoVM.State
            };



            var der = await _dataAccessProvider.AddSeller(sellerRecord);

            var result = new SellerInfoVM
            {
                Address = der.Address,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                State = der.State,
                SellerId = der.SellerId
            };

            return result;
        }
    }
}
