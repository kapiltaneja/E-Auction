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
    public class ExistProductHandler : IRequestHandler<ExistProductCommand, bool>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public ExistProductHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<bool> Handle(ExistProductCommand request,CancellationToken cancellationToken)
        {
            return await _dataAccessProvider.ExistsProducts(request.productId);
        }
    }
}
