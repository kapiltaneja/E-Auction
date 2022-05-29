using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Queries
{
    public record GetAllBidsQuery(int productId) : IRequest<BidsInfoVM>
{
       
    }
}
