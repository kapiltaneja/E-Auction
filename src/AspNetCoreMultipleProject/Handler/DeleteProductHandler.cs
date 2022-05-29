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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
        private readonly IDataAccessProvider _dataAccessProvider;

        public DeleteProductHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<Unit> Handle(DeleteProductCommand request,CancellationToken cancellationToken)
        {
            await _dataAccessProvider.DeleteProduct(request.productId);
            return Unit.Value;
        }
    }
}
