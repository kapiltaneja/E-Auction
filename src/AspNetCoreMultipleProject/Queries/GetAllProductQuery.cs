using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductInfoVM>>
{

}
}
