using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Commands
{
    public record DeleteProductCommand(long productId) : IRequest<Unit>
{

}
}
