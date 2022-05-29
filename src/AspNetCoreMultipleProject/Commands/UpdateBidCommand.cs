using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Commands
{
    public record UpdateBidCommand(int productId, string buyerEmailId, double newBidAmt) : IRequest<Unit>
{

}
}
