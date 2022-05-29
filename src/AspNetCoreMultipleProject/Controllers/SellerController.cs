using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreMultipleProject.Commands;
using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
       
        private readonly IMediator _mediator;
        public SellerController(IMediator mediator)
        {
            
            _mediator = mediator;
        }


        [HttpGet]
        [Route("/getAllSeller")]
        public async Task<IActionResult> GetAllSeller()
        {
            //return Ok(await _businessProvider.GetAllSeller());
            return Ok(await _mediator.Send(new GetAllSellerQuery()));
        }

        [HttpPost]
        [Route("/add-seller")]
        public async Task<IActionResult> AddSeller([FromBody] SellerInfoVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // var result = await _businessProvider.AddSeller(value);
            var result = await _mediator.Send(new AddSellerCommand(value));
            return Created("/api/DataEventRecord", result);
        }

        [HttpGet]
        [Route("/getAllproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            //return Ok(await _businessProvider.GetAllProducts());
            return Ok(await _mediator.Send(new GetAllProductQuery()));
        }



        #region E-Auction Direct API
        [HttpPost]
        [Route("/add-products")]  //Direct use
        public async Task<IActionResult> AddProducts([FromBody] ProductInfoVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var result = await _businessProvider.AddProduct(value);
            var result = await _mediator.Send(new AddProductCommand(value));
            return Created("/api/DataEventRecord", result);
        }

        [HttpGet]
        [Route("/show-bids/{productId}")]
        public async Task<IActionResult> ShowAllBids(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            // return Ok(await _businessProvider.ShowAllBids(productId));
            return Ok(await _mediator.Send(new GetAllBidsQuery(productId)));
        }

        [HttpDelete("/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if(! await _mediator.Send(new ExistProductCommand(id)))
            {
                return NotFound($"Product with Id {id} does not exist");
            }
            //if (!await _businessProvider.ExistsProducts(id))
            //{
            //    return NotFound($"Product with Id {id} does not exist");
            //}
            await _mediator.Send(new DeleteProductCommand(id));
           // await _businessProvider.DeleteProduct(id);

            return Ok();
        }
        #endregion


    }
}
